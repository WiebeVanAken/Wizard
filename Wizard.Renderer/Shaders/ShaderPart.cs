using OpenTK.Graphics.OpenGL.Compatibility;
using Wizard.Renderer.Exceptions;

namespace Wizard.Renderer.Shaders;

public class ShaderPart : IDisposable
{
    public int Handle { get; private set; }
    public ShaderType Type { get; }
    public bool Compiled { get; private set; } = false;
    internal string FilePath { get; }
    
    public ShaderPart(string filePath, ShaderType type)
    {
        FilePath = filePath;
        Type = type;
    }

    public void Compile()
    {
        Handle = GL.CreateShader(Type);
        
        var shaderSource = File.ReadAllText(FilePath);
        GL.ShaderSource(Handle, shaderSource);
        GL.CompileShader(Handle);
        GL.GetShaderi(Handle, ShaderParameterName.CompileStatus, out int compileSuccess);

        if (compileSuccess != 1)
        {
            var exception = ShaderCompilationExceptionBuilder.Build(this);
            throw exception;
        }
        
        Compiled = true;
    }

    public void Dispose()
    {
        GL.DeleteShader(Handle);
    }
}