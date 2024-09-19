using OpenTK.Graphics.OpenGL;
using Wizard.Renderer.Exceptions;

namespace Wizard.Renderer.Shaders;

public class ShaderBuilder : IShaderBuilder
{
    private readonly Dictionary<ShaderType, string> _shaderParts = new();
    
    public IShaderBuilder AddShaderPart(ShaderType shaderType, string filePath)
    {
        _shaderParts.Add(shaderType, filePath);
        
        return this;
    }

    public IShader? Build()
    {
        var partHandles = new List<int>(_shaderParts.Count);
        
        if (_shaderParts.Count == 0)
        {
            throw new InvalidOperationException("No shader parts defined.");
        }

        var handle = GL.CreateProgram();
        
        foreach (var part in _shaderParts)
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

public interface IShaderBuilder
{
    IShaderBuilder AddShaderPart(ShaderType shaderType, string shaderPart);
    IShader? Build();
}