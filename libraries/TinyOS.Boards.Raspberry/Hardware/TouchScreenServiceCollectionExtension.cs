
using Microsoft.Extensions.DependencyInjection;

namespace TinyOS.Boards
{
    public static class TouchScreenServiceCollectionExtension
    {
        public static IServiceCollection AddTouchScreen(
            this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException();
            }

            services.AddSingleton<ITouchScreenService, TouchScreenService>();

            return services;
        }
    }

    public interface ITouchScreenService : IDisposable
    {
        int Width { get; }
        int Height { get; }
        void Enable();
        void Disable();
    }

    public class TouchScreenService : ITouchScreenService
    {
        public int Width;
        public int Height;

        int ITouchScreenService.Width => throw new NotImplementedException();

        int ITouchScreenService.Height => throw new NotImplementedException();


        public TouchScreenService()
        {
        }

        public void Enable()
        {

        }

        public void Disable()
        {

        }

        public void Dispose()
        {
            Disable();
        }
    }
}