namespace AUSKF.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using Identity;
    using Newtonsoft.Json;

    // Promotion cannot inherit from entity base as we need to do 
    // an initial data import
    [DataContract(Namespace="")]
    public class Promotion : IComparable<Promotion>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int PromotionId { get; set; }

        [ForeignKey("User")]
        [JsonIgnore]
        public int? UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public int? ContactId { get; set; }

        // this is superfluous but we'll leave it for now
        [JsonIgnore]        
        public int? AuskfId { get; set; }

        // this is superfluous but we'll leave it for now
        [MaxLength(255)]
        [JsonIgnore]
        public string FirstName { get; set; }

        // this is superfluous but we'll leave it for now
        [MaxLength(255)]
        [JsonIgnore]
        public string LastName { get; set; }

        [DataMember]
        public bool Verified { get; set; }

        [DataMember]
        public DateTime? RankDate { get; set; }

        // this needs to be mapped still in legacy data
        [ForeignKey("Rank")]
        [JsonIgnore]
        public int? RankId { get; set; }

        [DataMember]
        public Rank Rank { get; set; }

        [MaxLength(20)]
        [DataMember]
        public string RankType { get; set; }

        [MaxLength(20)]
        [DataMember]
        public string RankListed { get; set; }

        [MaxLength(255)]
        [DataMember]
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
            if (other == null || other.RankDate == null)
            {
                return -1;
            }

            if (this.RankDate == null || this.RankDate.Value < other.RankDate.Value)
            {
                return 1;
            }

            if (this.RankDate.Value == other.RankDate.Value)
            {
                return 0;
            }

            return -1;
        }
    }
}