namespace AUSKF.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Identity;

    public class DivisionWinner
    {
        [Key]
        public int DivisionWinnerId { get; set; }

        [ForeignKey("Division")]
        public int DivisionId { get; set; }

        public Division Division { get; set; }

        [ForeignKey("Winner")]
        public int UserId { get; set; }

        public User Winner { get; set; }
    
    }
}