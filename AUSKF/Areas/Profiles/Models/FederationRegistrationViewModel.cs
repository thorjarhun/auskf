namespace AUSKF.Areas.Profiles.Models
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
     
    public class FederationRegistrationViewModel : MyProfileViewModel
    {
         
        [Required]
        [Display(Name = "Applicant Signature Parent/Guardian signature if under 18")]
        public bool AuthorizedElectronicSignature { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string ElectronicSignature { get; set; }

    }
}