using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace YCompany.Configurations
{
   public class AmazonSecretsManagerConfigurationProvider : ConfigurationProvider
    {
        private readonly string _region;
        private readonly string _secretName;

        public AmazonSecretsManagerConfigurationProvider(string region, string secretName)
        {
            _region = region;
            _secretName = secretName;
        }

        public override async void Load()
        {
            var secret = await GetSecret();

            Data = JsonSerializer.Deserialize<Dictionary<string, string>>(secret);
        }
        private async Task<string> GetSecret()
        {
            string secretName = "my-key";
            string region = "eu-north-1";

            AWSCredentials credentials = new BasicAWSCredentials("AKIAYS2NUQSEQSBBZPPA", "uIDN9E+ZZh7nuV0UvmoGEMxfcnCJ8zVdxeY1xdgs");
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(credentials, RegionEndpoint.GetBySystemName(region));


            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
            };

            GetSecretValueResponse response;

            try
            {
                response = await client.GetSecretValueAsync(request);
            }
            catch (Exception e)
            {
                throw e;
            }

            string secret = response.SecretString;

            return secret;
        }
   }

}

