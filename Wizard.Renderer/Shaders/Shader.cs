using OpenTK.Graphics.OpenGL.Compatibility;
using Wizard.Renderer.Exceptions;

namespace Wizard.Renderer.Shaders;

// TODO: Write a ShaderBuilder for building complex shaders at runtime
public class Shader : IDisposable
{
    public int Handle { get; private set; }
    private Dictionary<ShaderType, ShaderPart> ShaderParts { get; } = new();
    private bool Compiled { get; set; }
    
    public void AddShaderPart(ShaderPart part)
    {
        ShaderParts.TryAdd(part.Type, part);
    }

    public void Compile()
    {
        if (Compiled)
        {
            return;
        }
        
        Handle = GL.CreateProgram();
        
        foreach (var part in ShaderParts.Values)
        {
            try
            {
                part.Compile();
                GL.AttachShader(Handle, part.Handle);
            }
            catch (ShaderCompilationException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        GL.LinkProgram(Handle);
        GL.GetProgrami(Handle, ProgramProperty.LinkStatus, out var linkStatus);

        if (linkStatus == 0)
        {
            var exception = ShaderLinkingExceptionBuilder.Build(this);
            throw exception;
        }
        
        foreach (var part in ShaderParts.Values)
        {
            GL.DetachShader(Handle, part.Handle);
            GL.DeleteShader(part.Handle);
        }
        
        Compiled = true;
    }

    public void Use()
    {
        if (!Compiled)
        {
            throw new InvalidOperationException("Shader must be compiled before calling Use()");
        }
        
        GL.UseProgram(Handle);
    }

    public void Dispose()
    {
        Console.WriteLine("Disposed");
        GL.DeleteProgram(Handle);
    }
}