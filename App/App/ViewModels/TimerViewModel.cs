using App.Model;
using App.WPFModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class TimerViewModel: BindableBase
    {
        #region popup
        private void cancelCreateCommand()
        {
            CreatePopUpState = false;
        }

      
        private void openPopUpCreateCommand()
        {
            CreatePopUpState = true;

        }

        private bool _createPopUpState = false;
        public bool CreatePopUpState
        {
            get { return _createPopUpState; }
            set { SetProperty(ref _createPopUpState, value);}
        }
        
        private bool _isLowFocus = false;
        public bool IsLowFocus
        {
            get { return _isLowFocus; }
            set { SetProperty(ref _isLowFocus, value); if (value == true) { CounterManager.workDuration = 10; CounterManager.shortDuration = 5; CounterManager.longDuration = 10; CounterManager.UpdateCountersDuration(); } }
        }
        private bool _isDemostration = false;
        public bool IsDemostration
        {
            get { return _isDemostration; }
            set { SetProperty(ref _isDemostration, value); if (value == true) { CounterManager.workDuration = 5; CounterManager.shortDuration = 2; CounterManager.longDuration = 4; CounterManager.UpdateCountersDurationInSeconds(); } }
        }
        private bool _isStandard = true;
        public bool IsStandard
        {
            get { return _isStandard; }
            set { SetProperty(ref _isStandard, value); if (value == true) { CounterManager.workDuration = 25; CounterManager.shortDuration = 5; CounterManager.longDuration = 15; CounterManager.UpdateCountersDuration(); } }
        }

        private bool _isHighFocus = false;
        public bool IsHighFocus
        {
            get { return _isHighFocus; }
            set { SetProperty(ref _isHighFocus, value); if (value == true) { CounterManager.workDuration = 40; CounterManager.shortDuration = 8; CounterManager.longDuration = 20; CounterManager.UpdateCountersDuration(); } }
        } 
        
        private bool _isIntenseFocus = false;
        public bool IsIntenseFocus
        {
            get { return _isIntenseFocus; }
            set { SetProperty(ref _isIntenseFocus, value); if (value == true) { CounterManager.workDuration = 55; CounterManager.shortDuration = 10; CounterManager.longDuration = 25; CounterManager.UpdateCountersDuration(); } }
        }

        private bool _isCustom = false;
        public bool IsCustom
        {
            get { return _isCustom; }
            set { SetProperty(ref _isCustom, value); }
        }
        
        private int _workSliderValue = CounterManager.workDuration;
        public int WorkSliderValue
        {
            get { return _workSliderValue; }
            set { SetProperty(ref _workSliderValue, value); CounterManager.workDuration = value; CounterManager.UpdateCountersDuration(); }
        }
        
        private int _shortSliderValue = CounterManager.shortDuration;
        public int ShortSliderValue
        {
            get { return _shortSliderValue; }
            set { SetProperty(ref _shortSliderValue, value); CounterManager.shortDuration = value; CounterManager.UpdateCountersDuration(); }
        }
        
        private int _longSliderValue = CounterManager.longDuration;
        public int LongSliderValue
        {
            get { return _longSliderValue; }
            set { SetProperty(ref _longSliderValue, value); CounterManager.longDuration = value; CounterManager.UpdateCountersDuration();  }
        }

        public DelegateCommand CreateCommand { get; set; }
        public DelegateCommand CancelCreateCommand { get; set; }
        public DelegateCommand OpenPopUpCreateCommand { get; set; }


        public DelegateCommand UpdateSliderCommand { get; set; }
        private void UpdateSlider()
        {
            PlaySound();
        }

        #endregion
        public Dictionary<string, string> ActiveColorScheme = new Dictionary<string, string>()
        {
            {"Background", "#FFE05656" },
            {"Foreground", "#FFFFFF" },
            {"BorderBrush", "#Transparent" }
        };

        public Dictionary<string, string> DeactiveColorScheme = new Dictionary<string, string>()
        {
            {"Background", "#FFFFFF" },
            {"Foreground", "#FF717171" },
            {"BorderBrush", "#FFD8D8D8" }
        };
        public TimerViewModel()
        {
            CancelCreateCommand = new DelegateCommand(cancelCreateCommand);
            OpenPopUpCreateCommand = new DelegateCommand(openPopUpCreateCommand);

            UpdateSliderCommand = new DelegateCommand(UpdateSlider);

            WorkCommand = new DelegateCommand(WorkButton);
            ShortCommand = new DelegateCommand(ShortButton);
            LongCommand = new DelegateCommand(LongButton);

            SetWorkButton(ActiveColorScheme);
            SetShortButton(DeactiveColorScheme);
            SetLongButton(DeactiveColorScheme);
            CounterManager.CounterFinished += OnCounterFinished;
        }

        #region Buttons Properties
        private string _workButtonBackground;
        public string WorkButtonBackground
        {
            get { return _workButtonBackground; }
            set { SetProperty(ref _workButtonBackground, value); }
        }
        private string _workButtonForeground;
        public string WorkButtonForeground
        {
            get { return _workButtonForeground; }
            set { SetProperty(ref _workButtonForeground, value); }
        }
        private string _workButtonBorderBrush;
        public string WorkButtonBorderBrush
        {
            get { return _workButtonBorderBrush; }
            set { SetProperty(ref _workButtonBorderBrush, value); }
        }

        private string _shortButtonBackground;
        public string ShortButtonBackground
        {
            get { return _shortButtonBackground; }
            set { SetProperty(ref _shortButtonBackground, value); }
        }
        private string _shortButtonForeground;
        public string ShortButtonForeground
        {
            get { return _shortButtonForeground; }
            set { SetProperty(ref _shortButtonForeground, value); }
        }
        private string _shortButtonBorderBrush;
        public string ShortButtonBorderBrush
        {
            get { return _shortButtonBorderBrush; }
            set { SetProperty(ref _shortButtonBorderBrush, value); }
        }
        

        private string _longButtonBackground;
        public string LongButtonBackground
        {
            get { return _longButtonBackground; }
            set { SetProperty(ref _longButtonBackground, value); }
        }
        private string _longButtonForeground;
        public string LongButtonForeground
        {
            get { return _longButtonForeground; }
            set { SetProperty(ref _longButtonForeground, value); }
        }
        private string _longButtonBorderBrush;
        public string LongButtonBorderBrush
        {
            get { return _longButtonBorderBrush; }
            set { SetProperty(ref _longButtonBorderBrush, value); }
        }
# endregion

        #region Buttons Color Scheme Functions
        public void SetWorkButton(Dictionary<string, string> ColorScheme)
        {
            string background, foreground, borderBrush;
            ColorScheme.TryGetValue("Background", out background);
            ColorScheme.TryGetValue("Foreground", out foreground);
            ColorScheme.TryGetValue("BorderBrush", out borderBrush);
            
            WorkButtonBackground = background;
            WorkButtonForeground = foreground;
            WorkButtonBorderBrush = borderBrush;
        }    
        public void SetShortButton(Dictionary<string, string> ColorScheme)
        {
            string background, foreground, borderBrush;
            ColorScheme.TryGetValue("Background", out background);
            ColorScheme.TryGetValue("Foreground", out foreground);
            ColorScheme.TryGetValue("BorderBrush", out borderBrush);
            
            ShortButtonBackground = background;
            ShortButtonForeground = foreground;
            ShortButtonBorderBrush = borderBrush;
        }  
        public void SetLongButton(Dictionary<string, string> ColorScheme)
        {
            string background, foreground, borderBrush;
            ColorScheme.TryGetValue("Background", out background);
            ColorScheme.TryGetValue("Foreground", out foreground);
            ColorScheme.TryGetValue("BorderBrush", out borderBrush);
            
            LongButtonBackground = background;
            LongButtonForeground = foreground;
            LongButtonBorderBrush = borderBrush;
        }
        #endregion

        #region Buttons Delegates
        public DelegateCommand WorkCommand { get; set; }
        
        private void Work()
        {
            CounterManager.SetActiveCounter("work");
            SetWorkButton(ActiveColorScheme);
            SetShortButton(DeactiveColorScheme);
            SetLongButton(DeactiveColorScheme);
        }
        private void WorkButton()
        {
            PlaySound();
            Work();
        }
        public DelegateCommand ShortCommand { get; set; }
        private void Short()
        {
            CounterManager.SetActiveCounter("short");
            SetWorkButton(DeactiveColorScheme);
            SetShortButton(ActiveColorScheme);
            SetLongButton(DeactiveColorScheme);
        }
        private void ShortButton()
        {
            PlaySound();
            Short();
        }
        public DelegateCommand LongCommand { get; set; }
        private void Long()
        {
            CounterManager.SetActiveCounter("long");
            SetWorkButton(DeactiveColorScheme);
            SetShortButton(DeactiveColorScheme);
            SetLongButton(ActiveColorScheme);
        }
        private void LongButton()
        {
            PlaySound();
            Long();
        }
        #endregion

        private static void PlaySound()
        {
            var sound = Properties.Resources.click;
            SoundPlayer player = new SoundPlayer(sound);
            player.Play();
        }
        public void OnCounterFinished(object source, TimerEventArgs e)
        {
            if (e.ActiveCounter == "work") { Work(); return; }
            if (e.ActiveCounter == "short") { Short(); return; }
            if (e.ActiveCounter == "long") { Long(); return; }
        }
    }

}
