using App.Context;
using App.DbContexts;
using App.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

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

        public Pomodoro FindByGuid(Guid guid)
        {
            using (PomodoroDbContext context = _dbContextFactory.CreateDbContext())
            {
                var result = context.Pomodoro.ToList()
                    .Find((obj) => obj.Id == guid);
                return result != null ? result : null;
            }
        }

        public void updatePomodoroStatus(Guid id, bool selected)
        {
            using (PomodoroDbContext context = _dbContextFactory.CreateDbContext())
            {
                var result = context.Pomodoro
                    .Where(dbPomodoro => dbPomodoro.Id == id).FirstOrDefault();
                if (result != null)
                {
                    result.selected = selected;
                    result.status= "Solid_Check";
                    context.Pomodoro.Attach(result);
                    context.Entry(result).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void updatePomodoro(Guid id, Pomodoro Pomodoro)
        {
            using (PomodoroDbContext context = _dbContextFactory.CreateDbContext())
            {
                var result = context.Pomodoro
                    .Where(dbPomodoro => dbPomodoro.Id == id).FirstOrDefault();
                if (result != null)
                {
                    result.maxPomodoro = Pomodoro.maxPomodoro;
                    result.numberPomodoro = Pomodoro.numberPomodoro;
                    result.numberSmallBreakInterval = Pomodoro.numberSmallBreakInterval;
                    result.maxSmallBreakInterval = Pomodoro.maxSmallBreakInterval;
                    result.numberLongBreakInterval = Pomodoro.numberLongBreakInterval;
                    result.maxLongBreakInterval = Pomodoro.maxLongBreakInterval;
                    result.description = Pomodoro.description;
                    result.status = Pomodoro.status;
                    result.selected = Pomodoro.selected;
                    context.Pomodoro.Attach(result);
                    context.Entry(result).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }


        public void deselectPomodoros()
        {
            using (PomodoroDbContext context = _dbContextFactory.CreateDbContext())
            {
                var result = context.Pomodoro.ToList();

                if (result != null)
                {
                    result.ForEach(pomodoros => pomodoros.selected = false);
                    context.SaveChanges();
                }
            }
        }

        public void selectFirst()
        {
            using (PomodoroDbContext context = _dbContextFactory.CreateDbContext())
            {
                var result = context.Pomodoro.ToList();

                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        result[0].selected = true;
                    }
                    context.SaveChanges();
                }
            }

        }

        public void savePomodoro(Pomodoro pomodoro)
        {
            using (PomodoroDbContext context = _dbContextFactory.CreateDbContext())
            {
               context.Pomodoro.Add(pomodoro);
               context.SaveChanges();
            }
        }

        public void deletePomodoro(Guid Id)
        {
            using (PomodoroDbContext context = _dbContextFactory.CreateDbContext())
            {
                var remPomod = context.Pomodoro.Where(pomdoro => pomdoro.Id == Id).First();
                context.Pomodoro.Remove(remPomod);
                context.SaveChanges();
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