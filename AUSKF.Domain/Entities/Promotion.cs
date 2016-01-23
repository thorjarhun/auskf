namespace AUSKF.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Identity;

    // Promotion cannot inherit from entity base as we need to do 
    // an initial data import

    public class Promotion : IComparable<Promotion>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromotionId { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        public User User { get; set; }

        public int? ContactId { get; set; }

        // this is superfluous but we'll leave it for now
        public int? AuskfId { get; set; }

        // this is superfluous but we'll leave it for now
        [MaxLength(255)]
        public string FirstName { get; set; }

        // this is superfluous but we'll leave it for now
        [MaxLength(255)]
        public string LastName { get; set; }

        public bool Verified { get; set; }

        public DateTime? RankDate { get; set; }

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

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an 
        /// integer that indicates whether the current instance precedes, follows, or occurs 
        /// in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return 
        /// value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other" />
        ///  in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other" />.
        ///  Greater than zero This instance follows <paramref name="other" /> in the sort order.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">other</exception>
        public int CompareTo(Promotion other)
        {
            if ((other == null) || (other.RankDate == null))
            {
                return -1;
            }

            if (this.RankDate == null)
            {
                return 1;
            }
  
            return this.RankDate.Value.CompareTo(other.RankDate.Value);
        }
    }
}