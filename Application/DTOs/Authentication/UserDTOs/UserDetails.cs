using System;
using Domain.Settings;

namespace Application.DTOs.Authentication.UserDTOs
{
    public class UserListDetails : GridExtension
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string FullNameLocal { get; set; }
        public string Gender { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string PermanentAddress { get; set; }
        public string Status { get; set; }
        public string TemporaryAddress { get; set; }
    }
}
