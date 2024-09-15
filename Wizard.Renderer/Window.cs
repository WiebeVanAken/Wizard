using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Wizard.Renderer.Shaders;
using ShaderType = OpenTK.Graphics.OpenGL.Compatibility.ShaderType;

namespace Wizard.Renderer;

public class Window(int width, int height, string title) : GameWindow(GameWindowSettings.Default,
    new NativeWindowSettings
    {
        ClientSize = (width, height),
        Title = title
    })
{
    public VertexArrayObject VAO { get; private set; }
    public VertexBufferObject VBO { get; private set; }
    public Shader Shader { get; private set; }

    protected override void OnLoad() {
        base.OnLoad();
        
        Shader = new Shader();
        Shader.AddShaderPart(new ShaderPart("Shaders/basic.vert", ShaderType.VertexShader));
        Shader.AddShaderPart(new ShaderPart("Shaders/basic.frag", ShaderType.FragmentShader));
        Shader.Compile();
        
        double[] vertices = { -0.5d, -0.5d, 0.0d, 0.5d, -0.5d, 0.0d, 0.0d, 0.5d, 0.0d };
        VBO = new VertexBufferObject(vertices, BufferUsage.StaticDraw);
        VAO = new VertexArrayObject(VBO);
        
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
    }

    protected override void OnUnload()
    {
        Shader.Dispose();
    }
    
    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit);
        Shader.Use();
        GL.BindVertexArray(VAO.Handle);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

        SwapBuffers();
    }
    
    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }
}