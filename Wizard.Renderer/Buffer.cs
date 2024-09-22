using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace Wizard.Renderer;

public class Buffer<T>(BufferTarget bufferTarget, BufferUsage bufferUsage, T[] data) : IDisposable where T : unmanaged
{
    private int _handle = GL.GenBuffer();
    private BufferUsage _bufferUsage = bufferUsage;
    private T[] _data = data;

    public int Handle
    {
        get
        {
            if (NeedsBuffering)
            {
                BufferData();
            }
            
            return _handle; 
        }
        private set => _handle = value;
    }
    
    public BufferUsage BufferUsage
    {
        get => _bufferUsage;
        set
        {
            _bufferUsage = value;
            NeedsBuffering = true;
        }
    }
    
    public int ByteSize { get; } = Marshal.SizeOf<T>();

    public T[] Data
    {
        get => _data;
        private set
        {
            NeedsBuffering = true;
            _data = value;
        }
    }

    public BufferTarget BufferTarget { get; } = bufferTarget;

    private bool NeedsBuffering { get; set; }

    public Buffer(BufferTarget bufferTarget, BufferUsage bufferUsage) : this(bufferTarget, bufferUsage, [])
    {
        _handle = GL.GenBuffer();
        BufferTarget = bufferTarget;
    }

    private void BufferData()
    {
        GL.BindBuffer(BufferTarget, _handle);
        GL.BufferData(BufferTarget, ByteSize * _data.Length, _data, BufferUsage);
        NeedsBuffering = false;
    }

    private void ReleaseUnmanagedResources()
    {
        GL.DeleteBuffer(_handle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Buffer()
    {
        ReleaseUnmanagedResources();
    }
}