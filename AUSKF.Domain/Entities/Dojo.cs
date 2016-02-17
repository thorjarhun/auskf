namespace AUSKF.Domain.Entities
{
    using Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    [Table("Dojos")]
    [DataContract(Namespace="")]
    public class Dojo : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int DojoId { get; set; }

        [ForeignKey("Federation")]
        [DataMember]
        public int? FederationId { get; set; }
        
        [JsonIgnore]
        public Federation Federation { get; set; }

        [ForeignKey("Address")]
        [DataMember]
        public int? AddressId { get; set; }

        [DataMember]
        public Address Address { get; set; }

        [ForeignKey("PrimaryContact")]
        [JsonIgnore]
        public int? PrimaryContactId { get; set; }

        [JsonIgnore]
        public User PrimaryContact { get; set; }

        [Required, MaxLength(256)]
        [DataMember]
        public string DojoName { get; set; }

        [MaxLength(13), DataType(DataType.PhoneNumber)]
        [DataMember]
        public string Phone { get; set; }

        [MaxLength(512)]
        [DataMember]
        public string WebsiteUrl { get; set; }

        [MaxLength(512)]
        [DataMember]
        public string EmailAddress { get; set; }

        [MaxLength(1024)]
        [DataMember]
        public string Notes { get; set; }
    }
}