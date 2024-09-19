using OpenTK.Graphics.OpenGL.Compatibility;
using Wizard.Renderer.Shaders;

namespace Wizard.Renderer.Exceptions;

public static class ShaderCompilationExceptionBuilder
{
    public static ShaderCompilationException Build(int handle, string filePath)
    {
        GL.GetShaderInfoLog(handle, out var info);
        
        return new ShaderCompilationException(filePath, info);;
    }
}