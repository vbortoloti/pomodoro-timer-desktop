using App.Model;
using ImTools;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class TaskViewModel : BindableBase
    {

        public TaskViewModel()
        {
            AddCommand= new DelegateCommand(addCommand);
            ObservableCollection<Pomodoro> itemList = new ObservableCollection<Pomodoro>();
            itemList.Add(new Pomodoro()
            {
                description = "Primeiro Pomodoro",
                numberPomodoro = 5,
                maxPomodoro=10,
            });
            itemList.Add(new Pomodoro()
            {
                description = "Segundo Pomodoro",
                numberPomodoro = 5,
                maxPomodoro = 10,
            });
            itemList.Add(new Pomodoro()
            {
                description = "Terceiro Pomodoro",
                numberPomodoro = 5,
                maxPomodoro = 10,
            });
            itemList.Add(new Pomodoro()
            {
                description = "Quarto Pomodoro",
                numberPomodoro = 5,
                maxPomodoro = 10,
            });

            ItemList = itemList;
        }

        private ObservableCollection<Pomodoro> _itemList;
        public ObservableCollection<Pomodoro> ItemList
        {
            get { return _itemList; }
            set { SetProperty(ref _itemList, value); }
        }

        private string _welcomeText = "Teste";
        public string WelcomeText
        {
            get { return _welcomeText; }
            set { SetProperty(ref _welcomeText, value); }
        }

        public DelegateCommand AddCommand { get; set; }
        private void addCommand()
        {
            
        }
    }
}
