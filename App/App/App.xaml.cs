using WpfApp.Views;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using App.Views;
using Prism.Modularity;
using Microsoft.Extensions.DependencyInjection;
using App.Model;
using Microsoft.EntityFrameworkCore;
using Example;
using App.Context;
using App.DbContexts;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            var pomodoroFactory = new PomodoroDbContextFactory("Data source = Pomodoro.db");
            using (PomodoroDbContext dbContext = pomodoroFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.Register<Services.ICustomerStore, Services.DbCustomerStore>();

            containerRegistry.RegisterForNavigation<TaskView>();
            containerRegistry.RegisterForNavigation<TimerView>();
            containerRegistry.RegisterForNavigation<MusicPlayerView>();
            containerRegistry.RegisterForNavigation<MaximazedView>("MaximazedView");
            containerRegistry.RegisterForNavigation<MinimazedView>("MinimazedView");
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<ShellWindow>();
            moduleCatalog.AddModule<MaximazedView>();
        }
    }
}
