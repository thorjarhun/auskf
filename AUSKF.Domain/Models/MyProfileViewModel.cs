namespace AUSKF.Domain.Models
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

        [Display(Name = "Dojo")]
        public int? DojoId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "AUSKF Id Number")]
        public string AuskfIdNumber { get; set; }

        [Display(Name = "Federation")]
        public string MyFederation { get; set; }

        [Display(Name = "AUSKF Registration")]
        public bool IsAuskfRegistered { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

    }
}
