using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SharpGL.SceneGraph.NETDesignSurface.Converters;
using System.Xml.Serialization;

namespace SharpGL.SceneGraph
{
    /// <summary>
    /// The Vertex class represents a 3D point in space.
    /// </summary>
    [TypeConverter(typeof(VertexConverter))]
    public struct Vertex
    {
        private float x;
        private float y;
        private float z;

        public Vertex(float x, float y, float z)
        {
            this.x = x; 
            this.y = y; 
            this.z = z;
        }

        public Vertex(Vertex vertex)
        {
            this.x = vertex.X;
            this.y = vertex.Y;
            this.z = vertex.Z;
        }

        public void Set(float X, float Y, float Z)
        {
            this.X = X; this.Y = Y; this.Z = Z;
        }

        public void Push(float X, float Y, float Z) { this.X += X; this.Y += Y; this.Z += Z; }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }

        public static Vertex operator +(Vertex lhs, Vertex rhs)
        {
            return new Vertex(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        public static Vertex operator -(Vertex lhs, Vertex rhs)
        {
            return new Vertex(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public static Vertex operator *(Vertex lhs, Vertex rhs)
        {
            return new Vertex(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z);
        }

        public static Vertex operator *(Vertex lhs, float rhs)
        {
            return new Vertex(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);
        }

        public static Vertex operator * (Vertex lhs, Matrix rhs)
        {
            float X = lhs.X * (float)rhs[0,0] + lhs.Y * (float)rhs[1,0] + lhs.Z * (float)rhs[2,0];
            float Y = lhs.X * (float)rhs[0,1] + lhs.Y * (float)rhs[1,1] + lhs.Z * (float)rhs[2,1];
            float Z = lhs.X * (float)rhs[0,2] + lhs.Y * (float)rhs[1,2] + lhs.Z * (float)rhs[2,2];

            return new Vertex(X, Y, Z);
        }

        public static Vertex operator *(Matrix lhs, Vertex rhs)
        {
            float X = rhs.X * (float)lhs[0, 0] + rhs.Y * (float)lhs[1, 0] + rhs.Z * (float)lhs[2, 0];
            float Y = rhs.X * (float)lhs[0, 1] + rhs.Y * (float)lhs[1, 1] + rhs.Z * (float)lhs[2, 1];
            float Z = rhs.X * (float)lhs[0, 2] + rhs.Y * (float)lhs[1, 2] + rhs.Z * (float)lhs[2, 2];

            return new Vertex(X, Y, Z);
        }

        public static Vertex operator *(float mult, Vertex rhs)
        {
            return new Vertex(mult* rhs.x, mult * rhs.Y, mult * rhs.Z);
        }

        public static Vertex operator /(Vertex lhs, float rhs)
        {
            return new Vertex(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs);
        }

        /// <summary> 
        /// Component access
        /// </summary>
        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return this.X;
                    case 1: return this.Y;
                    case 2: return this.Z;
                    default: return float.NaN;
                }
            }
            set
            {
                switch (i)
                {
                    case 0: this.X = value; return;
                    case 1: this.Y = value; return;
                    case 2: this.Z = value; return;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        public bool EqualsAlmostExcactly(Vertex rhs)
        {
            return (rhs - this).ABSSqr <= 0.0000000001;
        }

        /// <summarY>
        /// This finds the Scalar Product (Dot Product) of two vectors.
        /// </summarY>
        /// <param name="rhs">The right hand side of the equation.</param>
        /// <returns>A Scalar Representing the Dot-Product.</returns>
        public float ScalarProduct(Vertex rhs)
        {
            return X * rhs.X + Y * rhs.Y + Z * rhs.Z;
        }

        /// <summarY>
        /// Find the Vector product (cross product) of two vectors.
        /// </summarY>
        /// <param name="rhs">The right hand side of the equation.</param>
        /// <returns>The Cross Product.</returns>
        public Vertex VectorProduct(Vertex rhs)
        {
            return new Vertex((Y * rhs.Z) - (Z * rhs.Y), (Z * rhs.X) - (X * rhs.Z),
                (X * rhs.Y) - (Y * rhs.X));
        }

        /// <summarY>
        /// If You use this as a Vector, then call this function to get the vector
        /// magnitude.
        /// </summarY>
        /// <returns></returns>
        public double Magnitude()
        {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        /// <summarY>
        /// Make this vector unit length.
        /// </summarY>
        public void UnitLength()
        {
            float f = X * X + Y * Y + Z * Z;
            float frt = (float)Math.Sqrt(f);
            X /= frt;
            Y /= frt;
            Z /= frt;
        }

        /// <summary>
        /// Returns the square length of the vector.
        /// </summary>
        public double ABSSqr
        {
            get
            {
                return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
            }
        }

        /// <summary>
        /// Normalizes this instance.
        /// </summary>
        public void Normalize()
        {
            UnitLength();
        }

        /// <summary>
        /// Convertes the coordinates in an float array
        /// </summary>
        /// <remarks>I don't understand the need of the implicit key word?</remarks>
        public static implicit operator float[](Vertex rhs)
        {
            return new float[] { rhs.X, rhs.Y, rhs.Z };
        }

        /// <summary>
        /// Returns the angle to another vector.
        /// </summary>
        /// <remarks>
        /// The order of the directions plays a role.
        /// </remarks>
        /// <returns>Value between -PI and + PI.</returns>
        public double getAngle(Vertex dir, Vertex viewDir)
        {
            Vertex thisNomalized = new Vertex(this);
            thisNomalized.Normalize();

            Vertex viewDirNormalized = new Vertex(viewDir);
            viewDirNormalized.Normalize();

            double dotProduct = thisNomalized.ScalarProduct(viewDirNormalized);

            double arc = Math.Acos(dotProduct);

            // We construct three points from the two directions.
            // Which are then checked, to see how they are.
            Vertex vec1 = new Vertex(this);
            Vertex vec2 = new Vertex();
            Vertex vec3 = new Vertex(dir);

            try
            {
                if (vec1.areClockwise(vec2, vec3, viewDir))
                    arc *= -1;
                return arc;
            }
            // The points lie on a straight line.
            catch (InvalidOperationException)
            {
                return arc;
            }
        }

        /// <summary>
        /// Tests the order of the points.
        /// 
        /// Seen in the direction, indicated by the third parameter.
        /// </summary>
        /// <param name = "p1"> First point. </param>
        /// <param name = "p2"> Second point. </param>
        /// <param name = "viewDir"> View direction from which the sense of rotation should be calculated. </param>
        /// <exception cref="InvalidOperationException">if the points lie on a line</exception>
        public bool areClockwise(Vertex p1, Vertex p2, Vertex viewDir)
        {
            Vertex u = p2 - this;
            u.Normalize();

            Vertex v = p1 - this;
            v.Normalize();

            Vertex w = u.VectorProduct(v);

            double test = w.ScalarProduct(viewDir);
            if (Math.Abs(test) < 0.000001)
                throw new InvalidOperationException("All points are on one line!");

            return test < 0;
        }
        
        /// <summary>
        /// The X coordinate.
        /// </summary>
        [XmlAttribute]
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// The Y coordinate.
        /// </summary>
        [XmlAttribute]
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// The Z coordinate.
        /// </summary>
        [XmlAttribute]
        public float Z
        {
            get { return z; }
            set { z = value; }
        }
    }
}
