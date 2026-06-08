using DeepSeek_v4_for_VisualStudio.Models;
using DeepSeek_v4_for_VisualStudio.Services;
using DeepSeek_v4_for_VisualStudio.Utils;
using Microsoft.VisualStudio.Shell;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DeepSeek_v4_for_VisualStudio.View
{
    /// <summary>
    /// 主题相关方法：检测 VS 主题、切换浅色/深色模式、更新 WPF 控件颜色。
    /// </summary>
    public partial class DeepSeekChatControl
    {
        #region Theme

        /// <summary>
        /// 主题切换按钮点击：在 Auto → Dark → Light 之间循环。
        /// </summary>
        private void ThemeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var nextMode = _themeService.UserThemeMode switch
            {
                ThemeMode.Auto => ThemeMode.Dark,
                ThemeMode.Dark => ThemeMode.Light,
                ThemeMode.Light => ThemeMode.Auto,
                _ => ThemeMode.Auto
            };
            _themeService.UserThemeMode = nextMode;
            UpdateThemeToggleIcon();
        }

        /// <summary>
        /// VS 主题或用户设置变更时触发。
        /// </summary>
        private void OnThemeChanged(bool isLight)
        {
            if (_isApplyingTheme) return;
            _isApplyingTheme = true;
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                ApplyWpfTheme(isLight);
                UpdateThemeToggleIcon();
                ReloadWebViewForTheme();
            }
            catch (Exception ex)
            {
                Logger.Warn($"[Theme] Failed to apply theme: {ex.Message}");
            }
            finally
            {
                _isApplyingTheme = false;
            }
        }

        /// <summary>
        /// 更新主题切换按钮图标。
        /// </summary>
        private void UpdateThemeToggleIcon()
        {
            try
            {
                if (ThemeToggleIcon == null) return;
                ThemeToggleIcon.Text = _themeService.UserThemeMode switch
                {
                    ThemeMode.Auto => "🌓",
                    ThemeMode.Dark => "🌙",
                    ThemeMode.Light => "☀️",
                    _ => "🌓"
                };

                var tooltip = _themeService.UserThemeMode switch
                {
                    ThemeMode.Auto => $"主题: 自动 ({(_themeService.IsLight ? "浅色" : "深色")})",
                    ThemeMode.Dark => "主题: 深色",
                    ThemeMode.Light => "主题: 浅色",
                    _ => "切换主题"
                };
                ThemeToggleButton.ToolTip = tooltip;
            }
            catch { }
        }

        /// <summary>
        /// 应用 WPF 控件颜色主题。
        /// </summary>
        private void ApplyWpfTheme(bool isLight)
        {
            if (isLight)
            {
                ApplyWpfLightTheme();
            }
            else
            {
                ApplyWpfDarkTheme();
            }
        }

        /// <summary>
        /// 应用 WPF 深色主题颜色（原有样式）。
        /// </summary>
        private void ApplyWpfDarkTheme()
        {
            var panelBg = new SolidColorBrush(Color.FromRgb(0x2D, 0x2D, 0x2D));
            var panelBorder = new SolidColorBrush(Color.FromRgb(0x3F, 0x3F, 0x46));
            var textColor = new SolidColorBrush(Color.FromRgb(0xD4, 0xD4, 0xD4));
            var mutedText = new SolidColorBrush(Color.FromRgb(0x88, 0x88, 0x88));
            var accentBlue = new SolidColorBrush(Color.FromRgb(0x6C, 0xAF, 0xD9));
            var accentGreen = new SolidColorBrush(Color.FromRgb(0x4E, 0xC9, 0xB0));
            var inputBg = new SolidColorBrush(Color.FromRgb(0x2D, 0x2D, 0x2D));
            var diffBarBg = new SolidColorBrush(Color.FromRgb(0x1A, 0x3A, 0x20));

            ApplyWpfColors(panelBg, panelBorder, textColor, mutedText, accentBlue, accentGreen, inputBg, diffBarBg);
        }

        /// <summary>
        /// 应用 WPF 浅色主题颜色。
        /// </summary>
        private void ApplyWpfLightTheme()
        {
            var panelBg = new SolidColorBrush(Color.FromRgb(0xF0, 0xF0, 0xF0));
            var panelBorder = new SolidColorBrush(Color.FromRgb(0xD0, 0xD0, 0xD0));
            var textColor = new SolidColorBrush(Color.FromRgb(0x33, 0x33, 0x33));
            var mutedText = new SolidColorBrush(Color.FromRgb(0x88, 0x88, 0x88));
            var accentBlue = new SolidColorBrush(Color.FromRgb(0x00, 0x78, 0xD4));
            var accentGreen = new SolidColorBrush(Color.FromRgb(0x2E, 0xA8, 0x7A));
            var inputBg = new SolidColorBrush(Color.FromRgb(0xF5, 0xF5, 0xF5));
            var diffBarBg = new SolidColorBrush(Color.FromRgb(0xE8, 0xF5, 0xE9));

            ApplyWpfColors(panelBg, panelBorder, textColor, mutedText, accentBlue, accentGreen, inputBg, diffBarBg);
        }

        /// <summary>
        /// 将主题颜色应用到所有 WPF 控件。
        /// </summary>
        private void ApplyWpfColors(
            SolidColorBrush panelBg, SolidColorBrush panelBorder,
            SolidColorBrush textColor, SolidColorBrush mutedText,
            SolidColorBrush accentBlue, SolidColorBrush accentGreen,
            SolidColorBrush inputBg, SolidColorBrush diffBarBg)
        {
            try
            {
                bool isLight = textColor.Color.R < 0x80; // 浅色主题文字颜色偏暗

                // ── 会话选择栏 ──
                ApplyBorderBrush(FindParentBorder(SessionComboBox), panelBg, panelBorder);

                // ── 状态栏 ──
                ApplyBorderBrush(FindParentBorder(StatusLabel), panelBg, panelBorder);
                if (StatusLabel != null) StatusLabel.Foreground = mutedText;

                // ── Diff 全局控制栏 ──
                if (DiffGlobalBar != null) DiffGlobalBar.Background = diffBarBg;

                // ── 输入区 ──
                if (InputTextBox != null)
                {
                    InputTextBox.Foreground = textColor;
                    InputTextBox.CaretBrush = textColor;
                }
                if (InputPlaceholder != null)
                    InputPlaceholder.Foreground = mutedText;
                ApplyBorderBrush(FindParentBorder(InputTextBox), inputBg, panelBorder);

                // ── 审批控制栏 ──
                ApplyBorderBrush(FindParentBorder(ApprovalModeComboBox), panelBg, panelBorder);

                // ── 发送按钮 ──
                if (SendButton != null) SendButton.Foreground = accentBlue;

                // ── 新对话按钮 ──
                if (NewChatButton != null) NewChatButton.Foreground = accentGreen;

                // ── 上传按钮 ──
                if (UploadButton != null) UploadButton.Foreground = new SolidColorBrush(Color.FromRgb(0xCE, 0x91, 0x78));

                // ── 删除会话按钮 ──
                if (DeleteSessionButton != null) DeleteSessionButton.Foreground = mutedText;
            }
            catch (Exception ex)
            {
                Logger.Warn($"[Theme] ApplyWpfColors error: {ex.Message}");
            }
        }

        private static void ApplyBorderBrush(Border? border, SolidColorBrush bg, SolidColorBrush borderBrush)
        {
            if (border == null) return;
            border.Background = bg;
            border.BorderBrush = borderBrush;
        }

        /// <summary>
        /// 向上查找最近的 Border 父元素。
        /// </summary>
        private static Border? FindParentBorder(DependencyObject? child)
        {
            if (child == null) return null;
            var parent = System.Windows.Media.VisualTreeHelper.GetParent(child);
            while (parent != null)
            {
                if (parent is Border border)
                    return border;
                parent = System.Windows.Media.VisualTreeHelper.GetParent(parent);
            }
            return null;
        }

        /// <summary>
        /// 主题变更后重新加载 WebView2 内容。
        /// 如果 WebView 已就绪且有消息，则重新渲染所有消息。
        /// </summary>
        private async void ReloadWebViewForTheme()
        {
            try
            {
                if (ChatWebView?.CoreWebView2 == null) return;
                if (_messages.Count == 0) return;

                // 重新生成完整 HTML 页面
                string newHtml = ChatHtmlService.BuildInitialPage(_messages);
                ChatWebView.CoreWebView2.NavigateToString(newHtml);
                Logger.Info("[Theme] WebView2 reloaded with new theme");
            }
            catch (Exception ex)
            {
                Logger.Warn($"[Theme] Failed to reload WebView2: {ex.Message}");
            }
        }

        #endregion
    }
}
