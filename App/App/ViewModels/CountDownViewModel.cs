using App.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
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
            CounterManager.Play();
        }
        public DelegateCommand PauseCommand { get; set; }
        private void Pause()
        {
            CounterManager.Pause();
        }

        public DelegateCommand ResetCommand { get; set; }
        private void Reset()
        {
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
            CountText = $"{CounterManager.GetActiveCounter().countDuration}:00";
        }

    }
}
