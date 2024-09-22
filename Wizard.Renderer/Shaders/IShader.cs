using OpenTK.Graphics.OpenGL;

namespace Wizard.Renderer.Shaders;

public interface IShader : IDisposable
{
    public int Handle { get; }

    public void Use()
    {
#if GL_RUNTIME
        GL.UseProgram(Handle);
#endif
    }

    public void SetUniformInt(string name, in int value)
    {
#if GL_RUNTIME
        var location = GL.GetUniformLocation(Handle, name);
        
        GL.Uniform1i(location, value);
#endif
    }
    
    void IDisposable.Dispose()
    {
#if GL_RUNTIME
        GL.DeleteProgram(Handle);
#endif
        GC.SuppressFinalize(this);
    }
}