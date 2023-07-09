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
    public class ProductImageMapping : AuditableEntityMapping<ProductImage>
    {
        public override void ConfigureDerivedEntity(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(x => x.ProductId)
                .HasColumnName("PRODUCT_ID")
                .HasColumnOrder(2);

            builder.Property(x => x.Path)
                .HasColumnName("PATH")
                .HasColumnType("nvarchar(150)")
                .HasColumnOrder(3);

            builder.Property(x => x.Order)
               .HasColumnName("ORDER")
               .HasColumnOrder(4);

            builder.Property(x => x.Order)
               .HasColumnName("ORDER")
               .HasColumnOrder(5);

            builder.Property(x => x.IsThumbnail)
                .HasColumnName("IS_THUMBNAIL")
                .HasColumnOrder(6);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductImages)
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("PRODUCT_IMAGE_PRODUCT_PRODUCT_ID");

            builder.ToTable("PRODUCT_IMAGES");
        }
    }
}
