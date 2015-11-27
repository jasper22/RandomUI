
namespace RandomUI.Behaviours
{
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Interactivity;
    using RandomUI.Controls;

    /// <summary>
    /// 'Resize window' behavior
    /// </summary>
    public class WindowResizeBehavior : Behavior<Window>
    {
        private WindowResizingAdorner windowResizingAdorner;

        private EtchedWindow currentWindow;

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.currentWindow = AssociatedObject as EtchedWindow;

            this.currentWindow.Loaded += CurrentWindow_Loaded;

            //base.OnAttached();
        }

        /// <summary>
        /// Handles the Loaded event of the CurrentWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        protected void CurrentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var topBorder = this.currentWindow.GetWindowRootBorder();
            this.windowResizingAdorner = new WindowResizingAdorner((UIElement)topBorder, currentWindow);

            var adornerLayer = AdornerLayer.GetAdornerLayer((UIElement)topBorder);
            adornerLayer.Add(this.windowResizingAdorner);
        }
    }
}
