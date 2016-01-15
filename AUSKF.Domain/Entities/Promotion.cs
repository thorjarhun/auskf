namespace AUSKF.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Identity;

    public class Promotion : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PromotionId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public int ContactId { get; set; }

        public User User { get; set; }

        // this is superfluous but we'll leave it for now
        public int AuskfId { get; set; }

        // this is superfluous but we'll leave it for now
        [MaxLength(255)]
        public string FirstName { get; set; }

        // this is superfluous but we'll leave it for now
        [MaxLength(255)]
        public string LastName { get; set; }

        public bool Verified { get; set; }

        public DateTime RankDate { get; set; }

        // this needs to be mapped still in legacy data
        [ForeignKey("Rank")]
        public int? RankId { get; set; }

        public Rank Rank { get; set; }

        [MaxLength(20)]
        public string RankType { get; set; }

        [MaxLength(20)]
        public string RankListed { get; set; }

        [MaxLength(255)]
        public string TestingFederationLocation { get; set; }
    }
}