using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace YCompany.Configurations
{
    public class SecretManagerConfigurationProvider : ConfigurationProvider
    {
        private readonly string _region;
        private readonly string _secretName;

        public SecretManagerConfigurationProvider(string region, string secretName) 
        {
            _region= region;
            _secretName= secretName;
        }

        public override async void Load()
        {
            var secret = await GetSecret();

            Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(secret);
        }

        private async Task<string> GetSecret()
        {
            string secretName = "MyInsuranceSecret";
            string region = "eu-north-1";

            AWSCredentials credentials = new BasicAWSCredentials("AKIA5FTY7KYPEVVHQSIX", "qie9fAjPbOcUozFZs8I0Qw2OZY0hD+criWtm9yn5");
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(credentials, RegionEndpoint.GetBySystemName(region));


            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT", 
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
