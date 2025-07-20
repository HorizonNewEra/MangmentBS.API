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
    public class BuildingConfigurations : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.Property(b => b.Size).HasColumnType("decimal(18,2)");
            builder.Property(b => b.LandPrice).HasColumnType("decimal(18,2)");
            builder.Property(b => b.ConstractionFees).HasColumnType("decimal(18,2)");
            builder.HasMany(l => l.BuildingContractImages)
                .WithOne(i => i.Building)
                .HasForeignKey(i => i.BuildingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
