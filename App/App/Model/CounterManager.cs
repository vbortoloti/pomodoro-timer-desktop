using App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public static class CounterManager
    {
        public static Dictionary<string, Counter> Counters = new Dictionary<string, Counter> {
            {"work",  new Counter(25) },
            {"short", new Counter(5) },
            {"long",  new Counter(15) },
        };
      
        public static string ActiveCounter = "work";

        public static CountDownViewModel ActiveView;
        public static CountDownViewModel DeactiveView;

        public static event EventHandler ActiveCounterChange;

        public static int ViewCount = 0;
        public static bool AllViewIsInitialzed = false;
        public static void SwitchView()
        {
            if (AllViewIsInitialzed)
            {
                SetAsActive(DeactiveView);
                return;
            }
            if(ViewCount >= 3)
            {
                AllViewIsInitialzed = true;
            }
        }
        public static void SetAsActive(CountDownViewModel view)
        {
            if(!AllViewIsInitialzed) ViewCount++;
            DeactiveView = ActiveView;
            ActiveView = view;
            Counter.SetView(ActiveView);
        }

        public static void Play()
        {
            Counter activeCouter = GetActiveCounter();
            activeCouter.Play();
        }
        
        public static void Pause()
        {
            Counter activeCouter = GetActiveCounter();
            activeCouter.Pause();
        }

        public static void Reset()
        {
            Counter activeCouter = GetActiveCounter();
            activeCouter.Reset();
        }

       public static Counter GetActiveCounter()
        {
            Counter activeCounter;
            Counters.TryGetValue(ActiveCounter, out activeCounter);
            return activeCounter;
        }

        public static void SetActiveCounter(string activeCounter)
        {
            Counter activeCouter = GetActiveCounter();
            activeCouter.Reset();
            ActiveCounter = activeCounter;
            OnActiveCounterChange();
        }

        public static void OnActiveCounterChange()
        {
            if (ActiveCounterChange != null)
            { 
                ActiveCounterChange(null, EventArgs.Empty);
            }
        }
    }
}
