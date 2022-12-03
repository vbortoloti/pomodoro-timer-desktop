
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using App.Model;

namespace App.Context
{
    public class PomodoroDbContext : DbContext
    {
        public PomodoroDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Pomodoro> Pomodoro { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pomodoro>().HasData(GetPomodoro());
            base.OnModelCreating(modelBuilder);
        }

        private static Pomodoro[] GetPomodoro()
        {
            return new Pomodoro[]
            {
                new Pomodoro
                {
                    Id = Guid.NewGuid(),
                    maxPomodoro = 2,
                    numberSmallBreakInterval = 0,
                    maxSmallBreakInterval = 2,
                    numberLongBreakInterval = 0,
                    maxLongBreakInterval = 2,
                    description = "Test Pomodoro",
                    status = "Solid_Circle",

                },
            };
        }
    }
}
