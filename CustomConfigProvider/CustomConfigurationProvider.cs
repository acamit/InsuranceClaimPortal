using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using CustomConfigProvider;
using Microsoft.Extensions.Configuration;

namespace YCompany.Configurations
{
    public class CustomConfigurationProvider :ConfigurationProvider
    {
        public async Task<string> GetSecrets()
        {
            var secretsService = new SecretsManagerService("AKIATODEJPR4HSRAM5UQ", "7u0tKwy46NWtjdzT3cu0v8J0Ym0EOuvDdRzMjgoj", "ap-south-1");
            string secretValue = await secretsService.GetSecretValueByNameAsync("ApiAccessKeys");
            return secretValue;
        }
        public async override void Load()
        {
           
            var text = File.ReadAllText(@"D:\SecurityMetadata.json");
            var options = new JsonSerializerOptions
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var content = JsonSerializer.Deserialize<SecurityMetaData>
           (text, options);
            if (content != null)
            {
                var Data = new Dictionary<string, string>
                {
                    {"ApiKey", content.ApiKey},
                    {"ApiSecret", content.ApiSecret}
                };
            }
        }

    }
}
