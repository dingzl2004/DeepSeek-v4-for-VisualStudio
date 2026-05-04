using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;
using System.Runtime.Serialization;

#pragma warning disable VSEXTPREVIEW_SETTINGS // The settings API is currently in preview and marked as experimental

namespace DeepSeek_v4_for_VisualStudio.Settings
{
    internal static class DeepSeekSettings
    {
        [VisualStudioContribution]
        public static SettingCategory SettingsCategory { get; } = new("deepSeekSettings", "DeepSeek Settings")
        {
            Description = "Settings for DeepSeek Visual Studio Extension",
            GenerateObserverClass = true,
        };

        [VisualStudioContribution]
        public static Setting.String ApiKeySetting { get; } = new("apiKey", "API Key", SettingsCategory, defaultValue: "")
        {
            Description = "API Key for DeepSeek",
        };
    }
}