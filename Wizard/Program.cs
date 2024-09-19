using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Wizard.Renderer;
using Wizard.Renderer.Shaders;
using Wizard.Renderer.Window;
using BufferUsage = OpenTK.Graphics.OpenGL.BufferUsage;

using GameWindow window = new Window(800, 600, "Wizard");

IShader? shader = null;
var VBO = new VertexBufferObject([-0.5d, -0.5d, 0.0d, 0.5d, -0.5d, 0.0d, 0.0d, 0.5d, 0.0d], BufferUsage.StaticDraw);
using VertexArrayObject VAO = new VertexArrayObject(VBO);

window.Load += () =>
{
    shader = new ShaderBuilder()
        .AddShaderPart(ShaderType.VertexShader, "Shaders/basic.vert")
        .AddShaderPart(ShaderType.FragmentShader, "Shaders/basic.frag")
        .Build();
};

window.Unload += () =>
{
    shader.Dispose();
};

window.RenderFrame += (FrameEventArgs e) =>
{
    GL.Clear(ClearBufferMask.ColorBufferBit);

    shader.Use();
    GL.BindVertexArray(VAO.Handle);
    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    
    window.SwapBuffers();
};

window.Run();