using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoyalBank.Models
{   [MetadataType(typeof(PartialLogin))]
    public partial class login_master
    {
    }
    public class PartialLogin
    {
        public int login_id { get; set; }
        [Display(Description = "UserName")]
        [Required]
        [RegularExpression("([a-zA-Z0-9]{8,})", ErrorMessage = "Enter only alphabets and numbers in UserName (Minimumlegth 8characters)")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^(?=.*[0-9])(?=.*[A-Z])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{10,}", ErrorMessage = "Enter Atleast 10 characters including one upper Case,Number and Special Character")]
        public string Password { get; set; }
        public Nullable<int> role_id { get; set; }
        public List<role_master> Roles { get; set; }


    }
}