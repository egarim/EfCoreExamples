using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreExamples.SoftDelete
{
    public static class EntityTypeBuilderExtensions
    {
        public static void ConfigureSoftDelete(this EntityTypeBuilder b)
        {
            if (b.Metadata.ClrType.IsAssignableTo<ISoftDelete>())
            {
                b.Property(nameof(ISoftDelete.IsDeleted))
                    .IsRequired()
                    .HasDefaultValue(false)
                    .HasColumnName(nameof(ISoftDelete.IsDeleted));
            }
        }
    }
}
