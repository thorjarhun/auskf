namespace AUSKF.Areas.Profiles.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model for user profile page
    /// </summary>
    public class MyProfileViewModel
    {

        [Display(Name = "Rank")]
        public int? RankId { get; set; }

        [Display(Name = "Rank")]
        public string Rank { get; set; }

        [Display(Name = "Dojo")]
        public int? DojoId { get; set; }

        [Display(Name = "Dojo")]
        public string Dojo { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "AUSKF Id Number")]
        public string AuskfIdNumber { get; set; }

        [Display(Name = "Federation")]
        public string MyFederation { get; set; }

        [Display(Name = "AUSKF Registration")]
        public bool IsAuskfRegistered { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Confirm")]
        public string EmailConfirm { get; set; }
         
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

    }
}
