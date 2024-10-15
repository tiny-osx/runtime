using TinyOS.Peripherals.Displays;

using System.Diagnostics;
using System.Text;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using TinyOS.Devices.Graphics.Buffers;



namespace TinyOS.Devices.Displays;

/// <summary>
/// Represents a display driver for an ASCII console
/// </summary>
public partial class FrameBufferDisplay : IPixelDisplay, IDisposable
{
    private MemoryMappedFile? _fbMmFile = null;

    private MemoryMappedViewStream? _fbStream = null;

    private object _mutex = new object();

    private bool _disposedValue = false;

    private const string DEFAULT_DISPLAY_DEVICE = "/dev/fb0";

    private IPixelBuffer _pixelBuffer = default;

    public string Device { get; private set; }

    public string? Id { get; private set; }

    public ColorMode ColorMode => _pixelBuffer.ColorMode;

    public ColorMode SupportedColorModes => 
         ColorMode.Format16bppRgb565 | ColorMode.Format24bppRgb888 | ColorMode.Format32bppRgba8888 ;
    
    public int Depth { get; private set; } = 0;

    public int Width { get; private set; } = 0;

    public int Height { get; private set; } = 0;

    public IPixelBuffer PixelBuffer => _pixelBuffer;

    public FrameBufferDisplay(string device) : this(device, true)
    {
    }

    public FrameBufferDisplay() : this(DEFAULT_DISPLAY_DEVICE, true)
    {
    }

    public FrameBufferDisplay(string device, int width, int height, ColorMode mode)
    {
        Device = device ?? Environment.GetEnvironmentVariable("FRAMEBUFFER") ?? DEFAULT_DISPLAY_DEVICE;
        Initialize(width, height, mode);
    }

    public FrameBufferDisplay(string device, bool autoprobe)
    {
        Device = device ?? Environment.GetEnvironmentVariable("FRAMEBUFFER") ?? DEFAULT_DISPLAY_DEVICE;

        if (autoprobe)
        {
            RefreshDeviceInfo();
        }
    }

    public void RefreshDeviceInfo()
    {
        using (var fileHandle = File.OpenHandle(Device, FileMode.Open, FileAccess.ReadWrite, FileShare.None, FileOptions.None, 0))
        {
            Interop.FrameBuffer.FixScreenInfo fixed_info = new Interop.FrameBuffer.FixScreenInfo();
            Interop.FrameBuffer.VarScreenInfo variable_info = new Interop.FrameBuffer.VarScreenInfo();

            if (Interop.Libc.IoCtl(fileHandle.DangerousGetHandle().ToInt32(), Interop.FrameBuffer.FBIOGET_FSCREENINFO, ref fixed_info) < 0)
            {
                // TODO: switch to PInvoke
                // string err = Marshal.GetLastPInvokeErrorMessage();
                // return string.IsNullOrWhiteSpace(err) ? string.Empty : $"Error: '{err}'";
                
                Debug.WriteLine($"Probe FrameBuffer ioctl({Interop.FrameBuffer.FBIOGET_FSCREENINFO}) error: {Marshal.ReadInt32(Interop.Libc.ErrnoLocation())}");
            }
            else
            {
                Id = Encoding.ASCII.GetString(fixed_info.id).TrimEnd(new char[] { '\r', '\n', ' ', '\0' });
                Debug.WriteLine($"Display memory for {Id} starts at: {fixed_info.smem_start} length: {fixed_info.smem_len}");
            }

            if (Interop.Libc.IoCtl(fileHandle.DangerousGetHandle().ToInt32(), Interop.FrameBuffer.FBIOGET_VSCREENINFO, ref variable_info) < 0)
            {
                Debug.WriteLine($"ProbeFrameBuffer ioctl({Interop.FrameBuffer.FBIOGET_VSCREENINFO}) error: {Marshal.ReadInt32(Interop.Libc.ErrnoLocation())}");
            }
            else
            {
                Debug.WriteLine($"Actual width => {variable_info.xres} height => {variable_info.yres} bpp => {variable_info.bits_per_pixel}");
                
                Depth = variable_info.bits_per_pixel;
                Width = variable_info.xres;
                Height = variable_info.yres;
                
                var mode = GetColorMode(variable_info);
                
                Initialize(Width, Height, mode);

                return;
           }

            throw new Exception ("Failed to autoprobe the display");
        }
    }

