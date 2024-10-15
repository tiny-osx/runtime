namespace TinyOS.Peripherals.Displays;

/// <summary>
/// Defines a Text Display
/// </summary>
public interface ITextDisplay : IDisplay
{
    /// <summary>
    /// Gets a Display Configuration
    /// </summary>
    TextDisplayConfig DisplayConfig { get; }

    /// <summary>
    /// Writes the specified string on the display
    /// </summary>
    /// <param name="text">String to display</param>
    void Write(string text);

    /// <summary>
    /// Writes the specified string to the specified line number on the display
    /// </summary>
    /// <param name="text">String to display.</param>
    /// <param name="lineNumber">Line Number.</param>
    /// <param name="showCursor">Show the cursor.</param>
    void WriteLine(string text, byte lineNumber, bool showCursor = false);

    /// <summary>
    /// Clears all ITextDisplay lines of text
    /// </summary>
    void ClearLines();

    /// <summary>
    /// Clears the specified line of characters on the display
    /// </summary>
    /// <param name="lineNumber">Line Number</param>
    void ClearLine(byte lineNumber);

    /// <summary>
    /// Set cursor in the specified row and column
    /// </summary>
    /// <param name="column"></param>
    /// <param name="line"></param>
    void SetCursorPosition(byte column, byte line);

    /// <summary>
    /// Update the display, not used by character displays
    /// </summary>
    void Show();
}