using WpfApp.Views;

namespace App.Helpers
{

    public class WindowHelper
    {
        public enum WindowStatus
        {
            Minimazed,
            Floating,
            Normal,
            Maximazed
        };
        public class WindowSize
        {
            public double Height { get; private set; }
            public double Width { get; private set; }
            public double Left { get; private set; }
            public double Top { get; private set; }
            public WindowSize(double height, double width)
            {
                Height = height;
                Width = width;
            }
            public WindowSize(double height, double width, double left, double top)
            {
                Height = height;
                Width = width;
                Left = left;
                Top = top;
            }
        }

        internal static void RepositionWindowOnBottomLeft(ShellWindow win)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            win.Left = desktopWorkingArea.Right - win.Width;
            win.Top = desktopWorkingArea.Bottom - win.Height;
        }

        internal static void RepositionWindowOnCenter(ShellWindow win)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = win.Width;
            double windowHeight = win.Height;
            win.Left = (screenWidth / 2) - (windowWidth / 2);
            win.Top = (screenHeight / 2) - (windowHeight / 2);
        }
        internal static void SetWindowSize(ShellWindow shellWin, WindowSize win)
        {
            shellWin.Height = win.Height;
            shellWin.Width = win.Width;
        }
    }
}