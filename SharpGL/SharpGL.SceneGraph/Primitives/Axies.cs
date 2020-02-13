using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SharpGL.SceneGraph.Collections;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Raytracing;
using SharpGL.SceneGraph.Helpers;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Transformations;
using SharpGL.SceneGraph.Assets;
using System.Text;
using SharpGL.Enumerations;

namespace SharpGL.SceneGraph.Primitives
{
    /// <summary>
    /// The axies objects are design time rendered primitives
    /// that show an RGB axies at the origin of the scene.
    /// 
    /// X - Red
    /// Y - Green
    /// Z - Blue
    /// </summary>
    public class Axies : RenderElement
    {
        public Axies()
        {
            Name = "Design Time Axies";
        }

        public override void Render(OpenGL gl, RenderMode renderMode)
        {
            // Push all attributes, disable lighting and depth testing.
            gl.PushAttrib(OpenGL.GL_CURRENT_BIT | OpenGL.GL_ENABLE_BIT |
                OpenGL.GL_LINE_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            gl.DepthFunc(OpenGL.GL_ALWAYS);

            // Set a nice fat line width.
            gl.LineWidth(1.50f);

            // Draw the axies.
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1f, 0f, 0f, 1f);
            gl.Vertex(0, 0, 0);
            gl.Vertex(200, 0, 0);
            gl.Color(0f, 1f, 0f, 1f);
            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 200, 0);
            gl.Color(0f, 0f, 1f, 1f);
            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 0, 200);
            gl.End();

            // Restore attributes.
            gl.PopAttrib();
        }
    }
}
