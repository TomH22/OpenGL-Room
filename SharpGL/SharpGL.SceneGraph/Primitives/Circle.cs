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

namespace SharpGL.SceneGraph.Primitives
{
    public class Circle :Arc
    {
        private List<Vertex> circleDrawPoints;

        public Circle(Vertex startPoint, Vertex secondPoint, Vertex endPoint) : base(startPoint, secondPoint, endPoint)
        {
            calcCircleDrawPoints();
            SetFacesFromVertexData(circleDrawPoints.ToArray());
        }

        private void calcCircleDrawPoints()
        {
            circleDrawPoints = new List<Vertex>();

            double arcAngle = getArcAngle();

            for (double i = 0; i < 360; i += 1)
            {
                double phi = i * Math.PI / (360 / 2.0);
                circleDrawPoints.Add(center + ((float)Math.Cos(phi)) * span1 + ((float)Math.Sin(phi)) * span2);
            }
        }

        /// <summary>
        /// Render to the provided instance of OpenGL.
        /// </summary>
        public override void Render(OpenGL gl, RenderMode renderMode)
        {
            //  If we're frozen, use the helper.
            if (freezableHelper.IsFrozen)
            {
                freezableHelper.Render(gl);
                return;
            }

            foreach (Face face in faces)
            {
                //  If the face has its own material, push it.
                if (face.Material != null)
                    face.Material.Push(gl);

                gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, LineMode);

                switch (DepthFunc)
                {
                    case SharpGL.Enumerations.DepthFunc.EQUAL:
                        gl.DepthFunc(OpenGL.GL_EQUAL);
                        break;
                    case SharpGL.Enumerations.DepthFunc.ALAYS:
                        gl.DepthFunc(OpenGL.GL_ALWAYS);
                        break;
                    default:
                        gl.DepthFunc(OpenGL.GL_LESS);
                        break;
                        // switch not comleted!
                }

                if (LineMode == OpenGL.GL_LINE)
                {
                    gl.LineWidth(0.9f);
                }

                if (Offset < 0 || Offset > 0)
                {
                    gl.Enable(OpenGL.GL_POLYGON_OFFSET_LINE);
                    gl.PolygonOffset(Offset, Offset);
                }
                else
                {
                    gl.Disable(OpenGL.GL_POLYGON_OFFSET_LINE);
                }

                gl.Begin(OpenGL.GL_POLYGON);

                foreach (Index index in face.Indices)
                {
                    //	Set the vertex.
                    gl.Vertex(vertices[index.Vertex]);
                }

                gl.End();

                //  If the face has its own material, pop it.
                if (face.Material != null)
                    face.Material.Pop(gl);
            }

            //	Restore the attributes.
            //gl.PopAttrib();
        }
    }
}
