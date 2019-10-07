namespace VaultSharp.V1.AuthMethods.AppRole
{
    /// <summary>
    /// Non login operations.
    /// </summary>
    public interface IAppRoleAuthMethod
    {
        System.Threading.Tasks.Task<AppRoleRoleId> GetClientIdAsync(string roleName);

        System.Threading.Tasks.Task<AppRoleSecretId> GetSecretIdAsync(string roleName);
    }
}
