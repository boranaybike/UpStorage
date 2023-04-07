﻿using Domain.Entities;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Application
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Name
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(150);

            // District
            builder.Property(x => x.District).IsRequired();
            builder.Property(x => x.AddressLine2).HasMaxLength(100);

            // PostCode
            builder.Property(x => x.PostCode).IsRequired();

            // AddressLine1
            builder.Property(x => x.AddressLine1).IsRequired();
            builder.Property(x => x.AddressLine2).HasMaxLength(150);

            // AddressLine2
            builder.Property(x => x.AddressLine2).IsRequired(false);
            builder.Property(x => x.AddressLine2).HasMaxLength(150);


            // AddresssType
            builder.Property(x => x.addressType).IsRequired();
            builder.Property(x => x.addressType).HasConversion<int>();

            /* Common Fields */

            // CreatedByUserId
            builder.Property(x => x.CreatedByUserId).IsRequired(false);
            builder.Property(x => x.CreatedByUserId).HasMaxLength(100);

            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();

            // ModifiedByUserId
            builder.Property(x => x.ModifiedByUserId).IsRequired(false);
            builder.Property(x => x.ModifiedByUserId).HasMaxLength(100);

            // LastModifiedOn
            builder.Property(x => x.ModifiedOn).IsRequired(false);

            // DeletedByUserId
            builder.Property(x => x.DeletedByUserId).IsRequired(false);
            builder.Property(x => x.DeletedByUserId).HasMaxLength(100);

            // DeletedOn
            builder.Property(x => x.DeletedOn).IsRequired(false);

            // IsDeleted
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValueSql("0");

            // Relationships
            builder.HasOne<User>().WithMany()
                .HasForeignKey(x => x.UserId);

            builder.ToTable("Addresses");

        }
    }
}
