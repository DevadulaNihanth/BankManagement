using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoyalBank.Models
{
    [MetadataType(typeof(PartialAddCustomer))]
    public partial class customer_details
    {
    }
    public class PartialAddCustomer
    {
        public int CustomerId { get; set; }
        [Required]
        //[RegularExpression("([1-9]{1}[0-9]{9})", ErrorMessage = "SSNID should be exactly 9 digits")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "SSNID must be numeric")]
        public Nullable<long> SSNID { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string DOB { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        public string Status { get; set; }

        public virtual ICollection<customer_log> customer_log { get; set; }
    }





}