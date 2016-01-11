﻿namespace AUSKF.Domain.Entities.Identity
{
    // Can't inherit from entity base if used in aspnet identity 

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Providers.Interfaces;

    /// <summary>
    /// </summary>
    [Table("Users")]

    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        public User()
        {
            this.Active = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [StringLength(20)]
        public override string UserName { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [StringLength(20)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [StringLength(256)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password last changed date.
        /// </summary>
        /// <value>
        /// The password last changed date.
        /// </value>
        [DataType(DataType.Date)]
        public DateTime PasswordLastChangedDate { get; set; }

        /// <summary>
        /// Gets or sets the maximum days between password change.
        /// </summary>
        /// <value>
        /// The maximum days between password change.
        /// </value>
        [HiddenInput(DisplayValue = false)]
        public int MaximumDaysBetweenPasswordChange { get; set; }

        /// <summary>
        /// Gets or sets the post count.
        /// </summary>
        /// <value>
        /// The post count.
        /// </value>
        public int PostCount { get; set; }

        /// <summary>
        /// Gets or sets the topic count.
        /// </summary>
        /// <value>
        /// The topic count.
        /// </value>
        public int TopicCount { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required, StringLength(256)]
        [DataType(DataType.EmailAddress)]
        public override string Email { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [StringLength(80)]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the last search.
        /// </summary>
        /// <value>
        /// The last search.
        /// </value>
        [MaxLength(256)]
        public string LastSearch { get; set; }

        /// <summary>
        /// Gets or sets the rank identifier.
        /// </summary>
        /// <value>
        /// The rank identifier.
        /// </value>
        [ForeignKey("KendoRank")]
        public Guid? KendoRankId { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public virtual KendoRank KendoRank { get; set; }

        /// <summary>
        /// Gets or sets the joined date.
        /// </summary>
        /// <value>
        /// The joined date.
        /// </value>
        [Required]
        [DataType(DataType.Date)]
        public DateTime JoinedDate { get; set; }

        /// <summary>
        /// Gets or sets the last login.
        /// </summary>
        /// <value>
        /// The last login.
        /// </value>
        [Required]
        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the row version.
        /// </summary>
        /// <value>
        /// The row version.
        /// </value>
        [Timestamp]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [StringLength(512)]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the user profile identifier.
        /// </summary>
        /// <value>
        /// The user profile identifier.
        /// </value>
        [ForeignKey("Profile")]
        public int UserProfileId { get; set; }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        /// <value>
        /// The profile.
        /// </value>
        public UserProfile Profile { get; set; }

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
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, Guid> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }
}