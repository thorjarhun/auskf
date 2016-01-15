namespace AUSKF.Domain.Entities.Identity
{
    // Can't inherit from entity base if used in aspnet identity 
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserClaim : IdentityUserClaim<int>
    {

        [Key]
        public override int Id { get; set; }

        [ForeignKey("User")]
        public override int UserId { get; set; }

        public virtual User User { get; set; }

    }
}