using EfCoreExamples.ChangeNotification.Explicit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreExamples.ChangeNotification.Attach
{
    public class ContextAttachEntities : DbContext
    {
        //public static readonly ILoggerFactory MyLoggerFactory
        //    = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //HACK log to any action that accepts a string message as parameter
                .LogTo((message) =>
                {
                    Debug.WriteLine(message);
                })
                //HACK tracking proxies required to attach entities https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies
                .UseChangeTrackingProxies()
                .UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=EfCoreExamplesContextAttachEntities;Trusted_Connection=True;");
        }
        public DbSet<SimplePerson> SimplePersons { get; set; }

        

    }
}
