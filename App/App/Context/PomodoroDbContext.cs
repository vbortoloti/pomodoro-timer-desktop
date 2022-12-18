
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
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Pomodoro> Pomodoro { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pomodoro>().HasData(GetPomodoro());
            base.OnModelCreating(modelBuilder);
        }

        //typeof stauts = Solid_Circle | Solid_Check | Regular_Circle
        private static Pomodoro[] GetPomodoro()
        {
            return new Pomodoro[]
            {
                new Pomodoro
                {
                    Id = Guid.NewGuid(),
                    maxPomodoro = 2,
                    //numberSmallBreakInterval = 0,
                    //maxSmallBreakInterval = 2,
                    //numberLongBreakInterval = 0,
                    //maxLongBreakInterval = 2,
                    description = "Test Pomodoro",
                    status = "Solid_Circle",
                    selected = false,

                },
                new Pomodoro
                {
                    Id = Guid.NewGuid(),
                    maxPomodoro = 4,
                    //numberSmallBreakInterval = 1,
                    //maxSmallBreakInterval = 3,
                    //numberLongBreakInterval = 1,
                    //maxLongBreakInterval = 1,
                    description = "Test Pomodoro 2",
                    status = "Regular_Circle",
                    selected = false,

                },
                new Pomodoro
                {
                    Id = Guid.NewGuid(),
                    maxPomodoro = 5,
                    //numberSmallBreakInterval = 5,
                    //maxSmallBreakInterval = 5,
                    //numberLongBreakInterval = 5,
                    //maxLongBreakInterval = 5,
                    description = "Test Pomodoro 3",
                    status = "Solid_Check",
                    selected = false,

                },

            };
        }
    }
}
