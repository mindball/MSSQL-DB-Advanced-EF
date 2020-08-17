namespace ProductShop.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;


    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FirstName)
                .IsRequired(false)
                .IsUnicode(false);

            builder.Property(a => a.LastName)
                .IsRequired(true)
                .IsUnicode(false);

            builder.Property(a => a.Age)
                .IsRequired(false);               

        }
    }
}
