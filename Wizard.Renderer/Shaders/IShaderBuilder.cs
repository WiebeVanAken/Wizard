using OpenTK.Graphics.OpenGL;

namespace Wizard.Renderer.Shaders;

public interface IShaderBuilder
{
    IShaderBuilder AddShaderPart(ShaderType shaderType, string shaderPart);
    Shader Build();
}