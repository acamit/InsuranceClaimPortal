using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace YCompany.Configurations
{
    public class CustomConfigurationProvider : ConfigurationProvider
    {
        private readonly string _customFilePath;

        public CustomConfigurationProvider(string customFilePath)
        {
            _customFilePath = customFilePath;
        }

        public override void Load()
        {
            if (File.Exists(_customFilePath))
            {
                var fileContents = File.ReadAllText(_customFilePath);

                // if the file is in json format we will use this type of code
                var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, string>>(fileContents);

                if (keyValuePairs != null)
                {
                    foreach (var kvp in keyValuePairs)
                    {
                        Data[kvp.Key] = kvp.Value;
                    }
                }
            }
            else
            {
                throw new FileNotFoundException($"Configuration file not found at path: {_customFilePath}");
            }
        }
    }
}
