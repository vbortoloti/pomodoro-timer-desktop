using App.Model;
using App.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using static App.Helpers.WindowHelper;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : IModule
    {
        
        WindowSize NormalSize = new WindowSize(720, 1280);
        WindowSize FloatingSize = new WindowSize(180, 250);

        private WindowStatus windowStatus = WindowStatus.Normal;

        static IRegionManager region;
        static IContainerProvider ContainerProvider;
        public ShellWindow()
        {

            InitializeComponent();
;        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ShellWindow.region = containerProvider.Resolve<IRegionManager>();
            ShellWindow.region.RegisterViewWithRegion("MainRegion", typeof(MaximazedView));
            SetWindowSize(this, NormalSize);
        }
        public void RegisterTypes(IContainerRegistry containerRegistry){ }

        public void CustomMaximaze()
        {
            windowStatus = WindowStatus.Normal;
            this.Topmost = true;
            SetWindowSize(this,NormalSize);
            RepositionWindowOnCenter(this);
        }
        public void CustomMinimaze()
        {
            windowStatus = WindowStatus.Floating;
            this.Topmost = true;
            SetWindowSize(this,FloatingSize);
            RepositionWindowOnBottomLeft(this);
        }

        private void Float_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            CustomMinimaze();
            ShellWindow.region.RequestNavigate("MainRegion", "MinimazedView");
            CounterManager.SwitchView();
        }

        private void Normal_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            CustomMaximaze();
            ShellWindow.region.RequestNavigate("MainRegion", "MaximazedView");
            CounterManager.SwitchView();
        }

    }
}
