using Microsoft.Extensions.Configuration;

namespace Ottobo.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static SensitiveDataConfiguration GetHasSensitiveDataConfiguration(this IConfiguration configuration)
        {
            SensitiveDataConfiguration sensitiveDataConfiguration = new SensitiveDataConfiguration();
            configuration.Bind("SensitiveDataConfiguration", sensitiveDataConfiguration);
            return sensitiveDataConfiguration;
        }
    }
}