    private ColorMode GetColorMode(Interop.FrameBuffer.VarScreenInfo screenInfo)
    {
        var width = screenInfo.xres;
        var height = screenInfo.yres;
        var bpp = screenInfo.bits_per_pixel;
        var red = screenInfo.red;
        var green = screenInfo.green;
        var blue = screenInfo.blue;
        var transp = screenInfo.transp;

        Debug.WriteLine($"w:{width} h:{height} bpp:{bpp} r:{red.length}/{red.offset} g:{green.length}/{green.offset} b:{blue.length}/{blue.offset} a:{transp.length}/{transp.offset}");

        switch(bpp)
        {
            case 16:
                return ColorMode.Format16bppRgb565;
            case 24:
                return ColorMode.Format24bppRgb888;
            case 32:
                return ColorMode.Format32bppRgba8888;
        }
        
        throw new Exception ($"Autoprobe could not identify color mode (bpp:{bpp},r:{red.length}/{red.offset},g:{green.length}/{green.offset},b:{blue.length}/{blue.offset}),a:{transp.length}/{transp.offset})");
    }

    private void Initialize(int width, int height, ColorMode mode)
    {
        switch (mode)
        {
            case ColorMode.Format16bppRgb565:
                _pixelBuffer = new BufferRgb565(width, height);
                break;
            case ColorMode.Format24bppRgb888:
                _pixelBuffer = new BufferRgb888(width, height);
                break;
            case ColorMode.Format32bppRgba8888:
                _pixelBuffer = new BufferRgba8888(width, height);
                break;
            default:
                throw new Exception($"Mode {mode} not supported");
        }
    }

    public virtual void Clear(Color? clear)
    {
        if (clear.HasValue)
        {
            Fill(clear.Value);
            return;
        }

        Clear();
    }

    public void Clear(bool updateDisplay = false)
    {
        _pixelBuffer.Clear();

        if (updateDisplay)
        {
            Show();
        }
    }

    public void DrawPixel(int x, int y, Color color)
    {
        _pixelBuffer.SetPixel(x, y, color);
    }

    public void DrawPixel(int x, int y, bool enabled)
    {
        DrawPixel(x, y, enabled);
    }

    public void Fill(Color fillColor, bool updateDisplay = false)
    {
        _pixelBuffer.Fill(fillColor);

        if (updateDisplay)
        {
            Show();
        }
    }

    public void Fill(int x, int y, int width, int height, Color fillColor)
    {
        _pixelBuffer.Fill(x, y, width, height, fillColor);
    }

    public void InvertPixel(int x, int y)
    {
        _pixelBuffer.InvertPixel(x, y);
    }

    public void WriteBuffer(int x, int y, IPixelBuffer displayBuffer)
    {
        _pixelBuffer.WriteBuffer(x, y, displayBuffer);
    }

    public void Show()
    {
        EnsureOpenStream();

        int len = (int)Math.Min(_pixelBuffer.ByteCount, _fbStream!.Length);
        _fbStream?.Seek(0, SeekOrigin.Begin);

        // Debug.WriteLine($"Writing to FB - Stream pos: {_fbStream?.Position} Length: {_fbStream?.Length} Buffer Size: {_pixelBuffer.Buffer.Length} Writing len: {len}");

        _fbStream?.Write(_pixelBuffer.Buffer, 0, len);
        _fbStream?.Flush();
    }

    public void Show(int left, int top, int right, int bottom)
    {
        throw new NotImplementedException();
    }
    
    public void Enabled()
    {
        IoCtlBlanking(Interop.FrameBuffer.FB_BLANK_UNBLANK);
        Task.Delay(500).Wait();
    }

    public void Disable()
    {
        IoCtlBlanking(Interop.FrameBuffer.FB_BLANK_POWERDOWN);
    }

    private void IoCtlBlanking(int arg)
    {
        EnsureOpenStream();

        if (Interop.Libc.IoCtl(_fbMmFile!.SafeMemoryMappedFileHandle.DangerousGetHandle().ToInt32(), Interop.FrameBuffer.FBIOBLANK, arg) < 0)
        {
            Debug.WriteLine($"Blanking ioctl({ Interop.FrameBuffer.FBIOBLANK}) arg({arg}) error: {Marshal.ReadInt32(Interop.Libc.ErrnoLocation())}");
        }
    }

    private void EnsureOpenStream()
    {
        try
        {
            lock (_mutex)
            {
                if (_fbMmFile == null)
                {
                    _fbMmFile = MemoryMappedFile.CreateFromFile(Device, FileMode.Open, null, Width * Height * (Depth / 8));
                    _fbStream = _fbMmFile.CreateViewStream();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to ensure framebuffer stream: ${ex}");
            throw;
        }
    }

    ~FrameBufferDisplay()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                if (_fbStream != null)
                {
                    _fbStream.Dispose();
                    _fbStream = null;
                }

                if (_fbMmFile != null)
                {
                    _fbMmFile.Dispose();
                    _fbMmFile = null;
                }
            }
            _disposedValue = true;
        }
    }
}