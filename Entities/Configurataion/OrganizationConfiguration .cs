using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasData
            (
                new Organization
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    OrgName = "xyz org",
                    Address = "583 Wall Dr. Gwynn Oak, MD 21207",
                    Country = "USA"
                },
                new Organization
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    OrgName = "lmnop org",
                    Address = "3036 English Creek Ave, EHT, NJ 08234",
                    Country = "USA"
                }
            );
        }
    }
}
