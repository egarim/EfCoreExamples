using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreExamples.ValueConverters.ConvertToStorageAndQuery
{
    public class ContextWithConverter : DbContext
    {
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //HACK there are several type of loggers, this is just one of them, check the nugets that start with the name Microsoft.Extensions.Logging
            //For example, Microsoft.Extensions.Logging.Console and Microsoft.Extensions.Logging.Debug
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    //HACK extra filters https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/simple-logging#custom-filters
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name
                        && level == LogLevel.Information)
                    .AddDebug();
            });

            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder
                .UseLoggerFactory(loggerFactory)
                .UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=EfContextWithConverter;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<SimplePerson>()
                .Property(e => e.Status)
                .HasConversion(
                    v => WriteData(v),
                    v => ReadData(v));
        }
        string WriteData(Status status)
        {
            return status.ToString();
        }
        Status ReadData(string status)
        {
           return  (Status)Enum.Parse(typeof(Status), status);
        }
        public DbSet<SimplePerson> SimplePersons { get; set; }

    }
}
