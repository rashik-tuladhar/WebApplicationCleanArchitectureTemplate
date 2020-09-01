namespace Application.Interfaces.CoreSetup.RoleManagement
{
    public interface IRoleExtension
    {
        bool HasPermission(string permissionValue);
    }
}
