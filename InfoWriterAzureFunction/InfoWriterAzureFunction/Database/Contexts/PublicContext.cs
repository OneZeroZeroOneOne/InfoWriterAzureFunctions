
using InfoWriterAzureFunction.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace InfoWriterAzureFunction.Database.Context
{
    public partial class PublicContext : DbContext
    {
        public string _connString;
        public PublicContext(string connString)
        {
            _connString = connString;
        }

        public PublicContext(DbContextOptions<PublicContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connString);
            }
        }

        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<StatusStorage> StatusStorage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('device_id_seq'::regclass)");

                entity.Property(e => e.ComputerName).IsRequired();

                entity.Property(e => e.DotNewVersion).IsRequired();

                entity.Property(e => e.Osname)
                    .IsRequired()
                    .HasColumnName("OSName");

                entity.Property(e => e.TimeZone).IsRequired();
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Tittle).IsRequired();
            });

            modelBuilder.Entity<StatusStorage>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('status_storage_id_seq'::regclass)");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.StatusStorage)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StatusStorage_DeviceId_fkey");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.StatusStorage)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StatusStorage_StatusId_fkey");
            });

            modelBuilder.HasSequence("device_id_seq");

            modelBuilder.HasSequence("status_storage_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
