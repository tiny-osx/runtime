#pragma warning disable IDE1006 // Naming Styles

using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Libc
    {
        [DllImport(Libraries.Libc, EntryPoint = "ioctl", SetLastError = true)]
        internal static extern int IoCtl(int handle, uint request, ref FrameBuffer.VarScreenInfo capability);

        [DllImport(Libraries.Libc, EntryPoint = "ioctl", SetLastError = true)]
        internal static extern int IoCtl(int handle, uint request, ref FrameBuffer.FixScreenInfo capability);

        [DllImport(Libraries.Libc, EntryPoint = "ioctl", SetLastError = true)]
        internal static extern int IoCtl(int handle, uint request, int arg);

        [DllImport(Libraries.Libc, EntryPoint = "__errno_location")]
        internal static extern nint ErrnoLocation();
    }
}