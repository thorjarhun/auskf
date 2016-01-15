namespace AUSKF.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Identity;

    public class Comment : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, MaxLength(1024)]
        public string Text { get; set; }

        [Required, ForeignKey("CreatedBy")]
        public int UserId { get; set; }

        public User CreatedBy { get; set; }
    }
}