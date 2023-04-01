using Domain.Entities;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Application
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.addressType).IsRequired();
            builder.Property(x => x.addressType).HasConversion<int>();

            // Relationships
            builder.HasOne<User>().WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}
