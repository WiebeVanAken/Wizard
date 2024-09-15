using OpenTK.Graphics.OpenGL;

namespace Wizard.Renderer;

public class VertexBufferObject
{
    public int Handle { get; private set; }
    public BufferUsage BufferUsage { get; private set; }
    public double[] DataBuffer { get; private set; }

    public VertexBufferObject(double[] dataBuffer, BufferUsage bufferUsage)
    {
        Handle = GL.GenBuffer();
        BufferUsage = bufferUsage;
        DataBuffer = dataBuffer;
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
        GL.BufferData(BufferTarget.ArrayBuffer, DataBuffer.Length * sizeof(double), dataBuffer, BufferUsage);
    }
}