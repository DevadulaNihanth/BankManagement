//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoyalBank.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class customer_details
    {
        public customer_details()
        {
            this.customer_log = new HashSet<customer_log>();
        }
    
        public int CustomerId { get; set; }
        public Nullable<long> SSNID { get; set; }
        public string CustomerName { get; set; }
        public string DOB { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<customer_log> customer_log { get; set; }
    }
}
