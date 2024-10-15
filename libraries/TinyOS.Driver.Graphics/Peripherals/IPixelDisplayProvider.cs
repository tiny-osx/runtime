namespace TinyOS.Peripherals.Displays;

/// <summary>
/// Represents an object that can create IPixelDisplays
/// </summary>
public interface IPixelDisplayProvider
{
    /// <summary>
    /// Creates an IPixelDisplay with the provided (or default) parameters
    /// </summary>
    /// <param name="width">The desired display width</param>
    /// <param name="height">The desired display height</param>
    /// <returns>An instance of an IPixelDisplay</returns>
    public IResizablePixelDisplay CreateDisplay(int? width = null, int? height = null);
}
