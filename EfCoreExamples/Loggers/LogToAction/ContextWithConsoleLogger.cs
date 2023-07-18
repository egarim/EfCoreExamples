using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreExamples.Loggers.LogToAction
{
    public class ContextWithConsoleLogger : DbContext
    {
        //public static readonly ILoggerFactory MyLoggerFactory
        //    = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //.UseLoggerFactory(MyLoggerFactory)
                .LogTo((message) => { Debug.WriteLine(message); })
                .UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=EfCoreExamplesEFLogging;Trusted_Connection=True;");
        }
        public DbSet<SimplePerson> SimplePersons { get; set; }

    }
}
