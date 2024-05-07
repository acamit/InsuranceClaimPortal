using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace YCompany.Configurations
{
    public class SecretManagerConfigurationProvider : ConfigurationProvider
    {
        private IAmazonSecretsManager _client;
        private string _secretName;
        private string _region;

        public SecretManagerConfigurationProvider(IAmazonSecretsManager client, string secretName, string region)
        {
            _client = client;
            _secretName = secretName;
            _region = region;
        }

        public override async void Load()
        {
            var getSecretValueRequest = new GetSecretValueRequest()
            {
                SecretId = _secretName,
                VersionStage = "AWSCURRENT"
            };

            try
            {
                var response = await _client.GetSecretValueAsync(getSecretValueRequest);
                var secretString = response.SecretString;
                //var text = File.ReadAllText("config.json");
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var content = JsonSerializer.Deserialize<SecretManagerConfigurationSecurityMetadata>(secretString, options);

                if (content != null)
                {
                    Data = new Dictionary<string, string>
                    {
                        { "Key1", content.Key1 },
                        { "Key2", content.Key2 }
                    };
                }
            }
            catch (ResourceNotFoundException)
            {
                Console.WriteLine("secret doesn't exist");
            }
            catch (InvalidRequestException)
            {
                Console.WriteLine("the request is invalid");
            }
            catch (InvalidParameterException)
            {
                Console.WriteLine("the request parameters are invalid");
            }
            catch (DecryptionFailureException)
            {
                Console.WriteLine("the secret can't be decrypted");
            }
            catch (InternalServiceErrorException)
            {
                Console.WriteLine("internal service error");
            }
        }
    }
}
