using Microsoft.Extensions.Configuration;


namespace YCompany.Configurations
{
    public static class SecretManagerConfigurationExtensions
    {
        public static IConfigurationBuilder AddSecurityConfiguration
        (this IConfigurationBuilder builder)
        {
            return builder.Add(new SecretManagerConfigurationSource());
        }
    }
}
