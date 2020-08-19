using FreeSecur.API.Domain.Entities.OrganisationUserRights;
using FreeSecur.API.Domain.Entities.Owners;
using FreeSecur.API.Domain.Entities.Teams;
using FreeSecur.API.Domain.Entities.TeamUserRights;
using FreeSecur.API.Domain.Entities.TeamUsers;
using FreeSecur.API.Domain.Entities.Users;
using FreeSecur.API.Domain.Entities.VaultOwnerRights;
using FreeSecur.API.Domain.Entities.VaultOwners;
using FreeSecur.API.Domain.Entities.Vaults;
using FreeSecur.API.Domain.Entities.VaultSecrets;
using FreeSecur.API.Domain.VaultItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;

namespace FreeSecur.API.Domain
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
