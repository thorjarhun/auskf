namespace AUSKF.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum RankType
    {
        Kendo = 1,
        Iaido = 2,
        Other = 3
    }

    public class Rank
    {
        /// <summary>
        /// Gets or sets the kendo rank identifier.
        /// </summary>
        /// <value>
        /// The kendo rank identifier.
        /// </value>
        [Key]
        public int RankId  { get; set; }

        /// <summary>
        /// Gets or sets the name of the kendo rank.
        /// </summary>
        /// <value>
        /// The name of the kendo rank.
        /// </value>
        [Required]
        public string RankName { get; set; }

        /// <summary>
        /// Gets or sets the kendo rank numeric.
        /// </summary>
        /// <value>
        /// The kendo rank numeric.
        /// </value>
        [Required]
        public int RankNumeric { get; set; }

        [Required, MaxLength(512)]
        public string Eligibility { get; set; }

        [Required, MaxLength(30)]
        public string MinimumRankOfExaminers { get; set; }

        [Required]
        public int NumberOfExaminers { get; set; }

        [Required]
        public int ConsentingExaminersRequired { get; set; }

        public RankType RankType { get; set; }
    }
}