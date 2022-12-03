using App.DbContexts;
using App.Model;
using App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
