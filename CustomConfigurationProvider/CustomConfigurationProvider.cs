using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace YCompany.Configurations
{
    public class CustomConfigurationProvider :ConfigurationProvider
    {
        public override void Load()
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
