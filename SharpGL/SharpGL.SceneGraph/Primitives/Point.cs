using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph.Core;
using System.Xml.Serialization;

namespace SharpGL.SceneGraph.Primitives
{
    // maybe change parent to RenderElement
    public class Point : SceneElement, IRenderable
    {
        public Vertex Vertex;

        public Point()
        {
            Name = "Point";
        }

        /// <summary>
        /// Render to the provided instance of OpenGL.
        /// </summary>
        public void Render(OpenGL gl, RenderMode renderMode)
        {
            //  Design time primitives render only in design mode.
            if (renderMode != RenderMode.Design)
                return;

            //  If we do not have the display list, we must create it.
            //  Otherwise, we can simple call the display list.
            if (displayList == null)
                CreateDisplayList(gl);
            else
                displayList.Call(gl);
        }

        /// <summary>
        /// Creates the display list. This function draws the
        /// geometry as well as compiling it.
        /// </summary>
        private void CreateDisplayList(OpenGL gl)
        {

            //  Create the display list. 
            displayList = new DisplayList();

            //  Generate the display list and 
            displayList.Generate(gl);
            displayList.New(gl, DisplayList.DisplayListMode.CompileAndExecute);

            //  Push all attributes, disable lighting and depth testing.
            gl.PushAttrib(OpenGL.GL_CURRENT_BIT | OpenGL.GL_ENABLE_BIT |
                OpenGL.GL_LINE_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            gl.DepthFunc(OpenGL.GL_ALWAYS);

            //  Set a nice fat line width.
            gl.LineWidth(2f);

            //  Draw the axies.
            gl.Color(0f, 0f, 1f, 1f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Vertex.X - .07, Vertex.Y - .07, Vertex.Z);
            gl.Vertex(Vertex.X + .07, Vertex.Y + .07, Vertex.Z);
            gl.End();

            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Vertex.X - .07, Vertex.Y + .07, Vertex.Z);
            gl.Vertex(Vertex.X + .07, Vertex.Y - .07, Vertex.Z);
            gl.End();

            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(Vertex.X, Vertex.Y, Vertex.Z - .07);
            gl.Vertex(Vertex.X, Vertex.Y, Vertex.Z + .07);
            gl.End();

            //  Restore attributes.
            gl.PopAttrib();

            //  End the display list.
            displayList.End(gl);
        }

        /// <summary>
        /// The internal display list.
        /// </summary>
        [XmlIgnore]
        private DisplayList displayList;
    }
}
