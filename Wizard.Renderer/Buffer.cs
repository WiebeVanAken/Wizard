﻿using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace Wizard.Renderer;

public class Buffer<T> : IDisposable where T : unmanaged
{
    private readonly int _handle;
    private BufferUsage _bufferUsage;
    private T[] _data;
    
    public int ByteSize { get; } = Marshal.SizeOf<T>();
    public T[] Data => _data;
    public BufferTarget BufferTarget { get; }
    internal bool NeedsBuffering { get; private set; } = true;
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
    
    public T this[int index]
    {
        get => _data[index];
        set
        {
            _data[index] = value;
            NeedsBuffering = true;
        }
    }
    
    public Buffer(BufferTarget bufferTarget, BufferUsage bufferUsage, T[] data)
    {
#if GL_RUNTIME
        _handle = GL.GenBuffer();
#endif
        _bufferUsage = bufferUsage;
        _data = data;
        BufferTarget = bufferTarget;
    }

    public void BufferData()
    {
        if (!NeedsBuffering)
        {
            return;
        }
        
#if GL_RUNTIME
        GL.BindBuffer(BufferTarget, _handle);
        GL.BufferData(BufferTarget, ByteSize * _data.Length, _data, BufferUsage);
#endif
        NeedsBuffering = false;
    }
    
    public void Bind()
    {
        if (NeedsBuffering)
        {
            BufferData();
        }

#if GL_RUNTIME
        GL.BindBuffer(BufferTarget, _handle);
#endif
    }

    private void ReleaseUnmanagedResources()
    {
#if GL_RUNTIME
        GL.DeleteBuffer(_handle);
#endif
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