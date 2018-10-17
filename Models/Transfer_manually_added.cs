using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoyalBank.Models
{
    public class Transfer_manually_added
    {
        [Required]
        public int CustomerId { get; set; }
         [Required]
        public String SrcAccounType { get; set; }
         [Required]
        public String DestAccounType { get; set; }
         [Required]
        [Range(1,25000, ErrorMessage="You cannot tranfer more than 25000 in a single transaction")]
        public decimal Amount { get; set; }
    }
}