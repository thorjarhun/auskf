﻿namespace AUSKF.Domain.Entities.Identity
{
    // Can't inherit from entity base if used in aspnet identity 
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserLogin : IdentityUserLogin<int>
    {
        [Key]
        public Guid UserLoginId { get; set; }

        [ForeignKey("User")]
        public override int UserId { get; set; }

        public User User { get; set; }

        [MaxLength(256)]
        public override string ProviderKey { get; set; }

        [MaxLength(256)]
        public override string LoginProvider { get; set; }
    }
}