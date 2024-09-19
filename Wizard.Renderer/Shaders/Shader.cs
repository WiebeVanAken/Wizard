﻿using OpenTK.Graphics.OpenGL.Compatibility;
using Wizard.Renderer.Exceptions;

namespace Wizard.Renderer.Shaders;

internal class Shader : IShader
{
    public int Handle { get; private set; }

    public Shader(int handle)
    {
        Handle = handle;
    }
    
    public void Use()
    {
        GL.UseProgram(Handle);
    }

    public void Dispose()
    {
        GL.DeleteProgram(Handle);
    }
}