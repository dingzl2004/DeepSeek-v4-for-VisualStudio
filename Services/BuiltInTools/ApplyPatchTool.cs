using DeepSeek_v4_for_VisualStudio.Models;
using DeepSeek_v4_for_VisualStudio.Services.EditTools;
using DeepSeek_v4_for_VisualStudio.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeepSeek_v4_for_VisualStudio.Services.BuiltInTools
{
    /// <summary>
    /// apply_patch 工具 — 应用 *** Begin Patch / *** End Patch 格式的补丁。
    /// 解析和应用委托给 EditTools.ApplyPatchTool（OpenAI Codex 兼容的 Chunk 重建引擎）。
    /// </summary>
    public class ApplyPatchTool : BuiltInToolBase
    {
        public override string Name => "apply_patch";

        public override ToolDefinition GetDefinition()
        {
            return new ToolDefinition
            {
                Type = "function",
                Function = new ToolFunction
                {
                    Name = "apply_patch",
                    Description = L["tool.apply_patch.desc"],
                    Parameters = new
                    {
                        type = "object",
                        properties = new
                        {
                            patch = new
                            {
                                type = "string",
                                description = LocalizationService.Instance["tool.applyPatch.description"]
                            }
                        },
                        required = new[] { "patch" }
                    }
                }
            };
        }

        public override string GetDisplayText(Dictionary<string, JsonElement> args)
        {
            return LocalizationService.Instance["tool.applyPatch.displayText"];
        }

        public override string GetResultSummary(string toolResult)
        {
            if (string.IsNullOrEmpty(toolResult)) return LocalizationService.Instance["tool.common.noResult"];
            if (toolResult.StartsWith("❌") || toolResult.StartsWith("⚠️")) return toolResult;
            return LocalizationService.Instance["tool.applyPatch.complete"];
        }

        public override async Task<string> ExecuteAsync(Dictionary<string, JsonElement> args, string? workspaceRoot)
        {
            string patchText = GetStringArg(args, "patch");

            if (string.IsNullOrEmpty(patchText))
                return LocalizationService.Instance["tool.applyPatch.missingParam"];

            workspaceRoot = NormalizeWorkspaceRoot(workspaceRoot);

            try
            {
                var results = new List<string>();

                // ── 统一使用 EditTools 版的解析器（OpenAI Codex 兼容，正则前缀检测）──
                var patches = EditTools.ApplyPatchTool.ParsePatches(patchText);

                if (patches.Count == 0)
                {
                    return LocalizationService.Instance["tool.applyPatch.noPatchBlock"] + "\n"
                        + LocalizationService.Instance["tool.applyPatch.formatHint"] + "\n"
                        + "*** Begin Patch\n"
                        + "*** Update File: /path/to/file\n"
                        + "@@ some context\n"
                        + " context line\n"
                        + "- old line to remove\n"
                        + "+ new line to add\n"
                        + " context line\n"
                        + "*** End Patch";
                }

                foreach (var patch in patches)
                {
                    string filePath = ResolvePath(patch.FilePath, workspaceRoot);

                    // ── 使用 EditTools 版的应用引擎（Chunk 重建、5级匹配、缩进适配、括号去重）──
                    var result = EditTools.ApplyPatchTool.ApplySinglePatch(
                        patch, filePath,
                        File.Exists(filePath) ? await Task.Run(() => File.ReadAllText(filePath)) : string.Empty);

                    if (result.Success && !string.IsNullOrEmpty(result.FinalContent))
                    {
                        await Task.Run(() => File.WriteAllText(filePath,
                            EditStringMatcher.NormalizeToCrLf(result.FinalContent)));
                        results.Add(LocalizationService.Instance.Format("tool.applyPatch.applied",
                            Path.GetFileName(filePath), patch.Hunks.Count));
                    }
                    else if (result.Success)
                    {
                        // Move/Delete 操作 — result.Success=true 但 FinalContent 为空
                        results.Add(LocalizationService.Instance.Format("tool.applyPatch.applied",
                            Path.GetFileName(filePath), patch.Hunks.Count));
                    }
                    else
                    {
                        string errorMsg = result.ErrorMessage ?? LocalizationService.Instance["tool.applyPatch.hunkFail"];
                        results.Add(errorMsg);
                    }
                }

                return results.Count > 0
                    ? string.Join("\n", results)
                    : LocalizationService.Instance["tool.applyPatch.noAction"];
            }
            catch (Exception ex)
            {
                return LocalizationService.Instance.Format("tool.applyPatch.failed", ex.Message);
            }
        }
    }
}
