namespace Wizard.Renderer.Exceptions;

public class ShaderCompilationException(string filePath, string infoLog)
    : Exception($"Shader at {filePath} could not be compiled. Error: {infoLog}")
{
    public string InfoLog { get; private set; } = infoLog;
    public string FilePath { get; private set; } = filePath;
}