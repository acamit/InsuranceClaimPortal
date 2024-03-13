using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace YCompany.Configurations
{
    public class AmazonSecretsManagerConfigurationSource : IConfigurationSource
    {
        private readonly string _region;
        private readonly string _secretName;

        public AmazonSecretsManagerConfigurationSource(string region, string secretName)
        {
            _region = region;
            _secretName = secretName;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AmazonSecretsManagerConfigurationProvider(_region, _secretName);
        }
    }
}
