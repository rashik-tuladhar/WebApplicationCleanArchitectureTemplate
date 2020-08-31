using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Infrastructure.Authentication.Models
{
    [Serializable]
    [Table("TokenAuthenticationDetails", Schema = "Authentication")]
    public class TokenAuthenticationDetails : BaseEntity
    {
        public string IdentificationId { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Roles { get; set; }
    }
}
