
namespace RandomUI.Behaviours
{
    using System;
    using System.Windows;
    using Microsoft.Xaml.Behaviors;
    using System.Windows.Interop;
    using System.Windows.Media;
    using Controls.Helpers;

    /// <summary>
    /// Behavior could fix 'transparent' issues with WinForms controls that located on transparent WPF window
    /// </summary>
    public class NativeTransperantWindowBehavior : Behavior<Window>
    {
        private Window window;

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.window = AssociatedObject as Window;
            this.window.SourceInitialized += currentWindow_SourceInitialized;
        }

        /// <summary>
        /// Handles the SourceInitialized event of the currentWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>http://blogs.msdn.com/b/adam_nathan/archive/2006/05/04/589686.aspx</remarks>
        protected void currentWindow_SourceInitialized(object sender, EventArgs e)
        {
            if (UnsafeNativeMethods.DwmIsCompositionEnabled() == false)
            {
                return;
            }

            IntPtr hwnd = new WindowInteropHelper(this.window).Handle;
            if (hwnd == IntPtr.Zero)
            {
                throw new InvalidOperationException("The Window must be shown before extending glass.");
            }

            // Set the background to transparent from both the WPF and Win32 perspectives
            window.Background = Brushes.Transparent;
            HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent;

            UnsafeNativeMethods.MARGINS margins = new UnsafeNativeMethods.MARGINS(new Thickness(-1, -1, -1, -1));     // -1 = All size

            UnsafeNativeMethods.DwmExtendFrameIntoClientArea(hwnd, ref margins);
        }
    }
}
