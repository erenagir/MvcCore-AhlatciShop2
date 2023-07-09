using Ahlatci.Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Persistence.Mapping
{
    public class CategoryMapping : AuditableEntityMapping<Category>
    {
        public override void ConfigureDerivedEntity(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(100)")
                .HasColumnName("NAME")
                .HasColumnOrder(2);

            
        }
    }
}
