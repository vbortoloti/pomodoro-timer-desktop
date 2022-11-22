using MahApps.Metro.Controls;

using Pomodoro.Constants;

using Prism.Regions;

namespace Pomodoro.Views;

public partial class ShellWindow : MetroWindow
{
    public ShellWindow(IRegionManager regionManager)
    {
        InitializeComponent();
        RegionManager.SetRegionName(shellContentControl, Regions.Main);
        RegionManager.SetRegionManager(shellContentControl, regionManager);
    }
}
