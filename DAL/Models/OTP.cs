using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class OTP
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Target { get; set; }
        public string CodeHash { get; set; }
        public OTPTargetType TargetType { get; set; }
        public DateTime ExpirationTime { get; set; } = DateTime.Now.AddMinutes(5);

    }
}
