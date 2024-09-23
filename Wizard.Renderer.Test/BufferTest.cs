using JetBrains.Annotations;
using OpenTK.Graphics.OpenGL;
using Buffer = System.Buffer;

namespace Wizard.Renderer.Test;

[TestSubject(typeof(Buffer))]
public class BufferTest
{

    [Fact]
    public void Buffer_Constructor_IsUnbuffered()
    {
        // Arrange
        var sut = new Buffer<int>(BufferTarget.ArrayBuffer, BufferUsage.StaticDraw, []);
        const bool expected = true;
        
        // Act
        var actual = sut.NeedsBuffering;
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Buffer_BufferData_Sets_NeedsBuffering_To_False()
    {
        // Arrange
        var sut = new Buffer<int>(BufferTarget.ArrayBuffer, BufferUsage.StaticDraw, []);
        const bool expected = false;
        
        // Act
        sut.BufferData();
        var actual = sut.NeedsBuffering;
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Buffer_Change_Data_Sets_NeedsBuffering_To_True()
    {
        // Arrange
        var sut = new Buffer<int>(BufferTarget.ArrayBuffer, BufferUsage.StaticDraw, [0]);
        sut.BufferData();
        const bool expectedAfterEdit = true;
        
        // Act
        sut[0] = 1;
        var actualAfterEdit = sut.NeedsBuffering;
        
        // Assert
        Assert.Equal(expectedAfterEdit, actualAfterEdit);
    }

    [Fact]
    public void Buffer_Change_BufferUsage_Sets_NeedsBuffering_To_True()
    {
        // Arrange
        var sut = new Buffer<int>(BufferTarget.ArrayBuffer, BufferUsage.StaticDraw, []);
        sut.BufferData();
        const bool expected = true;

        // Act
        sut.BufferUsage = BufferUsage.StaticDraw;
        var actual = sut.NeedsBuffering;
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Buffer_AccessHandle_Calls_BufferData_To_Set_NeedsBuffering_To_False()
    {
        // Arrange
        var sut = new Buffer<int>(BufferTarget.ArrayBuffer, BufferUsage.StaticDraw, []);
        const bool expected = false;
        
        // Act
        _ = sut.Handle;
        var actual = sut.NeedsBuffering;
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Buffer_Bind_Sets_NeedsBuffering_To_False_When_NeedsBuffering_Is_True()
    {
        // Arrange
        var sut = new Buffer<int>(BufferTarget.ArrayBuffer, BufferUsage.StaticDraw, []);
        const bool expected = false;
        
        // Act
        sut.Bind();
        var actual = sut.NeedsBuffering;
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Buffer_Int32_ByteSize_IsEqualTo_sizeof_Int32()
    {
        // Arrange
        var buffer = new Buffer<int>(BufferTarget.ArrayBuffer, BufferUsage.StaticDraw, []);
        const int expected = sizeof(int);
        
        // Act
        var actual = buffer.ByteSize;
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Buffer_Double_ByteSize_IsEqualTo_sizeof_Double()
    {
        // Arrange
        var buffer = new Buffer<double>(BufferTarget.ArrayBuffer, BufferUsage.StaticDraw, []);
        const int expected = sizeof(double);
        
        // Act
        var actual = buffer.ByteSize;
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Buffer_Float_ByteSize_IsEqualTo_sizeof_Float()
    {
        // Arrange
        var buffer = new Buffer<float>(BufferTarget.ArrayBuffer, BufferUsage.StaticDraw, []);
        const int expected = sizeof(float);
        
        // Act
        var actual = buffer.ByteSize;
        
        // Assert
        Assert.Equal(expected, actual);
    }
}