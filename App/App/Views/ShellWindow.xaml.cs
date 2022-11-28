using App.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : IModule
    {
        private enum WindowStatus
        {
            Minimazed,
            Floating,
            Normal,
            Maximazed
        };

        private IRegionManager region;
        public class WindowSize
        {
            public double Height { get;  private set; }
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

        WindowSize NormalSize = new WindowSize(400, 400);
        WindowSize FloatingSize = new WindowSize(180, 250);

        private WindowStatus windowStatus = WindowStatus.Normal;
        public ShellWindow()
        {
            InitializeComponent();
            NormalSize = new WindowSize(this.Height, this.Width, this.Left, this.Top);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            region = containerProvider.Resolve<IRegionManager>();
            region.RegisterViewWithRegion("MainRegion", typeof(MaximazedView));
        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        public void CustomMaximaze()
        {
            this.WindowState = WindowState.Normal;
            windowStatus = WindowStatus.Normal;
            this.Topmost = true;
            SetWindowSize(NormalSize);
            CenterWindowOnScreen();
            //this.WindowStyle = WindowStyle.None;
        }
        public void CustomMinimaze()
        {
            this.WindowState = WindowState.Normal;
            windowStatus = WindowStatus.Floating;
            this.Topmost = true;
            SetWindowSize(FloatingSize);
            BottomLeftWindowOnScreen();
            //this.WindowStyle = WindowStyle.None;
        }
        private void BottomLeftWindowOnScreen()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
        private void SetWindowSize(WindowSize win)
        {
            this.Height = win.Height;
            this.Width = win.Width;
        }

        private void Float_Click(object sender, RoutedEventArgs e)
        {
            CustomMinimaze();
            
        }


        private void Normal_Click(object sender, RoutedEventArgs e)
        {
            CustomMaximaze();
        }

    }
}
