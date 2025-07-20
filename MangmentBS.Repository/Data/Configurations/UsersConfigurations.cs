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
    public class UsersConfigurations: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.HasOne(u => u.Roles)
                .WithMany()
                .HasForeignKey(u => u.Role)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(u=>u.Email).IsUnique();
            builder.HasIndex(u=>u.UserName).IsUnique();

        }
    }
}
