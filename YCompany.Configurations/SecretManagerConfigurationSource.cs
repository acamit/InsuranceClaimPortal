using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace YCompany.Configurations
{
    internal class SecretManagerConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new SecretManagerConfigurationProvider();
        }
    }
}
