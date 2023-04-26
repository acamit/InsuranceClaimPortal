using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace YCompany.Claims.Logging
{
    public static class YCompanyLoggingProviderExtensions
    {
        public static ILoggingBuilder AddYCompanyLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, YCompanyLoggingProvider>());

            LoggerProviderOptions.RegisterProviderOptions<YCompanyLoggingProviderConfiguration, YCompanyLoggingProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddYCompanyLogger
            (this ILoggingBuilder builder,
            Action<YCompanyLoggingProviderConfiguration> configuration)
        {
            builder.AddYCompanyLogger();
            builder.Services.Configure(configuration);

            return builder;
        }
    }
}
