using Microsoft.EntityFrameworkCore;
using TradeAPI.Db.Entity;

namespace TradeAPI.Db;

public partial class TradeApiContext : DbContext
{
    public TradeApiContext()
    {
    }

    public TradeApiContext(DbContextOptions<TradeApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PnL> PnLs { get; set; }

    public virtual DbSet<StrategyPnL> StrategyPnLs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(LocalDB)\\MSSQLLocalDB;initial catalog=TradeApi;MultipleActiveResultSets=True;App=EntityFramework");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PnL>(entity =>
        {
            entity.HasKey(e => e.IdPnL).HasName("PK__PnL__2ACDACD85D8887BC");

            entity.ToTable("PnL");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Idstrategy).HasColumnName("IDStrategy");

            entity.HasOne(d => d.IdstrategyNavigation).WithMany(p => p.PnLs)
                .HasForeignKey(d => d.Idstrategy)
                .HasConstraintName("FK__PnL__IDStrategy__267ABA7A");
        });

        modelBuilder.Entity<StrategyPnL>(entity =>
        {
            entity.HasKey(e => e.Idstrategy).HasName("PK__Strategy__9BC5728B6BAF91A3");

            entity.ToTable("StrategyPnL");

            entity.Property(e => e.Idstrategy).HasColumnName("IDStrategy");
            entity.Property(e => e.Strategy)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
