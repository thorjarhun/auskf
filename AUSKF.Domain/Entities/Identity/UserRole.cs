namespace AUSKF.Domain.Entities.Identity
{
    // Can't inherit from entity base if used in aspnet identity 
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Newtonsoft.Json;

    public class UserRole : IdentityUserRole<int>
    {
        public UserRole()
        {
            this.Users = new List<User>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public override int RoleId { get; set; }

        [NotMapped, JsonIgnore]
        public override int UserId { get; set; }

        [MaxLength(56)]
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}