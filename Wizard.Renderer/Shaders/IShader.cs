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

    void IDisposable.Dispose()
    {
#if GL_RUNTIME
        GL.DeleteProgram(Handle);
#endif
        GC.SuppressFinalize(this);
    }

    public void SetUniformInt(string name, in int value)
    {
        var location = GL.GetUniformLocation(Handle, name);
        
        GL.Uniform1i(location, value);
    }

    // public void SetUniform<T>(string name, in T value) => value switch
    // {
    //     int i => GL.Uniform1i(GetUniformLocation(name), i),
    //     _ => throw new InvalidOperationException()
    // };
    //
    // private int GetUniformLocation(string name)
    // {
    //     return GL.GetUniformLocation(Handle, name);
    // }
}