using API_BurgerManiaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace API_BurgerManiaBackend.Data
{
    public class BurgerManiaDbContext: DbContext
    {
        public DbSet<UserData> UserDatas {  get; set; }
        public DbSet<BurgerAvailabilityData> BurgerDatas { get; set; }
        public DbSet<OrdersData> OrdersDatas { get; set; }
        public DbSet<BurgerOrderData> BurgerOrderDatas { get; set; }
        public BurgerManiaDbContext(DbContextOptions<BurgerManiaDbContext> options):base(options)
        {

        }

        // For Running tests
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(LocalDB)\\MsSqlLocalDb;Database=BurgerManiaDb;Trusted_connection=True;MultipleActiveResultSets=True;Trust Server Certificate=True;");

            }
        }
    }
}
