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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TeamUserRight>().Property(x => x.AccessRight).HasConversion(new EnumToStringConverter<TeamUserRightType>());
            modelBuilder.Entity<VaultOwnerRight>().Property(x => x.AccessRight).HasConversion(new EnumToStringConverter<VaultOwnerRightType>());
        }
    }
}
