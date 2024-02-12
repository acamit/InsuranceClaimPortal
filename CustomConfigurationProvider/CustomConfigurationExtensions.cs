using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace YCompany.Configurations
{
    public static class CustomConfigurationExtensions
    {
        public static IConfigurationBuilder AddSecurityConfiguration
        (this IConfigurationBuilder builder)
        {
            return builder.Add(new CustomConfigurationSource());
        }
    }
}
