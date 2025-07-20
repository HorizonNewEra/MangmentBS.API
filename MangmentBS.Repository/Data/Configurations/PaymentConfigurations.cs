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
    public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.StartPrice).HasColumnType("decimal(18,2)");
            builder.Property(p => p.FullPrice).HasColumnType("decimal(18,2)");
            builder.Property(p => p.MonthlyPrice).HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.Building)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BuildingId);
            builder.HasOne(p => p.Flat)
                .WithOne(p=>p.Payment)
                .HasForeignKey<Flat>(p => p.PaymentId);
        }
    }
}
