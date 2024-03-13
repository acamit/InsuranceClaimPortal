using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace YCompany.Configurations
{
    public class SecretManagerConfigurationSource : IConfigurationSource
    {
        private readonly IAmazonSecretsManager _client;
        private readonly string _secretName;
        private readonly string _region;

        public SecretManagerConfigurationSource(IAmazonSecretsManager client, string secretName, string region)
        {
            _client = client;
            _secretName = secretName;
            _region = region;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new SecretManagerConfigurationProvider(_client, _secretName, _region);
        }
    }
}
