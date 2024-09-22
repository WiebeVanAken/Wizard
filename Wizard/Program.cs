using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Wizard.Renderer;
using Wizard.Renderer.Shaders;
using Wizard.Renderer.Window;
using Buffer = OpenTK.Graphics.OpenGL.Buffer;
using BufferUsage = OpenTK.Graphics.OpenGL.BufferUsage;

using GameWindow window = new Window(800, 600, "Wizard");

IShader? shader = null;
using var vbo = new Buffer<double>(BufferTarget.ArrayBuffer, BufferUsage.StaticDraw,
    [
            0.5d, 0.5d, 0.0d, 1.0d, 0.0d, 0.0d, 
            0.5d, -0.5d, 0.0d, 0.0d, 1.0d, 0.0d, 
            -0.5d, -0.5d, 0.0d, 0.0d, 0.0d, 1.0d,
            -0.5d, 0.5d, 0.0d, 1.0d, 1.0d, 1.0d
    ]);
using var vao = new VertexArrayObject(vbo);
using var ebo = new Buffer<uint>(
    BufferTarget.ElementArrayBuffer, 
    BufferUsage.StaticDraw, 
    [0, 1, 3, 1, 2, 3]
);

window.Load += () =>
{
    shader = new ShaderBuilder()
        .AddShaderPart(ShaderType.VertexShader, "Shaders/basic.vert")
        .AddShaderPart(ShaderType.FragmentShader, "Shaders/basic.frag")
        .Build();
    shader.Use();
};

window.Unload += () =>
{
    shader.Dispose();
};

window.RenderFrame += (FrameEventArgs e) =>
{
    GL.Clear(ClearBufferMask.ColorBufferBit);

    vao.Bind();
    ebo.Bind();
    GL.DrawElements(PrimitiveType.Triangles, ebo.Data.Length, DrawElementsType.UnsignedInt, 0);
    
    window.SwapBuffers();
};

window.Run();