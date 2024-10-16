using Avalonia;
using Avalonia.Themes.Fluent;
using Avalonia.LinuxFramebuffer;

namespace TinyOS.Avalonia;

public class Program
{        
    // sudo echo 0 > /sys/class/backlight/*/bl_power
    // sudo echo 0 > /sys/class/graphics/fbcon/cursor_blink
    
    public static int Main(string[] args)
    {   
        var builder = AppBuilder.Configure<App>()
            .With(new LinuxFramebufferPlatformOptions
            {
                Fps = 30
            })
            .AfterSetup(b => b.Instance?.Styles.Add(new FluentTheme()))
            .WithInterFont()
            .LogToTrace();

        return builder.StartLinuxFbDev(args);
    }
}

// public static partial class AppBuilderExtensions
// {
//     public static AppBuilder UseFluentTheme(
//         this AppBuilder builder, 
//         ThemeVariant? themeVariant = null) 
//     {
//         return builder.AfterSetup(_ =>
//         {
//             builder.Instance?.Styles.Add(new FluentTheme());
//             if (themeVariant is { } && builder.Instance is { })
//             {
//                 builder.Instance.RequestedThemeVariant = themeVariant;
//             }
//         });
//     }
// }
