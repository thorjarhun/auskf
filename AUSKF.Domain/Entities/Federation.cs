namespace AUSKF.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// AUSKF member federation information
    /// </summary>
    public class Federation : EntityBase
    {
        /// <summary>
        /// Federation identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FederationId { get; set; }

        [Required]
        public string Name { get; set; }

        //[Required]
        [MaxLength(512)]
        public string Email { get; set; }

        [MaxLength(13), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [MaxLength(512)]
        public string WebsiteUrl { get; set; }
    }
}