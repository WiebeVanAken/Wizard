using OpenTK.Graphics.OpenGL;

namespace Wizard.Renderer;

public class ElementBufferObject : IDisposable
{
    private int _handle;
    private BufferUsage _bufferUsage;
    private int[] _data;

    public int Handle
    {
        get
        {
            if (NeedsBuffering)
            {
                Buffer();
            }
            
            return _handle;
        }
        private set => _handle = value;
    }

    public BufferUsage BufferUsage
    {
        get
        {
            if (NeedsBuffering)
            {
                Buffer();
            }
            
            return _bufferUsage;
        }
        set
        {
            _bufferUsage = value;
            NeedsBuffering = true;
        }
    }

    public int[] Data
    {
        get => _data;
        private set
        {
            NeedsBuffering = true;
            _data = value;
        }
    }

    private bool NeedsBuffering { get; set; } = true;

    private void Buffer()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _handle);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _data.Length * sizeof(int), _data, _bufferUsage);
        NeedsBuffering = false;
    }

    public ElementBufferObject(int[] data, BufferUsage bufferUsage)
    {
        Handle = GL.GenBuffer();
        _data = data;
        _bufferUsage = bufferUsage;
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

    ~ElementBufferObject()
    {
        ReleaseUnmanagedResources();
    }
}