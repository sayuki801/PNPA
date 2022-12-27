using Microsoft.EntityFrameworkCore;
using PNPA.Data.Entities;

namespace PNPA.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=bagsay-lahi;Username=postgres;Password=Admin");

        #region DbSet
        public virtual DbSet<User> Users { get; set; } = default!;
        public virtual DbSet<PersonsInfo> UsersInfo { get; set; } = default!;
        public virtual DbSet<Role> Roles { get; set; } = default!;
        public virtual DbSet<Rank> Ranks { get; set; } = default!;

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp").HasDefaultSchema("bagsay-lahi");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.ToTable("Users");

                entity.Property(e => e.ID)
                .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasIndex(e => e.ID)
                .HasDatabaseName("pki_usersid");

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.Users);
            });

            modelBuilder.Entity<PersonsInfo>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.ToTable("PersonsInfo");

                entity.Property(e => e.ID).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Unit)
                   .HasMaxLength(255);

                entity.Property(e => e.OfficeAddress)
                    .HasMaxLength(255);

                entity.Property(e => e.Designation)
                    .HasMaxLength(255);

                entity.Property(e => e.ViberNumber)
                    .HasMaxLength(255);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FacebookNameLink)
                    .HasMaxLength(255);

                entity.Property(e => e.MobileNum)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.BirthDate)
                    .IsRequired();

                entity.Property(e => e.BadgeNumber)
                    .IsRequired();

                entity.HasIndex(e => e.ID)
                    .HasDatabaseName("pki_userinfo");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.PersonsInfo);

            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.ToTable("Roles");

                entity.Property(e => e.ID)
                .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasMany(d => d.Users)
                    .WithOne(p => p.Roles);
            });
            modelBuilder.Entity<Rank>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.ToTable("Ranks");

                entity.Property(e => e.ID)
                .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.RankABBR)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasMany(d => d.PersonsInfo)
                   .WithOne(p => p.Rank);
            });

        }
    }
}
