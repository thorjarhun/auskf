﻿namespace AUSKF.Domain.Entities
{
    using Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Dojos")]
    public class Dojo : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DojoId { get; set; }

        [ForeignKey("Federation")]
        public int? FederationId { get; set; }

        public Federation Federation { get; set; }

        [ForeignKey("Address")]
        public int? AddressId { get; set; }

        public Address Address { get; set; }

        [ForeignKey("PrimaryContact")]
        public int? PrimaryContactId { get; set; }

        public User PrimaryContact { get; set; }

        [Required, MaxLength(256)]
        public string DojoName { get; set; }

        [MaxLength(13), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [MaxLength(512)]
        public string WebsiteUrl { get; set; }

        // TODO - NEVER expose an email address on the internet! 
        // TODO - We'll create a contact form and send a request on behalf
        [MaxLength(512)]
        public string EmailAddress { get; set; }

        [MaxLength(1024)]
        public string Notes { get; set; }
    }
}