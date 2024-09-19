namespace Wizard.Renderer.Shaders;

public interface IShader : IDisposable
{
    public int Handle { get; }
    public void Use();
}