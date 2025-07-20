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
    public class FlatConfigurations: IEntityTypeConfiguration<Flat>
    {
        public void Configure(EntityTypeBuilder<Flat> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(f => f.StanderdPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(f => f.SellingPrice).HasColumnType("decimal(18,2)");
            builder.Property(f => f.Size).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasOne(f => f.Building)
                .WithMany(ci => ci.Flats)
                .HasForeignKey(ci => ci.BuildingId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(f => f.FlatContractImages)
                .WithOne(fci => fci.Flat)
                .HasForeignKey(fci => fci.FlatId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(f => f.Client)
                .WithMany(c => c.Flats)
                .HasForeignKey(f => f.ClientId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Property(p => p.IsSold).HasDefaultValue("false");
        }
    }
}
