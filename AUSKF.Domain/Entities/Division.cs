namespace AUSKF.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Division
    {
        [Key]
        public int DivisionId { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [ForeignKey("MaxRank")]
        public int? MaxRankId { get; set; }

        public Rank MaxRank { get; set; }

        [ForeignKey("MinimumRank")]
        public int? MinimumRankId { get; set; }
       
        public Rank MinimumRank { get; set; }

        public int? MinimumAge { get; set; }
    }
}