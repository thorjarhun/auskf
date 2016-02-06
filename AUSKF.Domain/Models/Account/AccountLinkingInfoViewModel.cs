using System.ComponentModel.DataAnnotations;

namespace AUSKF.Domain.Models.Account
{

    public class AccountLinkingInfoViewModel
    {

        [Display(Name = "AUSKF Id")]
        public int AuskfId { get; set; }

    }

}