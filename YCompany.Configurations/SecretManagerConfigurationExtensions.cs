using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace YCompany.Configurations
{
    public static class SecretManagerConfigurationExtensions
    {
        public static IConfigurationBuilder AddSecurityConfiguration
       (this IConfigurationBuilder builder, IAmazonSecretsManager client, string secretName, string region)
        {
            return builder.Add(new SecretManagerConfigurationSource(client, secretName, region));
        }
    }
}
