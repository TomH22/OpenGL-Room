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
    public class Arc : ShapeLineBased
    {
        private Vertex secondPoint;

        // the normal on the arc plane
        protected Vertex normal;

        protected Vertex span1;
        protected Vertex span2;

        // Arc center, mid point of an imaginary circle
        protected Vertex center;

        private float radius;

        public Arc(Vertex startPoint, Vertex secondPoint, Vertex endPoint):base()
        {
            this.startPoint = startPoint;
            this.secondPoint = secondPoint;
            this.endPoint = endPoint;

            calcData();
        }

        /// <summary>
        /// Calculates the circle, which is defined by 3 points.
        /// </summary>
        public void calcData()
        {
            // Check whether the 3 points are in a row or not.
            if (areOnLine(startPoint, secondPoint, endPoint))
                throw new Exception("The points result in no circle");

            // direction vector from point b to a
            Vertex ba = startPoint - secondPoint;

            // direction vector from point b to c
            Vertex bc = endPoint - secondPoint;

            // Perpendicular to the plane of the triangle / circle.
            ba.Normalize(); bc.Normalize();
            this.normal = ba.VectorProduct(bc);
            this.normal.Normalize();

            // Calculation of the center of the circle
            this.center = getCenter();

            // Calculation of the radius
            this.radius = (float)(center - startPoint).Magnitude();


            // Determination of 2 clamping vectors.
            this.span1 = startPoint - center;
            this.span2 = span1.VectorProduct(this.normal);
        }

        public Vertex SecondPoint
        {
            get { return secondPoint; }
        }

        public float Radius
        {
            get { return radius; }
        }

        public Vertex Center
        {
            get { return center; }
        }

        public override void Render(OpenGL gl, RenderMode renderMode)
        {
            // not implemented yet
        }

        /// <summary>
        /// This auxiliary method checks whether 3 points in plane-dimensional space lie on a straight line.
        /// </summary>
        private static bool areOnLine(Vertex a, Vertex b, Vertex c)
        {
            Vertex ba = a - b;

            if (a.Equals(b) || a.Equals(c) || b.Equals(c))
            {
                return true;
            }
            else
            {
                float t = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (ba[i] != 0)
                    {
                        t = (c[i] - b[i]) / ba[i];
                        break;
                    }
                }

                if ((b + ba * t).EqualsAlmostExcactly(c))
                {
                    // The 3 vectors are linearly dependent.
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Calculates the center of the circle.
        /// </summary>
        private Vertex getCenter()
        {
            Vertex a = startPoint;
            Vertex b = secondPoint;
            Vertex c = endPoint;

            Vertex normal = (a - b).VectorProduct(c - b); // Normal vector of the plane
            normal.Normalize();

            Vertex Mba = 0.5f * (a + b); // mid point of ba
            Vertex Mbc = 0.5f * (c + b); // mid point bc

            Vertex dirMba = (b - a).VectorProduct(normal); // direction vector of the 1st straight line
            Vertex dirMbc = (c - b).VectorProduct(normal); // direction vector of the 2st straight line

            Vertex? v = new Line(Mba, Mba + dirMba).GetIntersection(new Line(Mbc, Mbc + dirMbc));
            if (v != null)
                return v.Value;

            throw new Exception("Failure at center calculation of an arc.");
        }

        /// <summary>
        /// Returns the opening angle of the arc.
        /// </summary>
        protected double getArcAngle()
        {
            Vertex mToStart = this.startPoint - center;
            Vertex mToEnd = this.endPoint - center;
            double alpha = mToStart.getAngle(mToEnd, normal * -1);
            if (alpha < 0)
                alpha += 2 * Math.PI;

            return alpha;
        }

        public override string ToString()
        {
            return "start point: "+startPoint.ToString() + System.Environment.NewLine+"second point: " + secondPoint.ToString() + System.Environment.NewLine+"end point: " + endPoint.ToString();
        }

    }
}
