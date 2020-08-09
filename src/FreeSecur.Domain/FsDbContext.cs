using FreeSecur.Domain.Entities.OrganisationUserRights;
using FreeSecur.Domain.Entities.Owners;
using FreeSecur.Domain.Entities.Teams;
using FreeSecur.Domain.Entities.TeamUserRights;
using FreeSecur.Domain.Entities.TeamUsers;
using FreeSecur.Domain.Entities.Users;
using FreeSecur.Domain.Entities.VaultOwnerRights;
using FreeSecur.Domain.Entities.VaultOwners;
using FreeSecur.Domain.Entities.Vaults;
using FreeSecur.Domain.Entities.VaultSecrets;
using FreeSecur.Domain.VaultItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;

namespace FreeSecur.Domain
{
    public class FsDbContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamUserRight> TeamUserRights { get; set; }
        public DbSet<TeamUser> TeamUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VaultItem> VaultItems { get; set; }
        public DbSet<VaultOwnerRight> VaultOwnerRights { get; set; }
        public DbSet<VaultOwner> VaultOwners { get; set; }
        public DbSet<Vault> Vaults { get; set; }
        public DbSet<VaultSecret> VaultSecrets { get; set; }

        public FsDbContext(DbContextOptions<FsDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<TeamUserRight>().Property(x => x.AccessRight).HasConversion(new EnumToStringConverter<TeamUserRightType>());
            modelBuilder.Entity<VaultOwnerRight>().Property(x => x.AccessRight).HasConversion(new EnumToStringConverter<VaultOwnerRightType>());
            modelBuilder.Entity<OrganisationUserRight>().Property(x => x.AccessRight).HasConversion(new EnumToStringConverter<OrganisationUserRightType>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
