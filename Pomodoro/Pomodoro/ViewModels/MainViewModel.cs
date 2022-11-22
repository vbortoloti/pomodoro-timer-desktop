using Pomodoro.Constants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Pomodoro.ViewModels;

public class MainViewModel : BindableBase
{

    private readonly IRegionManager _regionManager;
    private IRegionNavigationService _navigationService;

    public MainViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }
    public DelegateCommand GoToSettingsCommand { get; set; }
    private void GoToSettings()
    {
        _navigationService = _regionManager.Regions[Regions.Main].NavigationService;
        _navigationService.RequestNavigate(PageKeys.Sample);
    }

}
