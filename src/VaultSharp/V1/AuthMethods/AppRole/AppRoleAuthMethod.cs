using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using VaultSharp.Core;
using VaultSharp.V1.Commons;

namespace VaultSharp.V1.AuthMethods.AppRole
{
    internal class AppRoleAuthMethod : IAppRoleAuthMethod
    {
        private readonly Polymath _polymath;
        public AppRoleAuthMethod(Polymath polymath)
        {
            _polymath = polymath;
        }
        public async System.Threading.Tasks.Task<AppRoleCredentialRoleId> GetRoleIdAsync(string roleName)
        {
            return await _polymath.MakeVaultApiRequest<AppRoleCredentialRoleId>($"v1/auth/approle/role/{roleName}/role-id", HttpMethod.Get).ConfigureAwait(_polymath.VaultClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async System.Threading.Tasks.Task<AppRoleCredentialSecretId> GetSecretIdAsync(string roleName)
        {
            return await _polymath.MakeVaultApiRequest<AppRoleCredentialSecretId>($"v1/auth/approle/role/{roleName}/secret-id", HttpMethod.Post).ConfigureAwait(_polymath.VaultClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

    }

    #region AppRoleLogin
    public class AppRoleLogin : AppRoleCredentialBase
    { }

    public class AppRoleLoginPolicies
    {
        [JsonProperty("token_policies")]
        public List<string> TokenPolicies { get; set; }
    }

    #endregion AppRoleLogin

    public class AppRoleCredentialBase
    {
        [JsonProperty("auth")]
        public AuthInfo Auth { get; set; }

        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }

        [JsonProperty("wrap_info")]
        public WrapInfo WrapInfo { get; set; }

        [JsonProperty("lease_duration")]
        public int LeaseDuration { get; set; }

        [JsonProperty("renewable")]
        public bool Renewable { get; set; }

        [JsonProperty("lease_id")]
        public string LeaseId { get; set; }

    }
    public class AppRoleCredentialRoleId : AppRoleCredentialBase
    {
        [JsonProperty("data")]
        public AppRoleCredentialRoleIdData Data { get; set; }
    }

    public class AppRoleCredentialRoleIdData
    {
        [JsonProperty("role_id")]
        public string RoleId { get; set; }
    }


    public class AppRoleCredentialSecretId : AppRoleCredentialBase
    {
        [JsonProperty("data")]
        public AppRoleCredentialSecretIdData Data { get; set; }
    }

    public class AppRoleCredentialSecretIdData
    {
        [JsonProperty("secret_id_accessor")]
        public string SecretIdAccessor { get; set; }

        [JsonProperty("secret_id")]
        public string SecretId { get; set; }
    }
}
