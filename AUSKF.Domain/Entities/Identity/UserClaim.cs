namespace AUSKF.Domain.Entities.Identity
{
    // Can't inherit from entity base if used in aspnet identity 
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserClaim : IdentityUserClaim<Guid>
    {

        [Key]
        public override int Id { get; set; }

        [ForeignKey("User")]
        public override Guid UserId { get; set; }

        public User User { get; set; }

    }
}