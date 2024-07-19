using System;
using System.Collections.Generic;

namespace AuthService.Models
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
    }
}
