
namespace RandomUI.Controls.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Unsafe Windows API
    /// </summary>
    internal class UnsafeNativeMethods
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="UnsafeNativeMethods"/> class from being created.
        /// </summary>
        private UnsafeNativeMethods()
        {
        }

        /// <summary>
        /// WindowStyle values, WS_*
        /// </summary>
        [Flags]
        internal enum WS : uint
        {
            OVERLAPPED = 0x00000000,
            POPUP = 0x80000000,
            CHILD = 0x40000000,
            MINIMIZE = 0x20000000,
            VISIBLE = 0x10000000,
            DISABLED = 0x08000000,
            CLIPSIBLINGS = 0x04000000,
            CLIPCHILDREN = 0x02000000,
            MAXIMIZE = 0x01000000,
            BORDER = 0x00800000,
            DLGFRAME = 0x00400000,
            VSCROLL = 0x00200000,
            HSCROLL = 0x00100000,
            SYSMENU = 0x00080000,
            THICKFRAME = 0x00040000,
            GROUP = 0x00020000,
            TABSTOP = 0x00010000,

            MINIMIZEBOX = 0x00020000,
            MAXIMIZEBOX = 0x00010000,

            CAPTION = BORDER | DLGFRAME,
            TILED = OVERLAPPED,
            ICONIC = MINIMIZE,
            SIZEBOX = THICKFRAME,
            TILEDWINDOW = OVERLAPPEDWINDOW,

            OVERLAPPEDWINDOW = OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX,
            POPUPWINDOW = POPUP | BORDER | SYSMENU,
            CHILDWINDOW = CHILD,
        }

        /// <summary>
        /// GetWindowLongPtr values, GWL_*
        /// </summary>
        internal enum GWL
        {
            WNDPROC = (-4),
            HINSTANCE = (-6),
            HWNDPARENT = (-8),
            STYLE = (-16),
            EXSTYLE = (-20),
            USERDATA = (-21),
            ID = (-12)
        }


        /// <summary>
        /// Returned by the GetThemeMargins function to define the margins of windows that have visual styles applied.
        /// </summary>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb773244(v=vs.85).aspx
        /// </remarks>
        internal struct MARGINS
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MARGINS"/> struct.
            /// </summary>
            /// <param name="t">The t.</param>
            public MARGINS(System.Windows.Thickness t)
            {
                Left = (int)t.Left;
                Right = (int)t.Right;
                Top = (int)t.Top;
                Bottom = (int)t.Bottom;
            }

            /// <summary>
            /// The left
            /// </summary>
            public int Left;

            /// <summary>
            /// The right
            /// </summary>
            public int Right;

            /// <summary>
            /// The top
            /// </summary>
            public int Top;

            /// <summary>
            /// The bottom
            /// </summary>
            public int Bottom;
        }


        /// <summary>
        /// Gets the window long at x64 system.
        /// </summary>
        /// <param name="hWnd">The HWND to Window</param>
        /// <param name="nIndex">Index of the <see cref="GWL"/></param>
        /// <returns>Pointer to the Window</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, GWL nIndex);

        /// <summary>
        /// Gets the window long at x86 system
        /// </summary>
        /// <param name="hWnd">The HWND to Window</param>
        /// <param name="nIndex">Index of the <see cref="GWL"/></param>
        /// <returns>Pointer to the Window</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowLong", SetLastError = true)]
        private static extern int GetWindowLongPtr32(IntPtr hWnd, GWL nIndex);

        /// <summary>
        /// Sets the window long at x86 system
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <param name="dwNewLong">The dw new long.</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern int SetWindowLongPtr32(IntPtr hWnd, GWL nIndex, int dwNewLong);

        /// <summary>
        /// Sets the window long at x64 system
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <param name="dwNewLong">The dw new long.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, GWL nIndex, IntPtr dwNewLong);

        /// <summary>
        /// Extends the window frame into the client area.
        /// </summary>
        /// <param name="hwnd">The handle to the window in which the frame will be extended into the client area.</param>
        /// <param name="margins">A pointer to a MARGINS structure that describes the margins to use when extending the frame into the client area.</param>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/aa969512(v=vs.85).aspx
        /// </remarks>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        /// <summary>
        /// Obtains a value that indicates whether Desktop Window Manager (DWM) composition is enabled. 
        /// Applications on machines running Windows 7 or earlier can listen for composition state changes by handling the WM_DWMCOMPOSITIONCHANGED notification.
        /// </summary>
        /// <returns>A pointer to a value that, when this function returns successfully, receives TRUE if DWM composition is enabled; otherwise, FALSE.</returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/aa969518(v=vs.85).aspx
        /// </remarks>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DwmIsCompositionEnabled();

        /// <summary>
        /// Gets the pointer to window
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="gwlIndex">Index of the GWL.</param>
        /// <returns>Pointer to window</returns>
        /// <exception cref="Win32Exception"></exception>
        internal static IntPtr GetWindowLongPtr(IntPtr hwnd, GWL gwlIndex)
        {
            IntPtr ret = IntPtr.Zero;

            if (Environment.Is64BitProcess == true)
            {
                ret = GetWindowLongPtr64(hwnd, gwlIndex);
            }
            else
            {
                ret = new IntPtr(GetWindowLongPtr32(hwnd, gwlIndex));
            }

            if (ret == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            return ret;
        }

        /// <summary>
        /// Sets the window to <see cref="GWL"/> and <paramref name="style"/> via pointer
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="gwlIndex">Index of the GWL.</param>
        /// <param name="style">The style.</param>
        internal static IntPtr SetWindowLongPtr(IntPtr hwnd, GWL gwlIndex, IntPtr style)
        {
            if (Environment.Is64BitProcess == true)
            {
                return SetWindowLongPtr64(hwnd, gwlIndex, style);
            }
            else
            {
                return new IntPtr(SetWindowLongPtr32(hwnd, gwlIndex, style.ToInt32()));
            }
        }
    }
}
