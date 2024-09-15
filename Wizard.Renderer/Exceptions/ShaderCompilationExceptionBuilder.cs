using OpenTK.Graphics.OpenGL.Compatibility;
using Wizard.Renderer.Shaders;

namespace Wizard.Renderer.Exceptions;

public static class ShaderCompilationExceptionBuilder
{
    public static ShaderCompilationException Build(ShaderPart shaderPart)
    {
        GL.GetShaderInfoLog(shaderPart.Handle, out var info);
        
        var exception = new ShaderCompilationException(shaderPart.FilePath, info);
        return exception;
    }
}