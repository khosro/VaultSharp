using System.Net.Http;
using Newtonsoft.Json;
using VaultSharp.Core;

namespace VaultSharp.V1.AuthMethods.AppRole
{
    internal class AppRoleAuthMethod : IAppRoleAuthMethod
    {
        private readonly Polymath _polymath;
        public AppRoleAuthMethod(Polymath polymath)
        {
            _polymath = polymath;
        }
        public async System.Threading.Tasks.Task<AppRoleRoleId> GetClientIdAsync(string roleName)
        {
            return await _polymath.MakeVaultApiRequest<AppRoleRoleId>($"v1/auth/approle/role/{roleName}/role-id", HttpMethod.Get, false).ConfigureAwait(_polymath.VaultClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async System.Threading.Tasks.Task<AppRoleSecretId> GetSecretIdAsync(string roleName)
        {
            return await _polymath.MakeVaultApiRequest<AppRoleSecretId>($"v1/auth/approle/role/{roleName}/secret-id", HttpMethod.Post).ConfigureAwait(_polymath.VaultClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }

    public class AppRoleRoleModelBase
    {
        [JsonProperty("auth")]
        public AppRoleRoleIdData Auth { get; set; }

        [JsonProperty("warnings")]
        public AppRoleRoleIdData Warnings { get; set; }

        [JsonProperty("wrap_info")]
        public AppRoleRoleIdData WrapInfo { get; set; }

        [JsonProperty("lease_duration")]
        public int LeaseDuration { get; set; }

        [JsonProperty("renewable")]
        public bool Renewable { get; set; }

        [JsonProperty("lease_id")]
        public string LeaseId { get; set; }

    }
    public class AppRoleRoleId : AppRoleRoleModelBase
    {
        [JsonProperty("data")]
        public AppRoleRoleIdData Data { get; set; }
    }

    public class AppRoleRoleIdData
    {
        [JsonProperty("role_id")]
        public string RoleId { get; set; }
    }


    public class AppRoleSecretId : AppRoleRoleModelBase
    {
        [JsonProperty("data")]
        public AppRoleSecretIdData Data { get; set; }
    }

    public class AppRoleSecretIdData
    {
        [JsonProperty("secret_id_accessor")]
        public string SecretIdAccessor { get; set; }

        [JsonProperty("secret_id")]
        public string SecretId { get; set; }
    }
}
