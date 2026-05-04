using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeek_v4_for_VisualStudio.Windows
{
    /// <summary>
    /// A remote user control to use as tool window UI content.
    /// 注意：RemoteUserControl 是 RPC 代理，运行在扩展进程中。
    /// 真实的 WPF 控件位于 VS 主进程，无法通过 VisualTreeHelper 等方式直接访问。
    /// 所有 UI 交互必须通过 DataContext 的数据绑定来实现。
    /// </summary>
    internal class DeepSeekChatWindowContent : RemoteUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeepSeekChatWindowContent" /> class.
        /// </summary>
        public DeepSeekChatWindowContent(DeepSeekChatWindowData dataContext) : base(dataContext)
        {
        }

        public override async Task ControlLoadedAsync(CancellationToken cancellationToken)
        {
            await base.ControlLoadedAsync(cancellationToken);
        }

        protected override void Dispose(bool disposing)
        {
            // DataContext 由 ToolWindow 负责 dispose
            base.Dispose(disposing);
        }
    }
}