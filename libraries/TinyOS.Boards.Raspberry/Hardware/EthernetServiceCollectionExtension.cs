using Microsoft.Extensions.DependencyInjection;

namespace TinyOS.Boards
{
    public static class EthernetServiceCollectionExtension
    {
        public static IServiceCollection AddEthernet(
            this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException();
            }

            return services;
        }
    }
}