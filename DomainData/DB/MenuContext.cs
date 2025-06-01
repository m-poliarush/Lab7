using MenuManager.DB.Models;
using Microsoft.EntityFrameworkCore;
 

namespace MenuManager.DB
{
    public class MenuContext : DbContext
    {

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<ComplexDish> ComplexDishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DailyMenu> DailyMenus {  get; set; } 
        public string DbPath { get; }
        public MenuContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Host=localhost;Port=5432;Database=Lab5DB;Username=postgres;Password=12345");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseMenuItem>()
                .HasDiscriminator<string>("ItemType")
                .HasValue<Dish>("Dish")
                .HasValue<ComplexDish>("ComplexDish");
            modelBuilder.Entity<DailyMenu>()
        .HasMany(dm => dm.Dishes)
        .WithMany(bmi => bmi.menus)
        .UsingEntity(j => j.ToTable("BaseMenuItemDailyMenu"));


            base.OnModelCreating(modelBuilder);
        }


    }
}
