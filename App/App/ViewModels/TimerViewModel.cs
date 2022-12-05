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
        public TimerViewModel()
        {
            WorkCommand = new DelegateCommand(Work);
            ShortCommand = new DelegateCommand(Short);
            LongCommand = new DelegateCommand(Long);
        }
            
        public DelegateCommand WorkCommand { get; set; }
        private void Work()
        {
            CounterManager.SetActiveCounter("work");
        }
        public DelegateCommand ShortCommand { get; set; }
        private void Short()
        {
            CounterManager.SetActiveCounter("short");
        }
        public DelegateCommand LongCommand { get; set; }
        private void Long()
        {
            CounterManager.SetActiveCounter("long");
        }
    }
}
