namespace AUSKF.Domain.Entities.Identity
{
    // Can't inherit from entity base if used in aspnet identity 
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Newtonsoft.Json;

    [DataContract(Namespace="")]
    public class UserClaim : IdentityUserClaim<int>
    {

        [Key]
        [DataMember]
        public override int Id { get; set; }

        [JsonIgnore]
        [ForeignKey("User")]
        public override int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

    }
}