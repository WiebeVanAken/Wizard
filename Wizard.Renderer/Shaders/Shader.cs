using OpenTK.Graphics.OpenGL;

namespace Wizard.Renderer.Shaders;
    
public class Shader(int handle) : IDisposable
{
    public int Handle { get; } = handle;
    
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

    private void ReleaseUnmanagedResources()
    {
#if GL_RUNTIME
        GL.DeleteProgram(Handle);
#endif
    }
    
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Shader()
    {
        ReleaseUnmanagedResources();
    }
}