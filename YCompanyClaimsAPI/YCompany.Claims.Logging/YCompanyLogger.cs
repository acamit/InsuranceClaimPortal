using Microsoft.Extensions.Logging;

namespace YCompany.Claims.Logging
{
    public class YCompanyLogger : ILogger
    {
        private string categoryName;
        private Func<YCompanyLoggingProviderConfiguration> getCurrentConfig;

        public YCompanyLogger(string categoryName, Func<YCompanyLoggingProviderConfiguration> getCurrentConfig)
        {
            this.categoryName = categoryName;
            this.getCurrentConfig = getCurrentConfig;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (IsEnabled(logLevel))
            {
                // send log to queue for open search processing.
            }
        }
    }
}
