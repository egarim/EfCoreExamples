using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreExamples.Loggers.LogInterceptor
{
    public class ContextWithInterceptors : DbContext
    {
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .AddInterceptors(new QueryInterceptor())
                .UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=EfCoreExamplesLogInterceptor;Trusted_Connection=True;");
        }
        public DbSet<SimplePerson> SimplePersons { get; set; }

    }
}
