using App.DbContexts;
using App.Model;
using App.Repositories;
using App.Services;
using App.WPFModel;
using FontAwesome5;
using ImTools;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace App.ViewModels
{
    public class TaskViewModel : BindableBase
    {

        public List<PomodoroWPFModel> convertPomodoro2WPFModel (List<Pomodoro> pomodoroList)
        {
            List<PomodoroWPFModel> pomodoroWPF = new List<PomodoroWPFModel>();
            foreach ( var pomod in pomodoroList)
            {
                pomodoroWPF.Add(new PomodoroWPFModel()
                {
                    Id = pomod.Id.ToString(),
                    NumberPomodoro= pomod.numberPomodoro,
                    MaxPomodoro = pomod.maxPomodoro,
                    NumberSmallBreakInterval = pomod.numberSmallBreakInterval,
                    MaxSmallBreakInterval = pomod.maxSmallBreakInterval,
                    NumberLongBreakInterval = pomod.numberLongBreakInterval,
                    MaxLongBreakInterval = pomod.maxLongBreakInterval,
                    Description= pomod.description,
                    Status = pomod.status,
                    Selected = pomod.selected,
                    PopUpShowState = false,

                });
            }
            return pomodoroWPF;
        }

        public List<PomodoroWPFModel> OpenPopUp(List<Pomodoro> pomodoroList, Guid Id)
        {
            List<PomodoroWPFModel> pomodoroWPF = new List<PomodoroWPFModel>();
            foreach (var pomod in pomodoroList)
            {
                pomodoroWPF.Add(new PomodoroWPFModel()
                {
                    Id = pomod.Id.ToString(),
                    NumberPomodoro = pomod.numberPomodoro,
                    MaxPomodoro = pomod.maxPomodoro,
                    NumberSmallBreakInterval = pomod.numberSmallBreakInterval,
                    MaxSmallBreakInterval = pomod.maxSmallBreakInterval,
                    NumberLongBreakInterval = pomod.numberLongBreakInterval,
                    MaxLongBreakInterval = pomod.maxLongBreakInterval,
                    Description = pomod.description,
                    Status = pomod.status,
                    Selected = pomod.selected,
                    PopUpShowState = (Id == pomod.Id)? true :false,

                });
            }
            return pomodoroWPF;
        }
        public List<PomodoroWPFModel> ClosePopUp(List<Pomodoro> pomodoroList, Guid Id)
        {
            List<PomodoroWPFModel> pomodoroWPF = new List<PomodoroWPFModel>();
            foreach (var pomod in pomodoroList)
            {
                pomodoroWPF.Add(new PomodoroWPFModel()
                {
                    Id = pomod.Id.ToString(),
                    NumberPomodoro = pomod.numberPomodoro,
                    MaxPomodoro = pomod.maxPomodoro,
                    NumberSmallBreakInterval = pomod.numberSmallBreakInterval,
                    MaxSmallBreakInterval = pomod.maxSmallBreakInterval,
                    NumberLongBreakInterval = pomod.numberLongBreakInterval,
                    MaxLongBreakInterval = pomod.maxLongBreakInterval,
                    Description = pomod.description,
                    Status = pomod.status,
                    Selected = pomod.selected,
                    PopUpShowState = (Id == pomod.Id) ? false : true,
                });
            }
            return pomodoroWPF;
        }

        public TaskViewModel()
        {
            AddCommand= new DelegateCommand(addCommand);
            OnMouseOver = new DelegateCommand(onMouseHover);
            SelectCommand = new DelegateCommand<object>(obj => selectCommand(obj));
            OpenPopUPCommand = new DelegateCommand<object>(obj => openPopUPCommand(obj));
            
            EditCommand = new DelegateCommand<object>(obj => editCommand(obj));
            DeleteCommand = new DelegateCommand<object>(obj => deleteCommand(obj));
            CancelCommand = new DelegateCommand<object>(obj => cancelCommand(obj));
            //OnMouseHover= new DelegateCommand(onMouseHover);
            PomodoroRepository.DeselectPomodoros();
            
            var pomodoroList = PomodoroRepository.GetPomodoros();
            if (pomodoroList != null && pomodoroList?.Count > 0)
            {
                ObservableCollection<PomodoroWPFModel> itemList = new ObservableCollection<PomodoroWPFModel>(convertPomodoro2WPFModel(pomodoroList));
                //SelectedPomodoro = itemList[0].Id;
                ItemList = itemList;

            } else
            {
                ItemList = new ObservableCollection<PomodoroWPFModel>();

            }

            SelectedPomodoro = "";
        }

        public void RefreshCollection(List<PomodoroWPFModel> newItemList)
        {
            ItemList.Clear();
            var newColection= new ObservableCollection<PomodoroWPFModel>();
            var count = 0;
            foreach (var item in newItemList)
            {
                ItemList.Add(item);
                //if (count < 1)
                //    ItemList.Add(item);

                //}
                //count++;
            }

        }

        private ObservableCollection<PomodoroWPFModel> _itemList;
        public ObservableCollection<PomodoroWPFModel> ItemList
        {
            get { return _itemList; }
            set {
                _itemList = value;
                RaisePropertyChanged(nameof(ItemList));
                SetProperty(ref _itemList, value); 
            }
        }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand OnMouseOver{ get; set; }
        public DelegateCommand<object> SelectCommand { get; set; }
        public DelegateCommand<object> OpenPopUPCommand { get; set; }

        public DelegateCommand<object> EditCommand { get; set; }
        public DelegateCommand<object> DeleteCommand{ get; set; }
        public DelegateCommand<object> CancelCommand{ get; set; }


        private void openPopUPCommand(object sender)
        {
            var pomodoro = sender as PomodoroWPFModel;
            var newOpenPopUpState = OpenPopUp(PomodoroRepository.GetPomodoros(), new Guid(pomodoro.Id));
            RefreshCollection(newOpenPopUpState);
        }


        //public DelegateCommand OnMouseHover { get; set; }
        private void addCommand()
        {

            Console.WriteLine("Vamos trigar o add command");
            //SelectedIndex
            //PomodoroRepository.SavePomodoros(new Pomodoro()
            //{

            //});
            //ItemList = PomodoroRepository.GetPomodoros();

        }

        private void selectCommand(object sender)
        {
            var pomodoro = sender as PomodoroWPFModel;
            if (SelectedPomodoro != "")
            {
                PomodoroRepository.UpdatePomodoroStatus(new Guid(SelectedPomodoro), false);
            }
            SelectedPomodoro = pomodoro?.Id;
            PomodoroRepository.UpdatePomodoroStatus(new Guid(pomodoro.Id), true);
            RefreshCollection(convertPomodoro2WPFModel(PomodoroRepository.GetPomodoros()));

        }

        private void editCommand(object sender)
        {

            Console.WriteLine("Vamos trigar o edit command");
            var pomodoro = sender as PomodoroWPFModel;
            SelectedPomodoro = pomodoro?.Id;
            ItemList = new ObservableCollection<PomodoroWPFModel>(convertPomodoro2WPFModel(PomodoroRepository.GetPomodoros()));

            //SelectedIndex = 
        }

        private void deleteCommand(object sender)
        {

            Console.WriteLine("Vamos trigar o delete command");
            var pomodoro = sender as PomodoroWPFModel;
            PomodoroRepository.DeletePomodoro(new Guid(pomodoro.Id));
            RefreshCollection(convertPomodoro2WPFModel(PomodoroRepository.GetPomodoros()));

        }


        private void cancelCommand(object sender)
        {

            Console.WriteLine("Vamos trigar o cancel command");
            var pomodoro = sender as PomodoroWPFModel;
            ClosePopUp(PomodoroRepository.GetPomodoros(), new Guid(pomodoro.Id));
            ItemList = new ObservableCollection<PomodoroWPFModel>(convertPomodoro2WPFModel(PomodoroRepository.GetPomodoros()));

            //SelectedIndex = 
        }


        private String _selectedPomodoro = "";
        public String SelectedPomodoro
        {
            get { return _selectedPomodoro; }
            set { SetProperty(ref _selectedPomodoro, value); }
        }

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
