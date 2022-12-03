using App.DbContexts;
using App.Model;
using App.Repositories;
using App.Services;
using FontAwesome5;
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
using System.Windows.Controls;

namespace App.ViewModels
{
    public class TaskViewModel : BindableBase
    {

        public TaskViewModel()
        {
            AddCommand= new DelegateCommand(addCommand);
            OnMouseOver = new DelegateCommand(onMouseHover);
            //OnMouseHover= new DelegateCommand(onMouseHover);
            var pomodoroList = PomodoroRepository.GetPomodoros();
            ObservableCollection<Pomodoro> itemList = new ObservableCollection<Pomodoro>(pomodoroList);
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
        public DelegateCommand OnMouseOver{ get; set; }
        //public DelegateCommand OnMouseHover { get; set; }
        private void addCommand()
        {

            Console.WriteLine("Vamos trigar o add command");
            //PomodoroRepository.SavePomodoros(new Pomodoro()
            //{

            //});
            //ItemList = PomodoroRepository.GetPomodoros();

        }

        //private bool _isMouseOver= true;
        //public bool IsMouseOver
        //{
        //    get { return _isMouseOver; }
        //    set { SetProperty(ref _isMouseOver, value); }
        //}

        //public void onMouseHover()
        //{
        //    Console.WriteLine("Vamos trigar o hover command");
        //    IsMouseOver = true;


        //}


        private bool _isMouseOver = true;
        public bool mouseOver
        {
            get { return _isMouseOver; }
            set { SetProperty(ref _isMouseOver, value); }
        }

        public void onMouseHover()
        {
            Console.WriteLine("Vamos trigar o hover command");
            mouseOver = true;


        }
    }
}
