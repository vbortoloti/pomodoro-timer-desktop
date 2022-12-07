using App.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace App.Model
{
    public class TimerEventArgs : EventArgs
    {
        public string ActiveCounter { get; set; }
        public int CounterDurationInMinutes { get; set; }
        public TimerEventArgs(string activeCounter, int counterDurationInMinutes)
        {
            ActiveCounter = activeCounter;
            CounterDurationInMinutes = counterDurationInMinutes;   
        }
    }
    public static class CounterManager
    {
        public static int workDuration = 25, shortDuration = 5, longDuration = 15;
        public static int workCount = 0, shortCount = 0, longInterval = 4;

        public static string ActiveCounter = "work";

        public static CountDownViewModel ActiveView;
        public static CountDownViewModel DeactiveView;

        public static event EventHandler ActiveCounterChange;
        public static event EventHandler<TimerEventArgs> CounterFinished;

        public static int ViewCount = 0;
        public static bool AllViewIsInitialzed = false;

        public static Dictionary<string, Counter> Counters = new Dictionary<string, Counter> {
            {"work",  new Counter(workDuration) },
            {"short", new Counter(shortDuration) },
            {"long",  new Counter(longDuration) },
        };

        public static void UpdateCountersDuration()
        {
            Counter counter;
            Counters.TryGetValue("work", out counter);
            counter.countDuration = workDuration;
            Counters.TryGetValue("short", out counter);
            counter.countDuration = shortDuration;
            Counters.TryGetValue("long", out counter);
            counter.countDuration = longDuration;
            var time = new DateTime().AddMinutes(CounterManager.GetActiveCounter().countDuration);
            ActiveView.CountText = time.ToString("mm\\:ss");
        }
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
        public static void OnTimerEnd(object source, EventArgs e)
        {
            ActiveView.CountText = "ADASDADS";
            OnCounterFinished();
        }

        private static void OnCounterFinished()
        {
            PlaySound();
            string? nextCounter;
            if (ActiveCounter == "work")
            {
                workCount++;
                if (longInterval == workCount) nextCounter = "long";
                else nextCounter = "short";
            }
            else if (ActiveCounter == "short")
            {
                shortCount++;
                nextCounter = "work";
            }
            else
            {
                longInterval += longInterval;
                nextCounter = "work";
            }
            TimerEventArgs args = new TimerEventArgs(nextCounter, GetActiveCounter().countDuration);
            CounterFinished(null, args);
        }

        private static void PlaySound() {
            var sound = Properties.Resources.done;
            SoundPlayer player = new SoundPlayer(sound);
            player.Play();
        }
  
    }
}
