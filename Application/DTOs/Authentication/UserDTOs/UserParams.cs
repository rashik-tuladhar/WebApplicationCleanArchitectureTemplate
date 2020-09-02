using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Authentication.UserDTOs
{
    public class UserParams
    {
        public string Id { get; set; }
        public string Flag { get; set; }
        public string? ModifiedBy { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
