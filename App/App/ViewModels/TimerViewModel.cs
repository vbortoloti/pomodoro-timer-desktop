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
            WelcomeCommand = new DelegateCommand(welcome);
        }
            

        private string _welcomeText = "Teste";
        public string WelcomeText
        {
            get { return _welcomeText; }
            set { SetProperty(ref _welcomeText, value); }
        }

        public DelegateCommand WelcomeCommand { get; set; }
        private void welcome()
        {
            WelcomeText = "Apertou";
        }
    }
}
