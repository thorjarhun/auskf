namespace AUSKF.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    /// <summary>
    /// AUSKF member federation information
    /// </summary>
    [DataContract(Namespace="")]
    public class Federation : EntityBase
    {
        /// <summary>
        /// Federation identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int FederationId { get; set; }

        [Required]
        [DataMember]
        public string Name { get; set; }

        //[Required]
        [MaxLength(512)]
        [DataMember]
        public string Email { get; set; }

        [MaxLength(13), DataType(DataType.PhoneNumber)]
        [DataMember]
        public string Phone { get; set; }

        [MaxLength(512)]
        [DataMember]
        public string WebsiteUrl { get; set; }
    }
}