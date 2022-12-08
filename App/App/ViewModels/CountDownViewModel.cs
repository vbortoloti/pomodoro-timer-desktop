using App.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace App.ViewModels
{
    public class CountDownViewModel : BindableBase
    {
        private string _countText = $"{CounterManager.GetActiveCounter().countDuration}:00";
        public string CountText
        {
            get { return _countText; }
            set { SetProperty(ref _countText, value); }
        }

        public DelegateCommand PlayCommand { get; set; }
        private void Play()
        {
            PlaySound();
            if(!CounterManager.GetActiveCounter().isRunning)
                CounterManager.Play();
        }
        public DelegateCommand PauseCommand { get; set; }
        private void Pause()
        {
            PlaySound();
            CounterManager.Pause();
        }

        public DelegateCommand ResetCommand { get; set; }
        private void Reset()
        {
            PlaySound();
            CounterManager.Reset();
        }
        public CountDownViewModel()
        {
            CounterManager.SetAsActive(this);
            PlayCommand = new DelegateCommand(Play); 
            PauseCommand = new DelegateCommand(Pause); 
            ResetCommand = new DelegateCommand(Reset);
            CounterManager.ActiveCounterChange += OnActiveCounterChange;
        }

        public void OnActiveCounterChange(object source, EventArgs e)
        {
            DateTime time;
            if (CounterManager.GetActiveCounter().isSeconds)
            {
                 time = new DateTime().AddSeconds(CounterManager.GetActiveCounter().countDuration);
            }
            else
            {
                 time = new DateTime().AddMinutes(CounterManager.GetActiveCounter().countDuration);

            }
            CountText = time.ToString("mm\\:ss");
        }

        private static void PlaySound()
        {
            var sound = Properties.Resources.click;
            SoundPlayer player = new SoundPlayer(sound);
            player.Play();
        }

    }
}
