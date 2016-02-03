namespace AUSKF.Domain.Entities.Identity
{
    // Can't inherit from entity base if used in aspnet identity 
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// </summary>
    [DataContract(Namespace="")]
    public class UserProfile
    {
        /// <summary>
        /// Gets or sets the user profile identifier.
        /// </summary>
        /// <value>
        /// The user profile identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int UserProfileId { get; set; }

        /// <summary>
        /// Gets or sets the birth day.
        /// </summary>
        /// <value>
        /// The birth day.
        /// </value>
        [DataType(DataType.Date)]
        [DataMember]
        public DateTime? BirthDay { get; set; }

        // the old id from the access db
        [DataMember]
        public int ContactId { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [StringLength(20)]
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Middle Name
        /// </summary>
        [StringLength(20)]
        [DataMember]
        public string MiddleName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [StringLength(20)]
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// M for male. F for femail.
        /// </summary>
        [StringLength(1)]
        [DataMember]
        public string Gender { get; set; }
          
        /// <summary>
        /// Unique ID number
        /// </summary>
        /// <remarks>
        /// This has to be a nullable as it is quite possible to not
        /// have a valid auskf id number and still be tracked by the 
        /// auskf.
        /// </remarks>
        [DataMember]
        public int? AuskfIdNumber { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [MaxLength(200)]
        [DataMember]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [DataMember]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [DataMember]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the sig.
        /// </summary>
        /// <value>
        /// The sig.
        /// </value>
        [MaxLength(512)]
        [DataMember]
        public string Sig { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow HTML sig].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow HTML sig]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool AllowHtmlSig { get; set; }

        /// <summary>
        /// Gets or sets the facebook page.
        /// </summary>
        /// <value>
        /// The facebook page.
        /// </value>
        [MaxLength(512)]
        [DataMember]
        public string FacebookPage { get; set; }

        /// <summary>
        /// Gets or sets the name of the skype user.
        /// </summary>
        /// <value>
        /// The name of the skype user.
        /// </value>
        [MaxLength(256)]
        [DataMember]
        public string SkypeUserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the twitter.
        /// </summary>
        /// <value>
        /// The name of the twitter.
        /// </value>
        [MaxLength(50)]
        [DataMember]
        public string TwitterName { get; set; }

        /// <summary>
        /// Gets or sets the home page.
        /// </summary>
        /// <value>
        /// The home page.
        /// </value>
        [MaxLength(512)]
        [DataMember]
        public string HomePage { get; set; }

        [MaxLength(256)]
        [DataMember]
        public string SharePointEditor { get; set; }

        [MaxLength(256)]
        [DataMember]
        public string SharePointAuthor { get; set; }

        [DataMember]
        public DateTime? SharePointModifiedDate { get; set; }

        [DataMember]
        public DateTime? SharePointCreatedDate { get; set; }

        [ForeignKey("Dojo")]
        [DataMember]
        public int? DojoId { get; set; }

        [DataMember]
        public Dojo Dojo { get; set; }

        [ForeignKey("Federation")]
        [DataMember]
        public int? FederationId { get; set; }

        [DataMember]
        public virtual Federation Federation { get; set; }

        /// <summary>
        /// Foreign key to users current address
        /// </summary>
        [ForeignKey("Address")]
        [DataMember]
        public int? AddressId { get; set; }

        /// <summary>
        /// Users current address
        /// </summary>
        [DataMember]
        public virtual Address Address { get; set; }

        /// <summary>
        /// Gets or sets the rank identifier.
        /// </summary>
        /// <value>
        /// The rank identifier.
        /// </value>
        [ForeignKey("Rank")]
        [DataMember]
        public int? RankId { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        [DataMember]
        public virtual Rank Rank { get; set; }

        [DataType(DataType.Date)]
        [DataMember]
        public DateTime? RankDate { get; set; }

        [DataMember]
        public int? FirstYearRegistration { get; set; }

        [MaxLength(50)]
        [DataType(DataType.PhoneNumber)]
        [DataMember]
        public string HomePhone { get; set; }

        [MaxLength(50)]
        [DataType(DataType.PhoneNumber)]
        [DataMember]
        public string MobilePhone { get; set; }

        [MaxLength(50)]
        [DataType(DataType.PhoneNumber)]
        [DataMember]
        public string BusinessPhone { get; set; }

        [MaxLength(256)]
        [DataMember]
        public string WebPage { get; set; }

        [MaxLength(256)]
        [DataMember]
        public string Notes { get; set; }

        [JsonIgnore]
        public int? OldId { get; set; }

        [MaxLength(256)]
        [DataMember]
        public string ContactName { get; set; }

        [MaxLength(256)]
        [JsonIgnore]
        public string FileAs { get; set; }

        [MaxLength(256)]
        [JsonIgnore]
        public string Attachments { get; set; }
    }
}