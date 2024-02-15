using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace YCompany.Configurations
{
    public class SecretManagerConfigurationProvider : ConfigurationProvider
    {
        public override void Load()
        {
            var text = File.ReadAllText("config.json");
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var content = JsonSerializer.Deserialize<SecretManagerConfigurationSecurityMetadata>(text, options);

            if (content != null)
            {
                Data = new Dictionary<string, string>
                {
                    { "Key1", content.Key1 },
                    { "Key2", content.Key2 }
                };
            }
        }

    }
}
