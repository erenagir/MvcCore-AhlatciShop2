using Ahlatci.Shop.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Persistence.Mapping
{
    public abstract class AuditableEntityMapping<T> : IEntityTypeConfiguration<T> where T : AuditableEntity
    {
        public abstract void ConfigureDerivedEntity(EntityTypeBuilder<T> builder);
        public void Configure(EntityTypeBuilder<T> builder)
        {


            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                 .HasColumnName("ID")
                 .HasColumnOrder(1);


            ConfigureDerivedEntity(builder);
            builder.Property(x => x.CreateDate)
                .HasColumnName("CREATED_DATE")
                .HasColumnOrder(26);
            builder.Property(x => x.CreatedBy)
               .HasColumnName("CREATED_BY")
               .HasColumnOrder(27);
            builder.Property(x => x.modifiedBy)
               .HasColumnName("MODİFİED_BY")
               .HasColumnOrder(28);
            builder.Property(x => x.ModifiedDate)
               .HasColumnName("MODİFİED_DATE")
               .HasColumnOrder(29);

            builder.Property(x => x.IsDeleted)
                .HasColumnName("IS_DELETED")
                .HasDefaultValueSql("0")
                .HasColumnOrder(30);

        }
       
    }
}
