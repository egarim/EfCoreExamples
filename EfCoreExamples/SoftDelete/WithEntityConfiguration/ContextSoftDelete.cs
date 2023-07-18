using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreExamples.SoftDelete
{
    public class ContextSoftDelete : DbContext
    {
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }
        protected virtual void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {    
               
                if (entry.Members.Any(x => x.Metadata.Name == "IsDeleted"))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["IsDeleted"] = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["IsDeleted"] = true;
                            break;
                    }
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //HACK log to any action that accepts a string message as parameter

                .LogTo((message) =>
                {
                    Debug.WriteLine(message);
                })
                .UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=EfCoreExamplesContextSoftDelete;Trusted_Connection=True;");
        }
        public DbSet<SimplePerson> SimplePersons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //HACK not needed if you use configuration class

            //modelBuilder.Entity<SimplePerson>()
            //    .HasQueryFilter(user => !user.IsDeleted);

            modelBuilder.ApplyConfiguration(new SimplePersonConfiguration());



        }

    }
}
