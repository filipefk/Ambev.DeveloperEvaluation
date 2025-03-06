using Microsoft.Extensions.Configuration;

namespace Ambev.DeveloperEvaluation.ORM.Extensions
{
    public static class ConfigurationExtension
    {
        public static bool IsUnitTestEnviroment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("InMemoryTest");
        }

    }
}
