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
    public class CommentMapping : AuditableEntityMapping<Comment>
    {
        public override void ConfigureDerivedEntity(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.ProductId)
                 .HasColumnName("PRODUCT_ID")
                 .HasColumnOrder(2);
            builder.Property(x => x.Customerıd)
                 .HasColumnName("CUSTOMER_ID")
                 .HasColumnOrder(3);
            builder.Property(x => x.Detail)
                .IsRequired()
                .HasColumnType("nvarchar(max)")
                 .HasColumnName("DETAIL")
                 .HasColumnOrder(4);
            builder.Property(x => x.LikeCount)
                 .HasColumnName("LIKE_COUNT")
                 .HasColumnOrder(5);
            builder.Property(x => x.DislikeCount)
                 .HasColumnName("DİSLIKE_COUNT")
                 .HasColumnOrder(6);
            builder.Property(x => x.ISApproved)
                 .HasColumnName("IS_APPROVED")
                 .HasDefaultValueSql("0")
                 .HasColumnOrder(7);
            builder.HasOne(x => x.Product)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("COMMET_PRODUCT_PRODUCT_ID");

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.Customerıd)
                .HasConstraintName("COMMET_CUSTOMER_CUSTOMER_ID");









        }
    }
}
