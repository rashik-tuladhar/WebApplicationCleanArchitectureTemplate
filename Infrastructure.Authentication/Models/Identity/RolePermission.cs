using System;

namespace Infrastructure.Authentication.Models.Identity
{
    public class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public virtual DateTime? LastUpdated { get; set; }

    }

    public class RolePermission : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Group { get; set; }
        public string Icon { get; set; }
        public string SubGroup { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public string MenuName { get; set; }
        public string Link { get; set; }
        public string GroupIcon { get; set; }
        public string SubGroupName { get; set; }
    }
}
