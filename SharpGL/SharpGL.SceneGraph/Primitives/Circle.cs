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
    public class Circle : Arc
    {
        private List<Vertex> circleDrawPoints;

        public Circle(Vertex startPoint, Vertex secondPoint, Vertex endPoint) : base(startPoint, secondPoint, endPoint)
        {
            calcCircleDrawPoints();
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
            gl.Begin(OpenGL.GL_POLYGON);

            foreach (Vertex vertex in circleDrawPoints)
            {
                //	Set the vertex.
                gl.Vertex(vertex);
            }

            gl.End();
        }
    }
}
