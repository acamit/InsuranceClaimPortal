using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace YCompany.Claims.Logging
{
    public class YCompanyLoggingProvider : ILoggerProvider
    {
        private YCompanyLoggingProviderConfiguration _currentConfig;
        private readonly IDisposable? _onChangeToken;
        public YCompanyLoggingProvider(IOptionsMonitor<YCompanyLoggingProviderConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new YCompanyLogger(categoryName, GetCurrentConfig);
        }
        private YCompanyLoggingProviderConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _onChangeToken?.Dispose();
        }
    }
}
