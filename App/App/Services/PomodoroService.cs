using App.Context;
using App.DbContexts;
using App.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class DatabasePomodoroProvider 
    {
        private readonly PomodoroDbContextFactory _dbContextFactory;

        public DatabasePomodoroProvider(PomodoroDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public List<Pomodoro> GetAllPomodoro()
        {
            using (PomodoroDbContext context = _dbContextFactory.CreateDbContext())
            {
                return context.Pomodoro.ToList();
            }
        }

        public void savePomodoro(Pomodoro pomodoro)
        {
            using (PomodoroDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.Pomodoro.Add(pomodoro);
            }
        }

        //public async Task<IEnumerable<Pomodoro>> GetAllReservations()
        //{
        //    using (PomodoroDbContext context = _dbContextFactory.CreateDbContext())
        //    {
        //        await Task.Delay(3000);

        //        IEnumerable<Pomodoro> reservationDTOs = await context.Pomodoro.ToListAsync();

        //        return reservationDTOs.Select(r => r);
        //    }
        //}

    }
}