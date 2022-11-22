using Pomodoro.Constants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Pomodoro.ViewModels;

public class SampleViewModel : BindableBase
{
    private readonly IRegionManager _regionManager;
    private IRegionNavigationService _navigationService;

    public SampleViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        _regionManager.RegisterViewWithRegion("SamplePage", typeof(SampleViewModel));
        GoToSettingsCommand = new DelegateCommand(GoToSettings);
    }

    public DelegateCommand GoToSettingsCommand { get; set; }
    private void GoToSettings()
    {
        _navigationService = _regionManager.Regions[Regions.Sample].NavigationService;
        _navigationService.Journal.GoBack();
        WelcomeText = "Apertou";
    }


    private string _welcomeText = "Teste";
    public string WelcomeText
    {
        get { return _welcomeText; }
        set { SetProperty(ref _welcomeText, value); }

    }
}
