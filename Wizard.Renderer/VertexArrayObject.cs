using OpenTK.Graphics.OpenGL.Compatibility;

namespace Wizard.Renderer;

public class VertexArrayObject : IDisposable
{
    public int Handle { get; }

    public VertexArrayObject(Buffer<double> vertexBufferObject)
    {
        Handle = GL.GenVertexArray();
        GL.BindVertexArray(Handle);
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject.Handle);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBufferObject.Data.Length * vertexBufferObject.ByteSize, vertexBufferObject.Data, BufferUsage.StaticDraw);
        
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Double, false, 6 * sizeof(double), 0);
        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Double, false, 6 * sizeof(double), 3 * sizeof(double));
        GL.EnableVertexAttribArray(1);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(Handle);
    }
}