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
    public class CustomerMapping : AuditableEntityMapping<Customer>
    {
        public override void ConfigureDerivedEntity(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.AccountId)
                .HasColumnName("ACCOUNT_ID")
                .HasColumnOrder(2);
            builder.Property(x => x.CityId)
                .HasColumnName("CITY_ID")
                .HasColumnOrder(3);
            builder.Property(x => x.IdentityNumber)
                .HasColumnType("")
               .HasColumnName("CITY_ID")
               .HasColumnOrder(3);


        }
    }
}
