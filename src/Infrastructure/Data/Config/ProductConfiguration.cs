using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //builder.ToTable("Urunler"); tablo ismi değiştirmek için
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Author)
                .WithMany()
                .HasForeignKey(x=>x.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);//bağlıları silinsin

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);//bağlıları silme hata fırlatıcak

        }
    }
}
