using FreeSecur.API.Domain.Entities.TeamUsers;
using FreeSecur.API.Domain.Entities.Owners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FreeSecur.API.Core.Validation.Attributes;
using Microsoft.EntityFrameworkCore;

namespace FreeSecur.API.Domain.Entities.Users
{
    [Index(nameof(Username), IsUnique = true)]
    public class User : IOwner, IFsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [FsRequired]
        [FsEmailAddress]
        public string Email { get; set; }
        [FsRequired]
        public string Username { get; set; }

        [FsRequired]
        [FsMinLength(10)]
        public string Password { get; set; }
        [FsRequired]
        public string PasswordSalt { get; set; }
        [FsRequired]
        public string FirstName { get; set; }
        [FsRequired]
        public string LastName { get; set; }
        [FsRequired]
        public bool IsEmailConfirmed { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
        [FsRequired]
        public int OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }

        [InverseProperty(nameof(TeamUser.User))]
        public ICollection<TeamUser> Teams { get; set; }
    }
}
