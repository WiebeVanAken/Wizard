using OpenTK.Graphics.OpenGL;
using Wizard.Renderer.Exceptions;

namespace Wizard.Renderer.Shaders;

public class ShaderBuilder : IShaderBuilder
{
    internal Dictionary<ShaderType, string> ShaderParts { get; } = new();
    
    public IShaderBuilder AddShaderPart(ShaderType shaderType, string filePath)
    {
        ShaderParts.Add(shaderType, filePath);
        
        return this;
    }

    public IShader Build()
    {
        if (ShaderParts.Count == 0)
        {
            throw new InvalidOperationException("No shader parts defined.");
        }

        var partHandles = new List<int>(ShaderParts.Count);
        
#if GL_RUNTIME
        var handle = GL.CreateProgram();
        
        foreach (var part in ShaderParts)
        {
            var compiledPartHandle = CompileShaderPart(part.Key, part.Value);
            GL.AttachShader(handle, compiledPartHandle);
            partHandles.Add(compiledPartHandle);
        }
        
        GL.LinkProgram(handle);
        GL.GetProgrami(handle, ProgramProperty.LinkStatus, out var linkStatus);
        
        if (linkStatus == 0)
        {
            var exception = ShaderLinkingExceptionBuilder.Build(handle);
            throw exception;
        }
        
        foreach (var partHandle in partHandles)
        {
            GL.DetachShader(handle, partHandle);
            GL.DeleteShader(partHandle);
        }
        #else
        
        var handle = -1;
#endif
        
        return new Shader(handle);
    }

    private static int CompileShaderPart(ShaderType shaderType, string filePath)
    {
        var handle = GL.CreateShader(shaderType);
        
        var shaderSource = File.ReadAllText(filePath);
        GL.ShaderSource(handle, shaderSource);
        GL.CompileShader(handle);
        GL.GetShaderi(handle, ShaderParameterName.CompileStatus, out int compileSuccess);

        if (compileSuccess != 1)
        {
            var exception = ShaderCompilationExceptionBuilder.Build(handle, filePath);
            throw exception;
        }

        return handle;
    }
}