using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace FaceRecognitionSample1.Helpers
{
    public static class WindowBehavior
    {
        // Define an attached property to preserve the aspect ratio
        public static readonly DependencyProperty AspectRatioProperty =
            DependencyProperty.RegisterAttached(
                "AspectRatio",
                typeof(double),
                typeof(WindowBehavior),
                new PropertyMetadata(0.0, OnAspectRatioChanged));

        public static double GetAspectRatio(DependencyObject obj) => (double)obj.GetValue(AspectRatioProperty);
        public static void SetAspectRatio(DependencyObject obj, double value) => obj.SetValue(AspectRatioProperty, value);

        // A callback that is automatically called when a value is set in XAML
        private static void OnAspectRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                double ratio = (double)e.NewValue;
                if (ratio <= 0) return;

                // Register the Win32 hook when window initialization is complete
                window.SourceInitialized += (sender, args) =>
                {
                    WindowInteropHelper helper = new WindowInteropHelper(window);
                    HwndSource source = HwndSource.FromHwnd(helper.Handle);
                    source.AddHook((IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) =>
                    {
                        const int WM_SIZING = 0x0214;
                        const int WMSZ_LEFT = 1;
                        const int WMSZ_RIGHT = 2;
                        const int WMSZ_TOP = 3;
                        const int WMSZ_BOTTOM = 6;

                        if (msg == WM_SIZING)
                        {
                            RECT rc = (RECT)Marshal.PtrToStructure(lParam, typeof(RECT));
                            int width = rc.Right - rc.Left;
                            int height = rc.Bottom - rc.Top;
                            int edge = wParam.ToInt32();

                            if (edge == WMSZ_LEFT || edge == WMSZ_RIGHT)
                            {
                                rc.Bottom = rc.Top + (int)(width / ratio);
                            }
                            else if (edge == WMSZ_TOP || edge == WMSZ_BOTTOM)
                            {
                                rc.Right = rc.Left + (int)(height * ratio);
                            }
                            else
                            {
                                rc.Bottom = rc.Top + (int)(width / ratio);
                            }

                            Marshal.StructureToPtr(rc, lParam, false);
                            handled = true;
                        }
                        return IntPtr.Zero;
                    });
                };
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left; public int Top; public int Right; public int Bottom;
        }
    }
}