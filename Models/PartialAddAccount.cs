using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoyalBank.Models
{
    [MetadataType(typeof(PartialAddAccount))]
    public partial class account_details
    { }
    public class PartialAddAccount
    {
        public long AccountId { get; set; }
         [RegularExpression("([0-9]{6})", ErrorMessage = "Enter 6 digit CustomerID only(No Alphabets)")]
       
        public int CustomerId { get; set; }
        [Required]
        public string AccountType { get; set; }
        public string Status { get; set; }
        [Range(500,1000000,ErrorMessage="Please Open Account with Minimum balance as 500")]
        public decimal Balance { get; set; }
        public string Message { get; set; }
        public System.DateTime LastUpdated { get; set; }
    }
}