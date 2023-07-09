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
    public class AddressMapping : BaseEntityMapping<Address>
    {
        public override void ConfigureDerivedEntity(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.CityId)
                 .HasColumnName("CITY_ID")
                 .HasColumnOrder(2);
            builder.Property(x => x.Text)
                 .HasColumnName("TEXT")
                 .HasColumnType("nvarchar(max)")
                 .HasColumnOrder(3);

            builder.ToTable("ADDRESSES");

        }
    }
}
