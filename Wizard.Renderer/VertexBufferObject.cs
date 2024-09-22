using OpenTK.Graphics.OpenGL;

namespace Wizard.Renderer;

public class VertexBufferObject
{
    public int Handle { get; }
    public double[] DataBuffer { get; }
    private BufferUsage BufferUsage { get; }

    public VertexBufferObject(double[] dataBuffer, BufferUsage bufferUsage)
    {
        Handle = GL.GenBuffer();
        BufferUsage = bufferUsage;
        DataBuffer = dataBuffer;
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
        GL.BufferData(BufferTarget.ArrayBuffer, DataBuffer.Length * sizeof(double), dataBuffer, BufferUsage);
    }
}