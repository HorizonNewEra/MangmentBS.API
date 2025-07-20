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
    public class LandContractImagesConfigurations: IEntityTypeConfiguration<BuildingContractImages>
    {
        public void Configure(EntityTypeBuilder<BuildingContractImages> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
