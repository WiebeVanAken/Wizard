using JetBrains.Annotations;
using OpenTK.Graphics.OpenGL;
using Wizard.Renderer.Shaders;

namespace Wizard.Renderer.Test.Shaders;

[TestSubject(typeof(ShaderBuilder))]
public class ShaderBuilderTest
{

    [Fact]
    public void ShadeBuilder_Throws_InvalidOperationException_Without_Shaderparts()
    {
        // Arrange
        var sut = new ShaderBuilder();
        
        // Act
        var act = new Action(() => sut.Build());
        
        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void ShaderBuilder_AddShaderPart_Adds_ShaderPart()
    {
        // Arrange
        var sut = new ShaderBuilder();
        const int expectedShaderPartCount = 1;
        
        // Act
        sut.AddShaderPart(ShaderType.VertexShader, String.Empty);
        var actualShaderPartCount = sut.ShaderParts.Count;
        
        // Assert
        Assert.Equal(expectedShaderPartCount, actualShaderPartCount);
    }

    [Fact]
    public void ShaderBuilder_Builds_Shader_With_ShaderParts()
    {
        // Arrange
        var sut = new ShaderBuilder();
        sut.AddShaderPart(ShaderType.VertexShader, String.Empty);
        
        // Act
        var result = sut.Build();
        
        // Assert
        Assert.NotNull(result);
    }
}