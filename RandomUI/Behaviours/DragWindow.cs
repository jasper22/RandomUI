
namespace RandomUI.Behaviours
{
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interactivity;
    using RandomUI.Controls;

    /// <summary>
    /// Behavior to drag move border-less window
    /// </summary>
    public class DragWindow : Behavior<Window>
    {
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

            this.currentWindow.MouseLeftButtonDown += (o, e) =>
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    // Drag started
                    this.currentWindow.RaiseStartWindowDragEvent();

                    currentWindow.DragMove();

                    // Drag ended
                    this.currentWindow.RaiseFinishWindowDragEvent();
                }
            };
        }

        protected void DoDragMove(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
