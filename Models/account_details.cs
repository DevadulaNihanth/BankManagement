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
    
    public partial class account_details
    {
        public long AccountId { get; set; }
        public int CustomerId { get; set; }
        public string AccountType { get; set; }
        public string Status { get; set; }
        public decimal Balance { get; set; }
        public string Message { get; set; }
        public System.DateTime LastUpdated { get; set; }
    }
}