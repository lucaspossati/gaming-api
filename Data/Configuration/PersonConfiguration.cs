using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Application.Models;

namespace Data.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.FullName).HasColumnName("full_name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnName("phone_number").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Address).HasColumnName("address").HasMaxLength(200).IsRequired();
            builder.Property(x => x.CompanyId).HasColumnName("company_id").IsRequired();
        }
    }
}
