using OpenTK.Graphics.OpenGL.Compatibility;
using Wizard.Renderer.Shaders;

namespace Wizard.Renderer.Exceptions;

public static class ShaderLinkingExceptionBuilder
{
    public static ShaderLinkingException Build(Shader shader)
    {
        GL.GetProgramInfoLog(shader.Handle, out var info);
        
        var exception = new ShaderLinkingException(info);
        return exception;
    }
}