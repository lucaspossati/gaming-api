using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Application.Models;

namespace Data.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.RegistrationDate).HasColumnName("registration_date").IsRequired();
        }
    }
}
