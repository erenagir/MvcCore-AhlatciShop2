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
    internal class CityMapping : BaseEntityMapping<City>
    {
        public override void ConfigureDerivedEntity(EntityTypeBuilder<City> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasColumnType("nvarchar(20)")
                .HasColumnOrder(2)
                .IsRequired();
            builder.ToTable("CITIES");

        }
    }
}
