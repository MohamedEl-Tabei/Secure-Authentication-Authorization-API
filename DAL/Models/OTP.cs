using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class OTP
    {
        public string Target { get; set; }
        public string? CodeHash { get; set; }
        public bool IsVerified { get; set; } = false;
        public OTPTargetType TargetType { get; set; }
        public DateTime ExpirationTime { get; set; }

    }
}
