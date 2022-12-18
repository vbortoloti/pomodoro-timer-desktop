using App.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DbContexts
{
    public class PomodoroDbContextFactory
    {
        private readonly string _connectionString;

        public PomodoroDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PomodoroDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new PomodoroDbContext(options);
        }
    }
}