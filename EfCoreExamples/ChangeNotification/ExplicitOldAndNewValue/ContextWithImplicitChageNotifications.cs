using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreExamples.ChangeNotification.ExplicitOldAndNewValue
{
    public class ContextWithExplicitChangeNotifications : DbContext
    {
        //public static readonly ILoggerFactory MyLoggerFactory
        //    = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //HACK tracking proxies not required for custom change notifications
                //.UseChangeTrackingProxies()
                .UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=EfCoreExamplesContextWithExplicitChangeNotifications;Trusted_Connection=True;");
        }
       
        public DbSet<SimplePersonWithCustomNotificationTrigger> SimplePersonsWithNotificationTrigger { get; set; }
    }
}
