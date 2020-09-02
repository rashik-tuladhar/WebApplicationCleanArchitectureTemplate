using System;
using System.Collections.Generic;
using Application.DTOs.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Application.DTOs.Authentication.UserDTOs
{
    public class UserViewModel : CommonDetails
    {
        public string Password { get; set; }
        public string DefaultPassword { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string ContactNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public bool ForcePasswordChange { get; set; }
        public string FullName { get; set; }
        public string FullNameLocal { get; set; }
        public string Gender { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string PermanentAddress { get; set; }
        public string Status { get; set; }
        public string TemporaryAddress { get; set; }

        public string Role { get; set; }
        public List<SelectListItem> GenderList { get; set; }
        public List<SelectListItem> RoleList { get; set; }
    }

    public class UserViewModelDetails
    {
        public List<ListDropDown> ListGender { get; set; }
        public List<ListDropDown> ListRoles { get; set; }
    }
}
