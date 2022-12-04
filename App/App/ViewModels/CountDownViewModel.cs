using App.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace App.ViewModels
{
    public class CountDownViewModel: BindableBase
    {
        private string _countText = "00:00";
        public string CountText
        {
            get { return _countText; }
            set { SetProperty(ref _countText, value); }
        }

        

        public CountDownViewModel()
        {
            Counter counter = new Counter(1500,this);
            counter.InitializeTimer();
        }
  
    }
}
