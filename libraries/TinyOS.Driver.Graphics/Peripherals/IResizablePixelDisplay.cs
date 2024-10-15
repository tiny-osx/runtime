namespace TinyOS.Peripherals.Displays;

/// <summary>
/// Represents a resizable pixel based graphics display
/// </summary>
public interface IResizablePixelDisplay : IPixelDisplay
{
    /// <summary>
    /// Rsizes the display
    /// </summary>
    /// <param name="width">The new display width</param>
    /// <param name="height">The new display height</param>
    /// <param name="displayScale">The new display height</param>
    public void Resize(int width, int height, float displayScale = 1);
}
