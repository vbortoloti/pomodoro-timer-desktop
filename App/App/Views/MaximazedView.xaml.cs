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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace App.Views
{
    /// <summary>
    /// Interaction logic for MaximazedView.xaml
    /// </summary>
    public partial class MaximazedView : IModule
    {
        public MaximazedView()
        {
            InitializeComponent();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var region = containerProvider.Resolve<IRegionManager>();
            region.RegisterViewWithRegion("TaskRegion", typeof(TaskView));
            region.RegisterViewWithRegion("TimerRegion", typeof(TimerView));
            region.RegisterViewWithRegion("MusicPlayerRegion", typeof(MusicPlayerView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //throw new NotImplementedException();
        }
    }
}
