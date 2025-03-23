using System;
using System.Drawing;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

class RayTracer : GameWindow
{
    private int computeShader;
    private int imageTexture;

    public RayTracer() : base(GameWindowSettings.Default, new NativeWindowSettings
    {
        Size = new OpenTK.Mathematics.Vector2i(800, 600),
        Title = "Real-Time Ray Tracer with Compute Shader"
    }) { }

    protected override void OnLoad()
    {
        base.OnLoad();
        Console.WriteLine(" OpenGL Context Initialized.");

        computeShader = CreateComputeShader("computeShader.glsl");
        imageTexture = CreateTexture(800, 600);

        RunComputeShader();

        GL.BindTexture(TextureTarget.Texture2D, imageTexture);

        float[] pixels = new float[800 * 600 * 4]; // Allocate space for pixel data
        GL.GetTexImage(TextureTarget.Texture2D, 0, PixelFormat.Rgba, PixelType.Float, pixels);

        bool hasNonBlackPixels = false;
        for (int i = 0; i < pixels.Length; i += 4)
        {
            if (pixels[i] > 0 || pixels[i + 1] > 0 || pixels[i + 2] > 0)
            {
                hasNonBlackPixels = true;
                break;
            }
        }

        if (hasNonBlackPixels)
        {
            Console.WriteLine("Compute Shader Wrote to Texture.");
        }
        else
        {
            Console.WriteLine("Compute Shader Output is BLACK. Check Shader Logic.");
        }
    }

    private int CreateComputeShader(string filePath)
    {
        Console.WriteLine(" Loading Compute Shader...");

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"❌ ERROR: Compute Shader file '{filePath}' not found!");
            throw new Exception("Compute Shader file missing.");
        }

        int shader = GL.CreateShader(ShaderType.ComputeShader);
        string shaderSource = File.ReadAllText(filePath);
        GL.ShaderSource(shader, shaderSource);
        GL.CompileShader(shader);

        GL.GetShader(shader, ShaderParameter.CompileStatus, out int status);
        if (status == 0)
        {
            Console.WriteLine("Compute Shader Compilation Failed: " + GL.GetShaderInfoLog(shader));
            throw new Exception("Compute Shader Compilation Failed.");
        }

        int program = GL.CreateProgram();
        GL.AttachShader(program, shader);
        GL.LinkProgram(program);

        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out status);
        if (status == 0)
        {
            Console.WriteLine(" Compute Shader Linking Failed: " + GL.GetProgramInfoLog(program));
            throw new Exception("Compute Shader Linking Failed.");
        }

        Console.WriteLine("Compute Shader Loaded Successfully.");
        return program;
    }

    private int CreateTexture(int width, int height)
    {
        int tex;
        GL.GenTextures(1, out tex);
        GL.BindTexture(TextureTarget.Texture2D, tex);
        GL.TexStorage2D(TextureTarget2d.Texture2D, 1, SizedInternalFormat.Rgba32f, width, height);
        GL.BindImageTexture(0, tex, 0, false, 0, TextureAccess.WriteOnly, SizedInternalFormat.Rgba32f);
        return tex;
    }

    private void RunComputeShader()
    {
        GL.UseProgram(computeShader);
        GL.DispatchCompute(800 / 16, 600 / 16, 1);
        GL.MemoryBarrier(MemoryBarrierFlags.ShaderImageAccessBarrierBit);
        Console.WriteLine(" Compute Shader Executed.");
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.BindTexture(TextureTarget.Texture2D, imageTexture);
        SwapBuffers();
    }

    static void Main()
    {
        using (var window = new RayTracer())
        {
            window.Run();
        }
    }
}
