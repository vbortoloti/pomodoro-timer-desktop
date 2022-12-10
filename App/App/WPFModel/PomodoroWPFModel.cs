using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.WPFModel
{
    public class PomodoroWPFModel : INotifyPropertyChanged
    {

        //public string FirstName { get; set; }
        private string _Id;
        public string Id
        {
            get { return _Id; }
            set
            {
                _Id = value.ToString();
                OnPropertyChanged("Id");
            }
        }
        private int _numberPomodoro { get; set; }
        public int NumberPomodoro
        {
            get { return _numberPomodoro; }
            set
            {
                _numberPomodoro = value;
                OnPropertyChanged("NumberPomodoro");
            }
        }
        private int _maxPomodoro { get; set; }
        public int MaxPomodoro
        {
            get { return _maxPomodoro; }
            set
            {
                _maxPomodoro = value;
                OnPropertyChanged("MaxPomodoro");
            }
        }
        //private int _numberSmallBreakInterval { get; set; }
        //public int NumberSmallBreakInterval
        //{
        //    get { return _numberSmallBreakInterval; }
        //    set
        //    {
        //        _numberSmallBreakInterval = value;
        //        OnPropertyChanged("NumberSmallBreakInterval ");
        //    }
        //}
        //private int _maxSmallBreakInterval { get; set; }
        //public int MaxSmallBreakInterval
        //{
        //    get { return _maxSmallBreakInterval; }
        //    set
        //    {
        //        _maxSmallBreakInterval = value;
        //        OnPropertyChanged("MaxSmallBreakInterval");
        //    }
        //}
        //private int _numberLongBreakInterval { get; set; }
        //public int NumberLongBreakInterval
        //{
        //    get { return _numberLongBreakInterval; }
        //    set
        //    {
        //        _numberLongBreakInterval = value;
        //        OnPropertyChanged("NumberLongBreakInterval ");
        //    }
        //}
        //private int _maxLongBreakInterval { get; set; }
        //public int MaxLongBreakInterval
        //{
        //    get { return _maxLongBreakInterval; }
        //    set
        //    {
        //        _maxLongBreakInterval = value;
        //        OnPropertyChanged("MaxLongBreakInterval ");
        //    }
        //}
        private string _description { get; set; } = "";
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description ");
            }
        }
        private string _status { get; set; } = "";
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
        private bool _selected { get; set; } = false;
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged("selected");
            }
        }

        private bool _popUpShowState { get; set; } = false;
        public bool PopUpShowState
        {
            get { return _popUpShowState; }
            set
            {
                _popUpShowState = value;
                OnPropertyChanged("PopUpShowState");
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }




    }
}
