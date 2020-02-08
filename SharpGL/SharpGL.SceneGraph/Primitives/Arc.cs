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

        // Specifies the plane normal of the arc.
        protected Vertex normal;

        protected Vertex span1;
        protected Vertex span2;

        // Arc center, mid point of an imaginary circle
        protected Vertex center;

        private float radius;

        public Arc(Vertex startPoint, Vertex secondPoint, Vertex endPoint)
        {
            this.startPoint = startPoint;
            this.secondPoint = secondPoint;
            this.endPoint = endPoint;

            calcData();
        }

        /// <summary>
        /// Calculates the circle, which is defined either by 3 points or by center point, radius and plane.
        /// </summary>
        public void calcData()
        {
            // 0. Check whether the 3 points are in a row or not.
            if (areOnLine(startPoint, secondPoint, endPoint))
                throw new Exception("The points result in no circle");

            // 1. Calculation of the center of the circle.
            // 1.1.a) Direction vector from point b to a
            Vertex ba = startPoint - secondPoint;
            // 1.1.b) Direction vector from point b to c
            Vertex bc = endPoint - secondPoint;

            // 1.2. Perpendicular to the plane of the triangle / circle.
            ba.Normalize(); bc.Normalize();  //  Max: For very small radii, the vector is recognized as 0 after the cross product, i.e. normalize vectors beforehand
            this.normal = ba.VectorProduct(bc);
            this.normal.Normalize();

            // 1.3. Calculation of the center of the circle
            this.center = getCenter();
            // 2. Calculation of the radius
            this.radius = (float)(center - startPoint).Magnitude();


            // 3. Draw the circle and thus the arc.
            // 3.1. Determination of 2 clamping vectors (ellipses have different lengths)
            this.span1 = startPoint - center;
            this.span2 = span1.VectorProduct(this.normal);

            //this.CalcBoundingBox();
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
        /// This auxiliary method checks whether 3 points in plane-dimensional space, described by vectors, lie on a straight line.
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

            Vertex normal = (a - b).VectorProduct(c - b);   // Normalenvektor der Ebene
            normal.Normalize();

            Vertex Mba = 0.5f * (a + b);    // Mittelpunkt von ba
            Vertex Mbc = 0.5f * (c + b);    // Mittelpunkt von bc

            Vertex dirMba = (b - a).VectorProduct(normal);  // Richtungsvektor der 1. Geraden
            Vertex dirMbc = (c - b).VectorProduct(normal);  // Richtungsvektor der 2. Geraden

            Vertex? v = new Line(Mba, Mba + dirMba).GetIntersection(new Line(Mbc, Mbc + dirMbc));
            if (v != null)
                return v.Value;

            throw new Exception("Failure at center calculation of an arc.");
        }

        /// <summary>
        /// Returns the opening angle of the arch.
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
    }
}
