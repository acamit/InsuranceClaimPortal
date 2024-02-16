using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace YCompany.Configurations
{
    public class SecretManagerConfigurationProvider: ConfigurationProvider
    {
        public override void Load()
        {
            var text = File.ReadAllText("C:\\Users\\nitesh01\\source\\repos\\InsuranceClaimPortal\\YCompany.Configurations\\CustomSetting.json");
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var content = JsonSerializer.Deserialize<SecurityMetadata>(text, options);

            if (content != null)
            {
                Data = new Dictionary<string, string?>
                    {
                        { "ApiKey", content.ApiKey },
                        { "ApiSecret", content.ApiSecret }
                    };
            }

        }

    }
}
