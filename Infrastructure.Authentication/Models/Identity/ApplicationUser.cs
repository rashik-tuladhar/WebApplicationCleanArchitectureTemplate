using System;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string FullNameLocal { get; set; }
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string MobileNumber { get; set; }
        public string PermanentAddress { get; set; }
        public string TemporaryAddress { get; set; }
        public bool ForcePasswordChange { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}
