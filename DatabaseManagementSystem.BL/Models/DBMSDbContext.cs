using Microsoft.EntityFrameworkCore;

namespace DatabaseManagementSystem.WebApi.Models
{
    public class DBMSDbContext : DbContext
    {
        public virtual DbSet<Database> Databases { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public  virtual DbSet<Row> Rows { get; set; }
        public virtual DbSet<Field> Fields { get; set; }

        public DBMSDbContext(DbContextOptions<DBMSDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Database>(entity =>
            {
                entity.HasMany(d => d.Tables)
                    .WithOne(t => t.Database)
                    .HasForeignKey(t => t.DatabaseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Table>(entity =>
            {
                entity.HasMany(t => t.Fields)
                    .WithOne(f => f.Table)
                    .HasForeignKey(f => f.TableId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(t => t.Rows)
                    .WithOne(r => r.Table)
                    .HasForeignKey(r => r.TableId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
