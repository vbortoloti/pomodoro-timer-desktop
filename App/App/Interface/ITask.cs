using App.Model;
using App.Repositories;
using App.WPFModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Interface
{
    public interface ITask 
    {

        public List<PomodoroWPFModel> convertPomodoro2WPFModel(List<Pomodoro> pomodoroList);
        public Pomodoro convertWPFModel2Pomodoro(PomodoroWPFModel wpfModel);

        public List<PomodoroWPFModel> OpenPopUp(List<Pomodoro> pomodoroList, Guid Id);
        public List<PomodoroWPFModel> ClosePopUp(List<Pomodoro> pomodoroList, Guid Id);
        public void RefreshCollection(List<PomodoroWPFModel> newItemList);
        public void sumOneInSelected(object source, TimerEventArgs timer);


    }
}
