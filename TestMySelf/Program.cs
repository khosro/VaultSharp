using System;
using System.Collections.Generic;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.AppRole;
using VaultSharp.V1.AuthMethods.Token;

namespace TestMySelf
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Test();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine("Done!");

            Console.ReadLine();
        }

        private static async void Test()
        {
            IAuthMethodInfo authMethod;
            authMethod = new TokenAuthMethodInfo("s.2XoNGn1zjYomLPbCUVyoDhfq");

            var vaultClientSettings = new VaultClientSettings("http://127.0.0.1:8200", authMethod);

            IVaultClient vaultClient = new VaultClient(vaultClientSettings);

            var roleName = "my-role";

            var secretIdAsync = await vaultClient.V1.Auth.AppRole.GetSecretIdAsync(roleName);
            var appRoleRoleId = await vaultClient.V1.Auth.AppRole.GetRoleIdAsync(roleName);



            //var authMethodNew = new AppRoleAuthMethodInfo(appRoleRoleId.Data.RoleId,"");
            var authMethodNew = new AppRoleAuthMethodInfo(appRoleRoleId.Data.RoleId, secretIdAsync.Data.SecretId);
            var vaultClientSettingsNew = new VaultClientSettings("http://127.0.0.1:8200", authMethodNew);
            IVaultClient vaultClientNew = new VaultClient(vaultClientSettingsNew);


            // Use client to read a key-value secret.
            // var kv2Secret = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync("secret-name");
            var dic = new Dictionary<string, object>();
            for (var i = 0; i < 1000; i++)
            {
                dic.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            }
            var path = "my-secret";
            var mountPoint = "kv";
            await vaultClientNew.V1.Secrets.KeyValue.V1.WriteSecretAsync(path, dic, mountPoint);
            var d = vaultClientNew.V1.Auth;

            var kv2Secret = await vaultClientNew.V1.Secrets.KeyValue.V1.ReadSecretAsync(path, mountPoint);
            foreach (var data in kv2Secret.Data)
            {
                Console.WriteLine($" Key :  {data.Key}  ,  Value :  {data.Value} ");

            }
            // Generate a dynamic Consul credential
            //var database = await vaultClient.V1.Secrets.Database.GetCredentialsAsync("my-role");
            //var consulToken = database.Data.Password;
            //Console.WriteLine($" Username :  {database.Data.Username}  ,  Password :  {database.Data.Password} ");
        }
    }
}
