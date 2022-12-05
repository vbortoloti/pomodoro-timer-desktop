using App.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class TimerViewModel: BindableBase
    {
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
            WorkCommand = new DelegateCommand(Work);
            ShortCommand = new DelegateCommand(Short);
            LongCommand = new DelegateCommand(Long);

            SetWorkButton(ActiveColorScheme);
            SetShortButton(DeactiveColorScheme);
            SetLongButton(DeactiveColorScheme);
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
        public DelegateCommand ShortCommand { get; set; }
        private void Short()
        {
            CounterManager.SetActiveCounter("short");
            SetWorkButton(DeactiveColorScheme);
            SetShortButton(ActiveColorScheme);
            SetLongButton(DeactiveColorScheme);
        }
        public DelegateCommand LongCommand { get; set; }
        private void Long()
        {
            CounterManager.SetActiveCounter("long");
            SetWorkButton(DeactiveColorScheme);
            SetShortButton(DeactiveColorScheme);
            SetLongButton(ActiveColorScheme);
        }
        #endregion
    }
}
