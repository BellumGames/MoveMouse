using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace MouseMove
{
    internal class MoveMouse
    {
        public struct POINT
        {
            public int x;
            public int y;
        }

        [DllImport("User32.dll", EntryPoint = "mouse_event")]
        internal static extern void Mouse_Event(
          int dwFlags,
          int dx,
          int dy,
          int dwData,
          int dwExtraInfo);

        [DllImport("User32.dll", EntryPoint = "GetSystemMetrics")]
        internal static extern int InternalGetSystemMetrics(int value);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out MoveMouse.POINT point);

        public static MoveMouse.POINT GetMousePosition()
        {
            MoveMouse.POINT point;
            MoveMouse.GetCursorPos(out point);
            return point;
        }

        private static void Main(string[] args)
        {
            int num = 100;
            while (true)
            {
                MoveMouse.POINT mousePosition = MoveMouse.GetMousePosition();
                int systemMetrics1 = MoveMouse.InternalGetSystemMetrics(0);
                int systemMetrics2 = MoveMouse.InternalGetSystemMetrics(1);
                int dx = (int)Math.Round(mousePosition.x + num * 65536.0 / systemMetrics1);
                int dy = (int)Math.Round(mousePosition.y * 65536.0 / systemMetrics2);
                num *= -1;
                MoveMouse.Mouse_Event(32769, dx, dy, 0, 0);
                Thread.Sleep(5000);
            }
        }
    }
}
