using App.DbContexts;
using App.Model;
using App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace App.Repositories
{
    class PomodoroRepository
    {
        public static List<Pomodoro>  GetPomodoros()
        {
            var pomodoroFactory = new PomodoroDbContextFactory("Data source = Pomodoro.db");
            var provider = new DatabasePomodoroProvider(pomodoroFactory);
            return provider.GetAllPomodoro();

        }

        public static void SavePomodoros(Pomodoro pomodoro)
        {
            var pomodoroFactory = new PomodoroDbContextFactory("Data source = Pomodoro.db");
            var provider = new DatabasePomodoroProvider(pomodoroFactory);
            provider.savePomodoro(pomodoro);

        }

        public static void UpdatePomodoroStatus(Guid id, bool selected)
        {
            var pomodoroFactory = new PomodoroDbContextFactory("Data source = Pomodoro.db");
            var provider = new DatabasePomodoroProvider(pomodoroFactory);
            provider.updatePomodoroStatus(id, selected);
            
        }

        public static void UpdatePomodoro(Guid id, Pomodoro pomodoro)
        {
            var pomodoroFactory = new PomodoroDbContextFactory("Data source = Pomodoro.db");
            var provider = new DatabasePomodoroProvider(pomodoroFactory);
            provider.updatePomodoro(id, pomodoro);

        }
        

        public static void DeselectPomodoros()
        {
            var pomodoroFactory = new PomodoroDbContextFactory("Data source = Pomodoro.db");
            var provider = new DatabasePomodoroProvider(pomodoroFactory);
            provider.deselectPomodoros();

        }

        public static void DeletePomodoro(Guid Id)
        {
            var pomodoroFactory = new PomodoroDbContextFactory("Data source = Pomodoro.db");
            var provider = new DatabasePomodoroProvider(pomodoroFactory);
            provider.deletePomodoro(Id);

        }
    }
}
