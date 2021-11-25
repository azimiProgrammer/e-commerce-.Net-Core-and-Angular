using Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.EntityConfigurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(p => p.Id).IsRequired();

            builder.Property(p => p.Lat).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Long).HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.AppUser)
                .WithMany(a => a.Addresses)
                .HasForeignKey(p => p.AppUserId)
                .IsRequired(true);
        }
    }
}
