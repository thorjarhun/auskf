namespace AUSKF.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Identity;

    /// <summary>
    /// Individual's membership to a federation information
    /// </summary>
    public class FederationMembership : EntityBase
    {
        /// <summary>
        /// Federation membership identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FederationMembershipId { get; set; }
         
        /// <summary>
        /// User id for who's membership to the federation is defined.
        /// </summary>
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
         
        /// <summary>
        /// Federation id the user is a member of
        /// </summary>
        [Required]
        [ForeignKey("Federation")]
        public int FederationId { get; set; }

        /// <summary>
        /// User who's membership to the federation is defined.
        /// </summary> 
        public User User { get; set; }

        /// <summary>
        /// Federation the user is a member of
        /// </summary> 
        public Federation Federation { get; set; }

        /// <summary>
        /// Year for which this user is a member of the federation
        /// </summary>
        [Required]
        public int MembershipYear { get; set; }
    }
}