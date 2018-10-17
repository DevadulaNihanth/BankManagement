using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoyalBank.Models
{
    [MetadataType(typeof(PartialEditCustomer))]
    public partial class usp_view_by_customerid_Result
    {
    }

    public class PartialEditCustomer
    {


      
            public int CustomerId { get; set; }
         [RegularExpression("([0-9]{9,9})", ErrorMessage = "SSNID should be exactly 9 digits")]
         [Required]
            public Nullable<long> SSNID { get; set; }
         [Required]
            public string CustomerName { get; set; }
         [Required]
            public string DOB { get; set; }
         [Required]
            public string Address1 { get; set; }
            public string Address2 { get; set; }
         [Required]
            public string City { get; set; }
         [Required]
            public string State { get; set; }

            public string Status { get; set; }
        }
    
}