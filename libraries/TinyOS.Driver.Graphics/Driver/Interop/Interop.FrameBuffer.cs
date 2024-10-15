#pragma warning disable IDE1006 // Naming Styles

using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class FrameBuffer
    {
        internal const int FBIOGET_FSCREENINFO = 0x4602;
        internal const int FBIOGET_VSCREENINFO = 0x4600;
        internal const int FBIOPUT_VSCREENINFO = 0x4601;
        internal const int FBIOBLANK = 0x4611;
        internal const int FB_BLANK_UNBLANK = 0x0;
        internal const int FB_BLANK_POWERDOWN = 0x4;
        
        // internal enum FbIoCtl : uint
        // {
        //     FBIOGET_VSCREENINFO = 0x4600,
        //     FBIOPUT_VSCREENINFO = 0x4601,
        //     FBIOGET_FSCREENINFO = 0x4602,
        //     // FBIOGET_VBLANK = 0x80204612u,
        //     //FBIO_WAITFORVSYNC = 0x40044620,
        //     //FBIOPAN_DISPLAY = 0x4606,
        //     FBIOBLANK = 0x4611,
        //     FB_BLANK_UNBLANK = 0x0,
        //     FB_BLANK_POWERDOWN = 0x4,
        // }

        [StructLayout(LayoutKind.Sequential)]
        internal struct FixScreenInfo
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] id;

            [MarshalAs(UnmanagedType.U4)]
            public int smem_start;

            [MarshalAs(UnmanagedType.U4)]
            public int smem_len;

            [MarshalAs(UnmanagedType.U4)]
            public int type;

            [MarshalAs(UnmanagedType.U4)]
            public int type_aux;

            [MarshalAs(UnmanagedType.U4)]
            public int visual;

            [MarshalAs(UnmanagedType.U2)]
            public short xpanstep;

            [MarshalAs(UnmanagedType.U2)]
            public short ypanstep;

            [MarshalAs(UnmanagedType.U2)]
            public short ywrapstep;

            [MarshalAs(UnmanagedType.U4)]
            public int line_length;

            [MarshalAs(UnmanagedType.U4)]
            public int mmio_start;

            [MarshalAs(UnmanagedType.U4)]
            public int mmio_len;

            [MarshalAs(UnmanagedType.U4)]
            public int accel;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public short[] reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BitField
        {
            [MarshalAs(UnmanagedType.U4)]
            public int offset;

            [MarshalAs(UnmanagedType.U4)]
            public int length;

            [MarshalAs(UnmanagedType.U4)]
            public int msb_right;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VarScreenInfo
        {
            [MarshalAs(UnmanagedType.U4)]
            public int xres;

            [MarshalAs(UnmanagedType.U4)]
            public int yres;

            [MarshalAs(UnmanagedType.U4)]
            public int xres_virtual;

            [MarshalAs(UnmanagedType.U4)]
            public int yres_virtual;

            [MarshalAs(UnmanagedType.U4)]
            public int xoffset;

            [MarshalAs(UnmanagedType.U4)]
            public int yoffset;

            [MarshalAs(UnmanagedType.U4)]
            public int bits_per_pixel;

            [MarshalAs(UnmanagedType.U4)]
            public int grayscale;

            public BitField red;

            public BitField green;

            public BitField blue;

            public BitField transp;

            [MarshalAs(UnmanagedType.U4)]
            public int nonstd;

            [MarshalAs(UnmanagedType.U4)]
            public int activate;

            [MarshalAs(UnmanagedType.U4)]
            public int height;

            [MarshalAs(UnmanagedType.U4)]
            public int width;

            [MarshalAs(UnmanagedType.U4)]
            public int accel_flags;

            [MarshalAs(UnmanagedType.U4)]
            public int pixclock;

            [MarshalAs(UnmanagedType.U4)]
            public int left_margin;

            [MarshalAs(UnmanagedType.U4)]
            public int right_margin;

            [MarshalAs(UnmanagedType.U4)]
            public int upper_margin;

            [MarshalAs(UnmanagedType.U4)]
            public int lower_margin;

            [MarshalAs(UnmanagedType.U4)]
            public int hsync_len;

            [MarshalAs(UnmanagedType.U4)]
            public int vsync_len;

            [MarshalAs(UnmanagedType.U4)]
            public int sync;

            [MarshalAs(UnmanagedType.U4)]
            public int vmode;

            [MarshalAs(UnmanagedType.U4)]
            public int rotate;

            [MarshalAs(UnmanagedType.U4)]
            public int colorspace;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] reserved;
        }
    }
}