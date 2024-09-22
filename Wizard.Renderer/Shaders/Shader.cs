namespace Wizard.Renderer.Shaders;

public class Shader(int handle) : IShader
{
    public int Handle { get; } = handle;
}