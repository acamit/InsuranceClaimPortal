using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace YCompany.Configurations
{
    public class SecretManagerConfigurationSource : IConfigurationSource
    {
        private readonly string _region;
        private readonly string _secretName;

        public SecretManagerConfigurationSource(string region, string secretName)
        {
            _region = region;
            _secretName = secretName;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new SecretManagerConfigurationProvider(_region, _secretName);
        }
    }
}
