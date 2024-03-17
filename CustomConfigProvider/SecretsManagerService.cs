using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Amazon.SecretsManager.Model;

namespace CustomConfigProvider
{
    public class SecretsManagerService
    {
        private readonly AmazonSecretsManagerClient _secretsManagerClient; 
        public SecretsManagerService(string accessKey, string secretKey, string region)
        {
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var config = new AmazonSecretsManagerConfig
            {
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region)
            }; 
            _secretsManagerClient = new AmazonSecretsManagerClient(credentials, config);
        }     // Add methods to retrieve secrets here

         public async Task<string> GetSecretValueByNameAsync(string secretName) 
        {
            var request = new GetSecretValueRequest { SecretId = secretName };
            var response = await _secretsManagerClient.GetSecretValueAsync(request);
            return response.SecretString;
        }
   
    }
}
