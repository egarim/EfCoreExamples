using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace EfCoreExamples.SoftDelete.WithEntityConfiguration
{
    public class SimplePersonConfiguration : IEntityTypeConfiguration<SimplePerson>
    {
        public void Configure(EntityTypeBuilder<SimplePerson> builder)
        {

            builder.Property<bool>("IsDeleted");
            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

        }
    }
}
