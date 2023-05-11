using Microsoft.EntityFrameworkCore;
using TradeAPI.Db.Entity;

namespace TradeAPI.Db
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<PnL> Pnls { get; set; }
        public DbSet<StrategyPnL> StrategyPnls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("your_connection_string_here");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PnL>()
                .HasKey(p => p.Date);

            modelBuilder.Entity<StrategyPnL>()
                .HasKey(s => s.Id);
        }
    }
}