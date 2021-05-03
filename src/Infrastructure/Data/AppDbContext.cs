using ApplicationCore.Entities;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //burada fluent api ile de data annotaionları yapabilirdik fakat. Biz burda ınfrastructure içindeki ayarları getirdik.Yani infrastructure dll'i içindeki. Oda Config  diye bir klasör açıyoruz.

            //2. Yol
            //modelBuilder.ApplyConfiguration(new AuthorConfiguration()); //hepsini böyle tek tek yapmak yerrine yukarıdaki gibi tek bir komut  ile tüm configurationları hallettik.
        }
    }
}
