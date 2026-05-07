<h1 align="center">DeepSeek v4 for Visual Studio</h1>

<p align="center">
  <strong>将 DeepSeek V4 大模型深度集成到 Visual Studio 2022+ 中的 AI 编程助手扩展</strong>
</p>

<p align="center">
  <a href="https://github.com/zmy15/DeepSeek-v4-for-VisualStudio/blob/master/LICENSE">
    <img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="License" />
  </a>
  <a href="#">
    <img src="https://img.shields.io/badge/VS-2022%2017.14%2B-purple.svg" alt="Visual Studio 2022+" />
  </a>
  <a href="#">
    <img src="https://img.shields.io/badge/.NET-4.7.2-blueviolet.svg" alt=".NET Framework 4.7.2" />
  </a>
  <a href="#">
    <img src="https://img.shields.io/badge/DeepSeek-V4-green.svg" alt="DeepSeek V4" />
  </a>
</p>

---

## 📖 目录

- [项目简介](#项目简介)
- [核心特性](#核心特性)
- [系统要求](#系统要求)
- [安装指南](#安装指南)
- [配置说明](#配置说明)
- [使用指南](#使用指南)
- [项目架构](#项目架构)
- [技术栈](#技术栈)
- [开发指南](#开发指南)
- [常见问题](#常见问题)
- [贡献指南](#贡献指南)
- [许可证](#许可证)

---

## 项目简介

**DeepSeek v4 for Visual Studio** 是一个 Visual Studio 2022 扩展插件，将 DeepSeek V4 大语言模型无缝集成到 IDE 中。开发者无需离开编辑器即可与 AI 进行对话，获得代码解释、Bug 修复、代码重构、单元测试生成等智能辅助。

### 为什么选择这个扩展？

| 对比维度 | 本扩展 | 网页版 Chat | 其他 AI 插件 |
|---------|--------|------------|-------------|
| IDE 集成 | ✅ 深度集成，可停靠窗口 | ❌ 需切换窗口 | 部分支持 |
| 流式响应 | ✅ 实时流式输出 | ✅ 支持 | 部分支持 |
| 深度思考 | ✅ V4 Reasoning 模式 | ✅ 支持 | ❌ 多数不支持 |
| MCP 协议 | ✅ 支持工具扩展 | ❌ 不支持 | ❌ 少数支持 |
| 联网搜索 | ✅ 百度 + DuckDuckGo | ✅ 支持 | 部分支持 |
| OCR 图像识别 | ✅ 三大引擎 | ❌ 不支持 | ❌ 极少支持 |
| 文件解析 | ✅ 50+ 格式 | ✅ 支持 | 部分支持 |
| 离线可用 | ✅ 聊天记录本地存储 | ✅ 云端存储 | ✅ 支持 |

---

## 核心特性

### 🤖 DeepSeek V4 深度集成

- 支持 **deepseek-v4-pro** 和 **deepseek-v4-flash** 两种模型
- **流式响应 (Streaming)**：实时逐字输出，交互体验流畅
- **深度思考模式 (Reasoning)**：开启后模型会展示推理过程，支持 `high` / `max` 两档推理强度
- 可自定义 System Prompt，灵活调整 AI 角色行为

### 🔧 MCP 协议支持 (Model Context Protocol)

- 支持连接多个 MCP 服务器，扩展 AI 工具调用能力
- 自动将 MCP 工具转换为 DeepSeek API 的 Function Calling 格式
- 内置 MCP 配置管理界面 (`McpConfigDialog.xaml`)
- 支持 `stdio` 传输协议，兼容主流 MCP 服务器

### 🌐 联网搜索

- **双引擎策略**：百度千帆 API（每月 1500 次免费）+ DuckDuckGo（完全免费备用）
- 智能切换：百度额度耗尽自动降级至 DuckDuckGo
- 搜索结果自动融入对话上下文，提升回答时效性

### 📄 文件解析

支持解析 **50+ 种文件格式**：

| 类别 | 支持格式 |
|------|---------|
| 代码文件 | `.cs`, `.py`, `.cpp`, `.java`, `.js`, `.ts`, `.go`, `.rs`, `.swift`, `.kt` 等 |
| 配置文件 | `.json`, `.yaml`, `.xml`, `.toml`, `.ini`, `.cfg` 等 |
| 办公文档 | `.docx`, `.xlsx`, `.pdf` |
| 标记语言 | `.html`, `.css`, `.md`, `.rst` 等 |

### 🔍 图像 OCR（文字识别）

集成三大 OCR 引擎，自动识别截图中的报错信息和代码：

| 引擎 | 中文准确率 | 依赖 | 适用场景 |
|------|-----------|------|---------|
| **Windows 内置** | 一般 | 无（Win10+ 自带） | 英文为主，开箱即用 |
| **PaddleOCR** | ≥ 95% | NuGet 自动部署 | 中文场景，高精度 |
| **MCP Server OCR** | 取决于服务端 | MCP 连接 | 远程/自定义 OCR 服务 |

### 💬 智能聊天窗口

- 基于 **WebView2** 的现代化聊天界面
- **Markdown 渲染**（Markdig）：代码语法高亮、表格、数学公式
- **剪贴板集成**：支持直接粘贴截图进行 OCR 识别
- **聊天记录持久化**：自动保存所有对话，支持跨会话恢复
- 可停靠窗口：自由拖拽停靠在 IDE 任意位置

### ⚙️ 灵活配置

通过 `工具 (Tools)` → `选项 (Options)` → `DeepSeek Chat` 进行配置：

- **API 设置**：API Key、自定义系统提示词
- **模型设置**：模型选择、深度思考开关、推理强度
- **搜索设置**：搜索开关、搜索提供商、百度 API Key
- **OCR 设置**：OCR 引擎选择

---

## 系统要求

| 组件 | 最低要求 |
|------|---------|
| **Visual Studio** | 2022 (v17.14+) |
| **.NET Framework** | 4.7.2 |
| **操作系统** | Windows 10 x64 (v1809+) / Windows 11 |
| **WebView2 Runtime** | 自动部署（随扩展携带） |
| **磁盘空间** | ~500 MB（包含 PaddleOCR 模型） |

---

## 安装指南

### 方法一：从 Release 安装（推荐）

1. 前往 [Releases](https://github.com/zmy15/DeepSeek-v4-for-VisualStudio/releases) 页面
2. 下载最新的 `DeepSeek_v4_for_VisualStudio.vsix`
3. 关闭所有 Visual Studio 实例
4. 双击 `.vsix` 文件，按提示完成安装
5. 重新启动 Visual Studio

### 方法二：从源码编译

```powershell
# 1. 克隆仓库
git clone https://github.com/zmy15/DeepSeek-v4-for-VisualStudio.git
cd DeepSeek-v4-for-VisualStudio

# 2. 使用 Visual Studio 2022 打开 .slnx 解决方案

# 3. 还原 NuGet 包 & 编译
# Build → Build Solution (Ctrl+Shift+B)

# 4. 调试运行（F5），将启动实验性 VS 实例
```

---

## 配置说明

### 第一步：获取 API Key

1. 访问 [DeepSeek 开放平台](https://platform.deepseek.com/api_keys)
2. 注册/登录账号
3. 创建 API Key 并复制

### 第二步：配置扩展

1. 打开 Visual Studio
2. 进入 `工具` → `选项` → `DeepSeek Chat` → `API Settings`
3. 粘贴 API Key 到 `API Key` 字段
4. （可选）自定义 `System Prompt` 调整 AI 行为

### 第三步：选择模型

在 `Model Settings` 中选择：

- **deepseek-v4-pro** — 旗舰模型，最强性能（推荐）
- **deepseek-v4-flash** — 轻量模型，响应更快

> 💡 建议开启 `Enable Deep Thinking`，AI 会展示推理过程，回答质量更高。

---

## 使用指南

### 打开聊天窗口

- **菜单栏**：`视图` → `其他窗口` → `DeepSeek Chat`
- **工具栏**：点击标准工具栏上的 DeepSeek 图标 🧠

### 基本对话

直接在输入框中输入问题，按 `Enter` 发送。例如：

- "解释一下当前文件中的这段代码"
- "帮我重构 UserService 类"
- "这段代码有什么潜在 Bug？"

### 文件解析

将文件拖拽到聊天窗口，或点击 📎 附件按钮选择文件。支持的格式包括代码文件、Office 文档、PDF 等。

### 图像 OCR

1. 截取包含代码或报错信息的屏幕截图
2. 粘贴到聊天窗口（`Ctrl+V`）
3. 扩展会自动识别图像中的文字并填入输入框

### 联网搜索

在聊天窗口底部勾选 🌐 **联网搜索** 开关，AI 将自动搜索最新资料辅助回答。

---

## 项目架构

```
DeepSeek_v4_for_VisualStudio/
├── DeepSeek_v4_for_VisualStudioPackage.cs  # VS 扩展入口 (AsyncPackage)
├── source.extension.vsixmanifest           # VSIX 清单
├── VSCommandTable.vsct                     # 菜单/工具栏命令表
│
├── Commands/
│   └── ShowChatWindowCommand.cs            # 菜单和工具栏命令
│
├── Models/
│   ├── DeepSeekModels.cs                   # API 请求/响应模型
│   └── McpTypes.cs                         # MCP JSON-RPC 协议类型
│
├── Services/
│   ├── DeepSeekApiService.cs               # DeepSeek API 通信（流式）
│   ├── McpManagerService.cs                # MCP 服务器管理 & 工具聚合
│   ├── McpStdioClient.cs                   # MCP stdio 客户端实现
│   ├── McpConfigStore.cs                   # MCP 配置持久化
│   ├── WebSearchService.cs                 # 联网搜索（百度+DuckDuckGo）
│   ├── FileParserService.cs                # 多格式文件解析
│   ├── OcrService.cs                       # OCR 引擎（Windows/PaddleOCR）
│   ├── ChatHtmlService.cs                  # WebView2 HTML 生成
│   ├── ChatPersistenceService.cs           # 聊天记录持久化
│   └── AiPrompts.cs                        # Prompt 集中管理
│
├── Settings/
│   ├── DeepSeekOptionsPage.cs              # 工具→选项 配置页
│   └── DownloadLinkEditor.cs               # 下载链接 UI 编辑器
│
├── View/
│   ├── DeepSeekChatWindowPane.cs           # VS 工具窗口面板
│   ├── DeepSeekChatControl.xaml            # WPF 用户控件布局
│   ├── DeepSeekChatControl.xaml.cs         # WPF 控件逻辑
│   ├── DeepSeekChatControl.*.cs            # 分部类（事件/消息/渲染/会话/剪贴板）
│   └── McpConfigDialog.xaml                # MCP 配置对话框
│
├── Utils/
│   └── Logger.cs                           # 日志工具
│
└── Resources/                              # 图标、样式等资源
```

### 架构特色

- **AsyncPackage**：基于 VS SDK 异步包，支持后台加载，不阻塞 IDE 启动
- **WebView2 前端**：使用 WebView2 渲染聊天界面，支持现代 Web 技术
- **MCP 协议**：完整的 Model Context Protocol 实现，支持工具扩展生态
- **多引擎策略**：搜索和 OCR 均采用多引擎智能切换设计
- **分部类设计**：WPF `DeepSeekChatControl` 通过 6 个分部类文件分离关注点：
  - `DeepSeekChatControl.xaml.cs` — 核心控件逻辑
  - `DeepSeekChatControl.Events.cs` — 事件处理
  - `DeepSeekChatControl.Messaging.cs` — 消息收发
  - `DeepSeekChatControl.Rendering.cs` — 界面渲染
  - `DeepSeekChatControl.Sessions.cs` — 会话管理
  - `DeepSeekChatControl.Clipboard.cs` — 剪贴板处理

---

## 技术栈

| 层级 | 技术 |
|------|------|
| **框架** | .NET Framework 4.7.2, WPF |
| **VS SDK** | Microsoft.VisualStudio.SDK 17.14 |
| **前端渲染** | WebView2 (Chromium) |
| **Markdown** | Markdig 1.1.3 |
| **办公文档** | NPOI 2.8.0 (Word/Excel), PdfPig 0.1.14 (PDF) |
| **OCR** | Windows.Media.Ocr, Sdcb.PaddleOCR 3.0.1, OpenCvSharp 4.10 |
| **JSON** | System.Text.Json |
| **MCP 协议** | JSON-RPC 2.0 over stdio |

---

## 开发指南

### 环境准备

1. 安装 Visual Studio 2022（v17.14+）
2. 安装 `.NET Framework 4.7.2 SDK`
3. 安装 **Visual Studio Extension Development** 工作负载

### 调试

1. 在 VS 中打开 `DeepSeek_v4_for_VisualStudio.slnx`
2. 按 `F5` 启动调试 — 将启动一个新的实验性 VS 实例
3. 在实验性实例中，扩展已自动加载，可直接测试

### 打包发布

```powershell
# 在解决方案目录下运行
msbuild DeepSeek_v4_for_VisualStudio.csproj /p:Configuration=Release
# 输出 .vsix 文件位于 bin/Release/net472/
```

---

## 常见问题

<details>
<summary><b>Q: 扩展安装后找不到聊天窗口？</b></summary>

请检查：
1. 是否已重启 Visual Studio
2. 尝试通过 `视图` → `其他窗口` → `DeepSeek Chat` 打开
3. 检查扩展管理器 (`扩展` → `管理扩展`) 确认扩展已启用
</details>

<details>
<summary><b>Q: 提示 API Key 无效？</b></summary>

请确认：
1. API Key 是否从 [DeepSeek 开放平台](https://platform.deepseek.com/api_keys) 正确获取
2. API Key 是否有可用余额
3. 配置路径：`工具` → `选项` → `DeepSeek Chat` → `API Settings`
</details>

<details>
<summary><b>Q: OCR 识别中文不准？</b></summary>

将 OCR 引擎切换为 PaddleOCR：
1. 进入 `工具` → `选项` → `DeepSeek Chat` → `OCR Settings`
2. 将 `OCR Engine` 改为 `PaddleOCR`
3. PaddleOCR 模型随 NuGet 包自动部署，无需额外下载
</details>

<details>
<summary><b>Q: 如何添加自定义 MCP 服务器？</b></summary>

1. 在聊天窗口中点击 🔌 MCP 配置按钮
2. 在弹出的对话框中添加服务器配置（名称、命令、参数）
3. 保存后扩展会自动连接并加载工具列表
</details>

<details>
<summary><b>Q: WebView2 初始化失败？</b></summary>

扩展已内置 WebView2 Runtime（x64），正常情况下无需单独安装。若仍有问题，请从 [Microsoft 官网](https://developer.microsoft.com/microsoft-edge/webview2/) 下载安装 Evergreen Runtime。
</details>

---

## 贡献指南

我们欢迎所有形式的贡献！请遵循以下流程：

1. **Fork** 本仓库
2. 创建功能分支：`git checkout -b feature/amazing-feature`
3. 提交更改：`git commit -m 'feat: add amazing feature'`
4. 推送分支：`git push origin feature/amazing-feature`
5. 提交 **Pull Request**

### 提交规范

请使用 [Conventional Commits](https://www.conventionalcommits.org/) 格式：

- `feat:` 新功能
- `fix:` Bug 修复
- `docs:` 文档更新
- `refactor:` 代码重构
- `chore:` 构建/工具变更

---

## 许可证

本项目基于 [MIT License](LICENSE) 开源。

---

## 📧 联系方式

- **Issues**: [GitHub Issues](https://github.com/zmy15/DeepSeek-v4-for-VisualStudio/issues)
- **讨论**: [GitHub Discussions](https://github.com/zmy15/DeepSeek-v4-for-VisualStudio/discussions)

---

<p align="center">
  <sub>Made with ❤️ by <a href="https://github.com/zmy15">zmy15</a> | Powered by <a href="https://www.deepseek.com/">DeepSeek</a></sub>
</p>
