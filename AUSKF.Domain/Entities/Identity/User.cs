namespace AUSKF.Domain.Entities.Identity
{
    // Can't inherit from entity base if used in aspnet identity 

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Newtonsoft.Json;
    using Providers.Interfaces;

    /// <summary>
    /// </summary>
    [Table("Users")]
    [DataContract(Namespace="")]
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        [SuppressMessage("ReSharper", "VirtualMemberCallInContructor")]
        public User()
        {
            this.Active = true;
            this.Promotions = new List<Promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public override int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the user profile identifier.
        /// </summary>
        /// <value>
        /// The user profile identifier.
        /// </value>
        [ForeignKey("Profile")]
        [DataMember]
        public int UserProfileId { get; set; }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        /// <value>
        /// The profile.
        /// </value>
        [DataMember]
        public UserProfile Profile { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [MaxLength(20)]
        [DataMember]
        public override string UserName { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [MaxLength(20)]
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [MaxLength(20)]
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Middle Name
        /// </summary>
        [MaxLength(20)]
        [DataMember]
        public string MiddleName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [MaxLength(20)]
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// M for male. F for femail.
        /// </summary>
        [MaxLength(1)]
        [DataMember]
        public string Gender { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        [DataType(DataType.DateTime)]
        [DataMember]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Unique ID number
        /// </summary>
        [DataMember]
        public int AuskfId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [StringLength(256)]
        [DataType(DataType.Password)]
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password last changed date.
        /// </summary>
        /// <value>
        /// The password last changed date.
        /// </value>
        [DataType(DataType.Date)]
        [DataMember]
        public DateTime PasswordLastChangedDate { get; set; }

        /// <summary>
        /// Gets or sets the maximum days between password change.
        /// </summary>
        /// <value>
        /// The maximum days between password change.
        /// </value>
        [HiddenInput(DisplayValue = false)]
        [DataMember]
        public int MaximumDaysBetweenPasswordChange { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [StringLength(256)]
        [DataType(DataType.EmailAddress)]
        [DataMember]
        public override string Email { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [StringLength(80)]
        [DataMember]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the last search.
        /// </summary>
        /// <value>
        /// The last search.
        /// </value>
        [MaxLength(256)]
        [DataMember]
        public string LastSearch { get; set; }
         
        /// <summary>
        /// Gets or sets the joined date.
        /// </summary>
        /// <value>
        /// The joined date.
        /// </value>
        [DataType(DataType.Date)]
        [DataMember]
        public DateTime JoinedDate { get; set; }

        /// <summary>
        /// Gets or sets the last login.
        /// </summary>
        /// <value>
        /// The last login.
        /// </value>
        [DataType(DataType.Date)]
        [DataMember]
        public DateTime LastLogin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Required]
        [DataMember]
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the row version.
        /// </summary>
        /// <value>
        /// The row version.
        /// </value>
        [Timestamp]
        [JsonIgnore]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [StringLength(512)]
        [DataMember]
        public string Notes { get; set; }

        [NotMapped]
        public override string PhoneNumber { get; set; }

        [NotMapped]
        public override bool PhoneNumberConfirmed { get; set; }

        public virtual List<Promotion> Promotions { get; set; }


        /// <summary>
        /// Generates the user identity asynchronous.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(IApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }

        /// <summary>
        /// Generates the user identity asynchronous.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="authenticationType">Type of the authentication.</param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }
}