using Microsoft.EntityFrameworkCore;
using Senhoritah.API.Model;

namespace Senhoritah.API.Context
{
    public class context : DbContext
    {
        public context(DbContextOptions<context> options) : base(options) { }
        public DbSet<ConfigModel> Config { get; set; }
        public DbSet<ClientsModel> Clients { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<RecipesModel> Recipes { get; set; }
        public DbSet<ProductsRecipeModel> Recipe_products { get; set; }
        public DbSet<BillsModel> Bills { get; set; }
        public DbSet<UnitsModel> Units { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>()
            .HasOne(p => p.Unit)
            .WithMany()
            .HasForeignKey(p => p.IdUnit);
            modelBuilder.Entity<ConfigModel>().HasData(new ConfigModel
            {
                Id = 1,
                mao_de_obra = 10,
                energia_agua = 20,
                extra = 1,
                calculo_mercado = 2
            });

        }
    }
}
