
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //builder.HasKey(x => x.Id);//zorunlu değil geleneğe uyduk

            builder.Property(x => x.CategoryName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
