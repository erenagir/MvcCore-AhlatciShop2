using Ahlatci.Shop.Domain.Common;
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
    public class AccountMapping : BaseEntityMapping<Account>
    {
        public override void ConfigureDerivedEntity(EntityTypeBuilder<Account> builder)
        {
            builder.Property(x => x.CustomerId)
                 .HasColumnName("CUSTOMER_ID")
                 .HasColumnOrder(2);
            builder.Property(x => x.Username)
                .HasColumnType("nvarchar(10)")
                 .HasColumnName("USERNAME")
                 .HasColumnOrder(3);
            builder.Property(x => x.Password)
                .HasColumnType("nvarchar(100)")
                 .HasColumnName("PASSWORD")
                 .HasColumnOrder(4);
            builder.Property(x => x.LastUserIp)
                 .HasColumnName("LAST_USER_IP")
                 .IsRequired(false)
                 .HasColumnOrder(6);
            builder.Property(x => x.LastLoginDate)
                .HasColumnName("LAST_LOGİN_DATE")
                .IsRequired(false)
                .HasColumnOrder(5);

            builder.Property(x => x.Role)
                .HasColumnName("ROlE_ID")
                .HasColumnOrder(6);

            builder.HasOne(x => x.Customer)
                .WithOne(x => x.Account)
                .HasForeignKey<Account>(x => x.CustomerId);
            builder.ToTable("ACCOUNTS");
        }
    }
}
