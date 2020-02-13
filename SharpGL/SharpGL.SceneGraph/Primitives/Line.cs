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
    public class Line : ShapeLineBased
    {
        public Line(Vertex startPoint, Vertex endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        public Vertex Dir
        {
            get { return endPoint - startPoint; }
        }

        public float Length
        {
            get
            {
                return (float)Math.Sqrt((startPoint.X - endPoint.X) * (startPoint.X - endPoint.X) + (startPoint.Y - endPoint.Y) * (startPoint.Y - endPoint.Y)
                                                    + (startPoint.Z - endPoint.Z) * (startPoint.Z - endPoint.Z));
            }
        }

        public override void Render(OpenGL gl, RenderMode renderMode)
        {
            // Lines arent drawn yet.
        }

        /// <summary>
        /// Get intersectin with line.
        /// </summary>
        /// <remarks>
        /// Returns null if no intersectin was found.
        /// </remarks>
        public Vertex? GetIntersection(Line line)
        {
            if (!HaveIntersection(line))
                return null;

            Vertex u = this.Dir;
            Vertex v = line.Dir;
            Vertex a = this.StartPoint;
            Vertex b = line.StartPoint;
            float s, t;
            intersectInternal(line, out s, out t);
            return a + s * u;
        }

        /// <summary>
        /// Calculates whether the two lines have an intersection.
        /// If they are identical, they have no intersection.
        /// </summary>
        public bool HaveIntersection(Line l2)
        {
            float s, t;
            if (!intersectInternal(l2, out s, out t))
                return false;

            /// Tolerance built in
            if (s < (0 - 0.0001) || s > (1 + 0.0001))
                return false;
            if (t < (0 - 0.0001) || t > (1 + 0.0001))
                return false;
            return true;
        }

        /// <summary>
        /// Internal auxiliary function for calculating the intersection of two straight lines.
        ///
        /// TODO: Still not working if the coefficient matrix of x and y values is singular. Maybe use pivoting.
        /// </summary>
        private bool intersectInternal(Line l2, out float s, out float t)
        {
            Vertex u = this.Dir;
            Vertex v = l2.Dir;
            Vertex a = this.startPoint;
            Vertex b = l2.startPoint;
            Vertex c = b - a;

            Vertex cross = u.VectorProduct(v);
            s = t = 0;

            // parallel?
            if (cross.Magnitude() < 0.00001)
                return false;

            Plane pl = new Plane(a, cross);
            if (Math.Abs(pl.GetPointDistance(b)) + Math.Abs(pl.GetPointDistance(this.EndPoint)) + Math.Abs(pl.GetPointDistance(l2.EndPoint)) > 1)
                return false;

            //1. case
            float u1 = u.X, u2 = u.Y;
            float v1 = v.X, v2 = v.Y;
            float c1 = c.X, c2 = c.Y;
            float det = -u1 * v2 + v1 * u2;

            //2. case
            float temp = -u1 * v.Z + v1 * u.Z;
            if (Math.Abs(temp) > Math.Abs(det))
            {
                u2 = u.Z;
                v2 = v.Z;
                c2 = c.Z;
                det = temp;
            }

            //3. case
            temp = -u.Y * v.Z + v.Y * u.Z;
            if (Math.Abs(temp) > Math.Abs(det))
            {
                u2 = u.Z;
                v2 = v.Z;
                c2 = c.Z;
                u1 = u.Y;
                v1 = v.Y;
                c1 = c.Y;
                det = temp;
            }

            float ds = -c1 * v2 + v1 * c2;
            float dt = u1 * c2 - c1 * u2;

            s = ds / det;
            t = dt / det;
            return true;
        }
    }
}
