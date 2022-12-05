using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class Pomodoro
    {
        public Guid Id { get; set; }

        public int    numberPomodoro{ get; set; }
        public int    maxPomodoro { get; set; }
        public int    numberSmallBreakInterval { get; set; }
        public int    maxSmallBreakInterval { get; set; }
        public int    numberLongBreakInterval { get; set; }
        public int    maxLongBreakInterval { get; set; }
        public string description { get; set; } = "";
        public string status { get; set; } = "";
        public bool selected { get; set; } = false;

    }
}