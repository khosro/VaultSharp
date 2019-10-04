using System;
using System.Collections.Generic;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.AppRole;

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
            // Initialize one of the several auth methods.
            // authMethod = new TokenAuthMethodInfo("s.6DYy3mX1U5nvrPItdoLsRWaH");

            authMethod = new AppRoleAuthMethodInfo("2d313f4c-214e-454f-efd0-3cb602f19305");
    
            // Initialize settings. You can also set proxies, custom delegates etc. here.
            var vaultClientSettings = new VaultClientSettings("http://localhost:8200", authMethod);

            IVaultClient vaultClient = new VaultClient(vaultClientSettings);

            // Use client to read a key-value secret.
            // var kv2Secret = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync("secret-name");
            var dic = new Dictionary<string, object>();
            for (var i = 0; i < 1000; i++)
            {
                dic.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            }
            var path = "my-secret";
            var mountPoint = "kv";
            await vaultClient.V1.Secrets.KeyValue.V1.WriteSecretAsync(path, dic, mountPoint);

            var kv2Secret = await vaultClient.V1.Secrets.KeyValue.V1.ReadSecretAsync(path, mountPoint);
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
