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
    public class FlatContractImagesConfigurations: IEntityTypeConfiguration<FlatContractImages>
    {
        public void Configure(EntityTypeBuilder<FlatContractImages> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
