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
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
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
                    //numberSmallBreakInterval = 0,
                    //maxSmallBreakInterval = 2,
                    //numberLongBreakInterval = 0,
                    //maxLongBreakInterval = 2,
                    description = "Test Pomodoro",

                },
            };
        }
    }
}
