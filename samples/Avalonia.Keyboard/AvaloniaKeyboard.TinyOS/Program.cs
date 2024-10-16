using System;
using System.Linq;
using Avalonia;

namespace AvaloniaKeyboard.Desktop;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp().StartLinuxFbDev(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .WithInterFont()
            .LogToTrace();

}
