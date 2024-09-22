namespace Wizard.Renderer.Exceptions;

public class ShaderLinkingException(string infoLog)
    : Exception($"Shader could not be linked. Error: {infoLog}")
{
    public string InfoLog { get; private set; } = infoLog;
}