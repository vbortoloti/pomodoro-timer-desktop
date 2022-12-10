using App.DbContexts;
using App.Interface;
using App.Model;
using App.Repositories;
using App.Services;
using App.WPFModel;
using FontAwesome5;
using ImTools;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace App.ViewModels
{
    public class TaskViewModel : BindableBase
    {

        public List<PomodoroWPFModel> convertPomodoro2WPFModel(List<Pomodoro> pomodoroList)
        {
            List<PomodoroWPFModel> pomodoroWPF = new List<PomodoroWPFModel>();
            var index = 0;
            foreach (var pomod in pomodoroList)
            {
                pomodoroWPF.Add(new PomodoroWPFModel()
                {
                    Id = pomod.Id.ToString(),
                    NumberPomodoro = NumberPomodoroState[index],
                    MaxPomodoro = pomod.maxPomodoro,
                    //NumberSmallBreakInterval = pomod.numberSmallBreakInterval,
                    //MaxSmallBreakInterval = pomod.maxSmallBreakInterval,
                    //NumberLongBreakInterval = pomod.numberLongBreakInterval,
                    //MaxLongBreakInterval = pomod.maxLongBreakInterval,
                    Description = pomod.description,
                    Status = pomod.status,
                    Selected = pomod.selected,
                    PopUpShowState = false,

                });
                index++;
            }
            return pomodoroWPF;
        }
        public Pomodoro convertWPFModel2Pomodoro(PomodoroWPFModel wpfModel)
        {
            return new Pomodoro()
            {
                Id = new Guid(wpfModel.Id),
                //numberPomodoro = wpfModel.NumberPomodoro,
                maxPomodoro = wpfModel.MaxPomodoro,
                //numberSmallBreakInterval = wpfModel.NumberSmallBreakInterval,
                //maxSmallBreakInterval = wpfModel.MaxSmallBreakInterval,
                //numberLongBreakInterval = wpfModel.NumberLongBreakInterval,
                //maxLongBreakInterval = wpfModel.MaxLongBreakInterval,
                description = wpfModel.Description,
                status = wpfModel.Status,
                selected = wpfModel.Selected,

            };
        }

        public List<PomodoroWPFModel> OpenPopUp(List<Pomodoro> pomodoroList, Guid Id)
        {
            List<PomodoroWPFModel> pomodoroWPF = new List<PomodoroWPFModel>();
            var index = 0;
            foreach (var pomod in pomodoroList)
            {
                pomodoroWPF.Add(new PomodoroWPFModel()
                {
                    Id = pomod.Id.ToString(),
                    //NumberPomodoro = pomod.numberPomodoro,
                    NumberPomodoro = NumberPomodoroState[index],
                    MaxPomodoro = pomod.maxPomodoro,
                    //NumberSmallBreakInterval = pomod.numberSmallBreakInterval,
                    //MaxSmallBreakInterval = pomod.maxSmallBreakInterval,
                    //NumberLongBreakInterval = pomod.numberLongBreakInterval,
                    //MaxLongBreakInterval = pomod.maxLongBreakInterval,
                    Description = pomod.description,
                    Status = pomod.status,
                    Selected = pomod.selected,
                    PopUpShowState = (Id == pomod.Id) ? true : false,

                });
                index++;

            }
            return pomodoroWPF;
        }
        public List<PomodoroWPFModel> ClosePopUp(List<Pomodoro> pomodoroList, Guid Id)
        {
            List<PomodoroWPFModel> pomodoroWPF = new List<PomodoroWPFModel>();
            var index = 0;
            foreach (var pomod in pomodoroList)
            {
                pomodoroWPF.Add(new PomodoroWPFModel()
                {
                    Id = pomod.Id.ToString(),
                    NumberPomodoro = NumberPomodoroState[index],
                    //NumberPomodoro = pomod.numberPomodoro,
                    MaxPomodoro = pomod.maxPomodoro,
                    //NumberSmallBreakInterval = pomod.numberSmallBreakInterval,
                    //MaxSmallBreakInterval = pomod.maxSmallBreakInterval,
                    //NumberLongBreakInterval = pomod.numberLongBreakInterval,
                    //MaxLongBreakInterval = pomod.maxLongBreakInterval,
                    Description = pomod.description,
                    Status = pomod.status,
                    Selected = pomod.selected,
                    PopUpShowState = (Id == pomod.Id) ? false : true,
                });
                index++;
            }
            return pomodoroWPF;
        }

        public static bool alreadySubscribed = false;

        public static int counter = 0;

        private List<int> initializeNumberPomodoro(int len)
        {
            var newList = new List<int> { };
            for (var index = 0; index < len;  index++)
            {
                newList.Add(0);
            }
            return newList;
        }
        public TaskViewModel()
        {
            AddCommand = new DelegateCommand(addCommand);
            CancelCreateCommand = new DelegateCommand(cancelCreateCommand);
            CreateCommand = new DelegateCommand(createCommand);
            OpenPopUpCreateCommand = new DelegateCommand(openPopUpCreateCommand);
            OnMouseOver = new DelegateCommand(onMouseHover);
            SelectCommand = new DelegateCommand<object>(obj => selectCommand(obj));
            OpenPopUPCommand = new DelegateCommand<object>(obj => openPopUPCommand(obj));

            EditCommand = new DelegateCommand<object>(obj => editCommand(obj));
            DeleteCommand = new DelegateCommand<object>(obj => deleteCommand(obj));
            CancelCommand = new DelegateCommand<object>(obj => cancelCommand(obj));
            //OnMouseHover= new DelegateCommand(onMouseHover);
            PomodoroRepository.DeselectPomodoros();
            PomodoroRepository.SelectFirst();
            CounterManager.CounterFinished += sumOneInSelected;

            //if (!alreadySubscribed)
            //{
            //    CounterManager.CounterFinished += sumOneInSelected;
            //    alreadySubscribed = true;
            //}

            //counter = counter + 1;
            //if(counter == 6)
            //{
            //    CounterManager.CounterFinished += sumOneInSelected;
            //}

            var pomodoroList = PomodoroRepository.GetPomodoros();
            NumberPomodoroState = initializeNumberPomodoro(pomodoroList.Count);

            if (pomodoroList != null && pomodoroList?.Count > 0)
            {
                ObservableCollection<PomodoroWPFModel> itemList = new ObservableCollection<PomodoroWPFModel>(convertPomodoro2WPFModel(pomodoroList));
                SelectedPomodoro = itemList[0].Id;
                ItemList = itemList;

            }
            else
            {
                ItemList = new ObservableCollection<PomodoroWPFModel>();
                SelectedPomodoro = "";

            }

        }

        public void RefreshCollection(List<PomodoroWPFModel> newItemList)
        {
            ItemList.Clear();
            var newColection = new ObservableCollection<PomodoroWPFModel>();
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
            set
            {
                _itemList = value;
                RaisePropertyChanged(nameof(ItemList));
                SetProperty(ref _itemList, value);
            }
        }

        private List<int> _numberPomodoroState;
        public List<int> NumberPomodoroState
        {
            get { return _numberPomodoroState; }
            set { SetProperty(ref _numberPomodoroState, value); }
        }




        public DelegateCommand CreateCommand { get; set; }
        public DelegateCommand CancelCreateCommand { get; set; }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand OpenPopUpCreateCommand { get; set; }
        public DelegateCommand OnMouseOver { get; set; }
        public DelegateCommand<object> SelectCommand { get; set; }
        public DelegateCommand<object> OpenPopUPCommand { get; set; }

        public DelegateCommand<object> EditCommand { get; set; }
        public DelegateCommand<object> DeleteCommand { get; set; }
        public DelegateCommand<object> CancelCommand { get; set; }


        private void openPopUPCommand(object sender)
        {
            var pomodoro = sender as PomodoroWPFModel;
            var newOpenPopUpState = OpenPopUp(PomodoroRepository.GetPomodoros(), new Guid(pomodoro.Id));
            RefreshCollection(newOpenPopUpState);
        }

        private void cancelCreateCommand()
        {
            CreatePopUpState = false;
            resetCreateState();
        }

        private void resetCreateState()
        {
            NewNumberPomodoro = "";
            NewDescription = "";
        }


        //public DelegateCommand OnMouseHover { get; set; }
        private void addCommand()
        {
            CreatePopUpState = true;
        }

        private void openPopUpCreateCommand()
        {
            CreatePopUpState = true;

        }

        private void selectCommand(object sender)
        {
            PomodoroRepository.DeselectPomodoros();

            var pomodoro = sender as PomodoroWPFModel;
            if (SelectedPomodoro != "")
            {
                PomodoroRepository.UpdatePomodoroStatus(new Guid(SelectedPomodoro), false);
            }
            SelectedPomodoro = pomodoro?.Id;
            PomodoroRepository.UpdatePomodoroStatus(new Guid(pomodoro.Id), true);
            RefreshCollection(convertPomodoro2WPFModel(PomodoroRepository.GetPomodoros()));
            //sumOneInSelected();

        }

        private void createCommand()
        {
            try
            {
                PomodoroRepository.DeselectPomodoros();
                var newPomdoroItem = new Pomodoro()
                {
                    maxPomodoro = int.Parse(NewNumberPomodoro),
                    description = NewDescription,
                    //numberPomodoro = 0,
                    status = "Regular_Circle",
                    selected = true,
                };
                NumberPomodoroState.Add(0);
                PomodoroRepository.SavePomodoros(newPomdoroItem);
                SelectedPomodoro = newPomdoroItem.Id.ToString();

                ItemList = new ObservableCollection<PomodoroWPFModel>(convertPomodoro2WPFModel(PomodoroRepository.GetPomodoros()));


                resetCreateState();

                CreatePopUpState = false;
            }
            catch (Exception Erro)
            {
                Console.WriteLine(Erro);
            }
        }
        private void editCommand(object sender)
        {

            Console.WriteLine("Vamos trigar o edit command");
            var pomodoro = sender as PomodoroWPFModel;
            SelectedPomodoro = pomodoro?.Id;
            var pomodoroIndex= findPomodoroIndex(pomodoro?.Id);
            if (pomodoroIndex >= 0)
            {
                NumberPomodoroState[pomodoroIndex] = pomodoro.NumberPomodoro;
            }
            PomodoroRepository.UpdatePomodoro(new Guid(pomodoro.Id), convertWPFModel2Pomodoro(pomodoro));


            ItemList = new ObservableCollection<PomodoroWPFModel>(convertPomodoro2WPFModel(PomodoroRepository.GetPomodoros()));
            ClosePopUp(PomodoroRepository.GetPomodoros(), new Guid(pomodoro.Id));

            //SelectedIndex = 
        }

        private void deleteCommand(object sender)
        {

            Console.WriteLine("Vamos trigar o delete command");
            var pomodoro = sender as PomodoroWPFModel;
            var pomodoroIndex = findPomodoroIndex(pomodoro?.Id);
            if (pomodoroIndex >= 0)
            {
                NumberPomodoroState.RemoveAt(pomodoroIndex);
            }
            PomodoroRepository.DeletePomodoro(new Guid(pomodoro.Id));
            PomodoroRepository.SelectFirst();
            var newPomodorosList = convertPomodoro2WPFModel(PomodoroRepository.GetPomodoros());
            RefreshCollection(newPomodorosList);
            if (pomodoro.Id == SelectedPomodoro)
            {
                if (newPomodorosList.Count > 0)
                {
                    SelectedPomodoro = newPomodorosList[0].Id;
                }
            }
            ClosePopUp(PomodoroRepository.GetPomodoros(), new Guid(pomodoro.Id));

        }

        private int findPomodoroIndex(string Id)
        {
            var pomodoroList = convertPomodoro2WPFModel(PomodoroRepository.GetPomodoros());
            for (var index = 0; index < pomodoroList.Count; index++)
            {
                if (pomodoroList[index].Id == Id)
                {
                    return index;
                }
            }
            return -1;
        }

        private PomodoroWPFModel sumUmLogic()
        {
            var pomodoroList = convertPomodoro2WPFModel(PomodoroRepository.GetPomodoros());
            int selectedPomodoroIndex = findPomodoroIndex(SelectedPomodoro);
            if (selectedPomodoroIndex >= 0)
            {
                while (selectedPomodoroIndex < pomodoroList.Count)
                {
                    if (selectedPomodoroIndex == pomodoroList.Count - 1)
                    {
                        if (pomodoroList[selectedPomodoroIndex].NumberPomodoro >= pomodoroList[selectedPomodoroIndex].MaxPomodoro)
                            return null;
                    }
                    if (pomodoroList[selectedPomodoroIndex].NumberPomodoro < pomodoroList[selectedPomodoroIndex].MaxPomodoro)
                    {
                        pomodoroList[selectedPomodoroIndex].NumberPomodoro += 1;
                        pomodoroList[selectedPomodoroIndex].Selected = true;
                        return pomodoroList[selectedPomodoroIndex];
                    }

                    selectedPomodoroIndex++;
                }

            }
            return null;
        }

        public void sumOneInSelected(object source, TimerEventArgs timer)
        //public void sumOneInSelected()
        {
            //var pomodorosList = PomodoroRepository.GetPomodoros();
            //var newPomodorosList = convertPomodoro2WPFModel();
            //RefreshCollection(newPomodorosList);
            counter++;
            if ( timer.ActiveCounter != "work")
            {
                PomodoroRepository.DeselectPomodoros();

                Console.WriteLine("Vamos trigar comando soma 1");
                PomodoroWPFModel selectedPomodoro = sumUmLogic();
                if (selectedPomodoro != null)
                {
                    var pomodoroIndex = findPomodoroIndex(selectedPomodoro.Id);
                    if (pomodoroIndex >= 0)
                    {
                        NumberPomodoroState[pomodoroIndex] += 1;
                    }

                    PomodoroRepository.UpdatePomodoro(new Guid(selectedPomodoro.Id), convertWPFModel2Pomodoro(selectedPomodoro));
                    SelectedPomodoro = selectedPomodoro.Id;

                }
                var pomodoros = PomodoroRepository.GetPomodoros();
                RefreshCollection(convertPomodoro2WPFModel(pomodoros));

            }
        }

        //public void sumOneInSelected()
        //{
        //    //var pomodorosList = PomodoroRepository.GetPomodoros();
        //    //var newPomodorosList = convertPomodoro2WPFModel();
        //    //RefreshCollection(newPomodorosList);
        //    PomodoroRepository.DeselectPomodoros();

        //    Console.WriteLine("Vamos trigar comando soma 1");
        //    PomodoroWPFModel selectedPomodoro = sumUmLogic();
        //    if (selectedPomodoro != null)
        //    {
        //        PomodoroRepository.UpdatePomodoro(new Guid(selectedPomodoro.Id), convertWPFModel2Pomodoro(selectedPomodoro));
        //        SelectedPomodoro = selectedPomodoro.Id;

        //    }
        //    var pomodoros = PomodoroRepository.GetPomodoros();

        //    RefreshCollection(convertPomodoro2WPFModel(pomodoros));

        //}

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

        private bool _createPopUpState = false;
        public bool CreatePopUpState
        {
            get { return _createPopUpState; }
            set { SetProperty(ref _createPopUpState, value); }
        }

        private string _newDescription = "";
        public string NewDescription
        {
            get { return _newDescription; }
            set { SetProperty(ref _newDescription, value); }
        }

        private string _newNumberPomodoro = "";
        public string NewNumberPomodoro
        {
            get { return _newNumberPomodoro; }
            set { SetProperty(ref _newNumberPomodoro, value); }
        }



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
