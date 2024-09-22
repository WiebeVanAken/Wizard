using OpenTK.Graphics.OpenGL.Compatibility;
using Wizard.Renderer.Shaders;

namespace Wizard.Renderer.Exceptions;

public static class ShaderLinkingExceptionBuilder
{
    public static ShaderLinkingException Build(int handle)
    {
        GL.GetProgramInfoLog(handle, out var info);
        
        var exception = new ShaderLinkingException(info);
        return exception;
    }
}