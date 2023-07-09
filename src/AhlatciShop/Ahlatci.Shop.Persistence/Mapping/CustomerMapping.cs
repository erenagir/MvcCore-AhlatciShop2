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
                .HasColumnType("nchar(11)")
               .HasColumnName("IDENTITYNUMBER")
               .IsRequired()
               .HasColumnOrder(4);
            builder.Property(x => x.Name)
              .HasColumnType("nvarchar(30)")
             .HasColumnName("NAME")
             .IsRequired()
             .HasColumnOrder(5); 
            builder.Property(x => x.Surname)
              .HasColumnType("nvarchar(30)")
             .HasColumnName("SURNAME")
             .IsRequired()
             .HasColumnOrder(6);

            builder.Property(x => x.Email)
              .HasColumnType("nvarchar(150)")
             .HasColumnName("EMAİL")
             .IsRequired()
             .HasColumnOrder(7);
             builder.Property(x => x.Phone)
              .HasColumnType("nchar(13)")
             .HasColumnName("PHONE")
             .IsRequired()
             .HasColumnOrder(8);
            builder.Property(x => x.Birthdate)
           .HasColumnName("BIRTHDATE")
            .IsRequired()
            .HasColumnOrder(9);

            builder.Property(x => x.Gender)
                .HasColumnName("GENDER")
                .IsRequired()
                .HasColumnOrder(10);
            builder.HasOne(x => x.Account)
               .WithOne(x => x.Customer)
               .HasForeignKey<Customer>(x => x.AccountId)
               .HasConstraintName("CUSTOMER_ACCOUNT_ACCOUNT_ID");
            builder.HasOne(x => x.City)
               .WithMany(x => x.Customers)
               .HasForeignKey(x => x.CityId)
               .HasConstraintName("CUSTOMER_CITY_CITY_ID");

            builder.ToTable("CUSTOMERS");

        }
    }
}
