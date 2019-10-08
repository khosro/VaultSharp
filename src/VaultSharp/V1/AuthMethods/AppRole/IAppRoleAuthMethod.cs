namespace VaultSharp.V1.AuthMethods.AppRole
{
    /// <summary>
    /// Non login operations.
    /// </summary>
    public interface IAppRoleAuthMethod
    {
        System.Threading.Tasks.Task<AppRoleCredentialRoleId> GetRoleIdAsync(string roleName);

        System.Threading.Tasks.Task<AppRoleCredentialSecretId> GetSecretIdAsync(string roleName);
    }
}
