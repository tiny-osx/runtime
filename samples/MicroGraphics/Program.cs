using TinyOS.Devices.Displays;

namespace TinyOS.MicroGraphics;

public class Program
{        
    private static void Main(string[] args)
    {
        using (var display = new FrameBufferDisplay())
        {                
            display.Clear(true);
            display.Enabled();
            
            for (int x = 0; x < display.Width; x++)
            {
                for (int y = 0; y < display.Height; y++)
                {
                    int a = x / 32;
                    int b = y / 32;

                    if (((a + b) % 2) == 0)
                    {
                        display.DrawPixel(x, y, Color.Blue);
                    }
                    else
                    {
                        display.DrawPixel(x, y, Color.Orange);
                    }
                }
            }

            display.Show();
        }
    }
}