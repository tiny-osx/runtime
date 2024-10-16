using Avalonia;

namespace TinyOS.Avalonia;

public class Program
{        
    // sudo echo 0 > /sys/class/backlight/*/bl_power
    // sudo echo 0 > /sys/class/graphics/fbcon/cursor_blink
    
    public static int Main(string[] args)
    {   
        var builder = BuildAvaloniaApp();            
        return builder.StartLinuxFbDev(args);
        //return builder.StartLinuxDrm(args, card: "/dev/dri/card1", scaling: 1);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .LogToTrace();
}
