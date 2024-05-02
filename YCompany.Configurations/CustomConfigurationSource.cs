using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace YCompany.Configurations
{
    public class CustomConfigurationSource : IConfigurationSource
    {
        private readonly string _customFilePath;

        public CustomConfigurationSource(string customFilePath)
        {
            _customFilePath = customFilePath;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CustomConfigurationProvider(_customFilePath);
        }
    }
}
