using MangmentBS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Repository.Data.Configurations
{
    public class InstallmentConfigurations : IEntityTypeConfiguration<Installment>
    {
        public void Configure(EntityTypeBuilder<Installment> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(i => i.PaidDate).IsRequired(false);
            builder.HasOne(i => i.Payment)
                .WithMany(p => p.Installments)
                .HasForeignKey(i => i.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
