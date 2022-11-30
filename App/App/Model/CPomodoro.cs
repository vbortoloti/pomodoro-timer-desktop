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

namespace App.Model
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Pomodoro> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pomodoro>().HasData(GetUsers());
            base.OnModelCreating(modelBuilder);
        }

        private static Pomodoro[] GetUsers()
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

                },
            };
        }
    }
}
