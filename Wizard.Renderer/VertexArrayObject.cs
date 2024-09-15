using OpenTK.Graphics.OpenGL.Compatibility;

namespace Wizard.Renderer;

public class VertexArrayObject : IDisposable
{
    public int Handle { get; }

    public VertexArrayObject(VertexBufferObject vertexBufferObject)
    {
        Handle = GL.GenVertexArray();
        GL.BindVertexArray(Handle);
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject.Handle);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBufferObject.DataBuffer.Length * sizeof(double), vertexBufferObject.DataBuffer, BufferUsage.StaticDraw);
        
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Double, false, 3 * sizeof(double), 0);
        GL.EnableVertexAttribArray(0);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(Handle);
    }
}