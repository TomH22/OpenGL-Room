using System;
using System.ComponentModel;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;

namespace SharpGL.SceneGraph.Core
{
    /// <summary>
    /// The ArcBall camera supports arcball projection, making it ideal for use with a mouse.
    /// </summary>
    [Serializable()]
    public class ArcBall
    {
        private bool mouseIsDown;

        // Last position of the mouse
        private float mouseDownX, mouseDownY;

        private Matrix transformMatrix;
        private Matrix scaleMatrix;

        private Cameras.LookAtCamera camera;

        public ArcBall(Cameras.LookAtCamera camera)
        {
            this.camera = camera;

            transformMatrix = new Matrix(4, 4);
            scaleMatrix = new Matrix(4, 4);

            //  Set the identity matrices.
            transformMatrix.SetIdentity();
            scaleMatrix.SetIdentity();

            mouseIsDown = false;
        }

        /// <summary>
        /// This is the class' main function, to override this function and perform a 
        /// perspective transformation.
        /// </summary>
        public void TransformMatrix(OpenGL gl)
        {
            gl.MultMatrix(scaleMatrix.AsColumnMajorArray);
            gl.MultMatrix(transformMatrix.AsColumnMajorArray);
        }

        public void MouseDown(int x, int y)
        {
            this.mouseIsDown = true;

            this.mouseDownX = x;
            this.mouseDownY = y;
        }

        public void MouseMove(int x, int y)
        {
            if (!mouseIsDown)
                return;

            Vertex UpVector = camera.UpVector * transformMatrix;
            Vertex Position = new Vertex(0, 0, 1) * transformMatrix;
            Vertex Target = camera.Target;

            double horizontal = (this.mouseDownX - x) * -0.005f;
            double vertikal = (this.mouseDownY - y) * -0.005f;

            transformView(UpVector, Position, Target, horizontal, vertikal, false);

            mouseDownX = x;
            mouseDownY = y;
        }

        /// <summary>
        /// Rotates the view.
        /// </summary>
        private void transformView(Vertex UpVector, Vertex Position, Vertex Target, double horizontal, double vertikal, bool forceCalc)
        {
            Matrix m = null;

            if (forceCalc)
            {
                m = new Matrix(4, 4);
                m.SetIdentity();
            }

            if (vertikal == 0)
            {
                if (horizontal != 0)
                {
                    // Rotation around the Z axis
                    m = Matrix.GetRotate_Z_Matrix(horizontal);
                }
            }
            else
            {
                // x direction
                Vertex viewX = UpVector.VectorProduct(Position - Target);
                viewX.Normalize();

                if (horizontal == 0)
                {
                    // Rotation around the vector that points to the right in the image.
                    m = Matrix.GetRotateMatrix(viewX, vertikal);
                }
                else
                {
                    // Combined rotation.
                    m = Matrix.GetRotate_Z_Matrix(horizontal) * Matrix.GetRotateMatrix(viewX, vertikal);
                }
            }

            // There was a rotation at all.
            if (m != null)
            {
                Vertex lookAt = Target;         // Target
                Vertex pos = Position - lookAt; // Position
                Vertex upDir = UpVector;        // UpVector

                Vertex posTransformed = lookAt + (pos * m);
                Vertex upDirTransformed = upDir * m;

                Vertex posTransformedNormalized = (posTransformed - lookAt);
                posTransformedNormalized.Normalize();

                Vertex upDirTransformedNormalized = upDirTransformed;
                upDirTransformedNormalized.Normalize();

                transformMatrix = LookAtRH(lookAt + posTransformedNormalized, lookAt, upDirTransformedNormalized);
            }
        }

        public void MouseUp(int x, int y)
        {
            this.mouseIsDown = false;
        }

        /// <summary>
        /// Gets the transformation matrix.
        /// </summary>
        public static Matrix LookAtRH(Vertex pos, Vertex lookat, Vertex updir)
        {
            Vertex zaxis = (pos - lookat); zaxis.Normalize();
            Vertex xaxis = updir; xaxis = xaxis.VectorProduct(zaxis); xaxis.Normalize();
            Vertex yaxis = zaxis; yaxis = yaxis.VectorProduct(xaxis);

            Matrix result = new Matrix(4, 4);
            result.SetIdentity();

            result[0, 0] = xaxis.X;
            result[0, 1] = xaxis.Y;
            result[0, 2] = xaxis.Z;
            result[0, 3] = -xaxis.ScalarProduct(pos);
            result[1, 0] = yaxis.X;
            result[1, 1] = yaxis.Y;
            result[1, 2] = yaxis.Z;
            result[1, 3] = -yaxis.ScalarProduct(pos);
            result[2, 0] = zaxis.X;
            result[2, 1] = zaxis.Y;
            result[2, 2] = zaxis.Z;
            result[2, 3] = -zaxis.ScalarProduct(pos);
            result[3, 0] = 0;
            result[3, 1] = 0;
            result[3, 2] = 0;
            result[3, 3] = 1.0;

            return result;
        }

        public Vertex GetTransformedDirection(Vertex vertex, Matrix matrix)
        {
            double[] result = new double[4];
            //temp ist der Vektor erweitert um eine wall-Komponente (wall = 0.0)
            double[] temp = new double[4];
            temp[0] = vertex.X;
            temp[1] = vertex.Y;
            temp[2] = vertex.Z;
            temp[3] = 0d;

            //Multiplikation mit der Matrix
            for (int i = 0; i <= 3; i++)
            {
                result[i] = 0;
                for (int j = 0; j <= 3; j++)
                {
                    result[i] += matrix[i, j] * temp[j];
                }
            }

            return new Vertex((float)result[0], (float)result[1], (float)result[2]);
        }

        public void MouseWheel(int delta)
        {
            float scale = (delta / 2000.0f);// 120

            float newVal = (float)Math.Abs(scaleMatrix[0, 0] + scale);

            scaleMatrix[0, 0] = newVal;
            scaleMatrix[1, 1] = newVal;
            scaleMatrix[2, 2] = newVal;
        }

        private Matrix Matrix3fSetRotationFromQuat4f(float[] q1)
        {
            float n, s;
            float xs, ys, zs;
            float wx, wy, wz;
            float xx, xy, xz;
            float yy, yz, zz;
            n = (q1[0] * q1[0]) + (q1[1] * q1[1]) + (q1[2] * q1[2]) + (q1[3] * q1[3]);
            s = (n > 0.0f) ? (2.0f / n) : 0.0f;

            xs = q1[0] * s; ys = q1[1] * s; zs = q1[2] * s;
            wx = q1[3] * xs; wy = q1[3] * ys; wz = q1[3] * zs;
            xx = q1[0] * xs; xy = q1[0] * ys; xz = q1[0] * zs;
            yy = q1[1] * ys; yz = q1[1] * zs; zz = q1[2] * zs;

            Matrix matrix = new Matrix(3, 3);

            matrix[0, 0] = 1.0f - (yy + zz); matrix[1, 0] = xy - wz; matrix[2, 0] = xz + wy;
            matrix[0, 1] = xy + wz; matrix[1, 1] = 1.0f - (xx + zz); matrix[2, 1] = yz - wx;
            matrix[0, 2] = xz - wy; matrix[1, 2] = yz + wx; matrix[2, 2] = 1.0f - (xx + yy);

            return matrix;
        }

        private void Matrix4fSetRotationFromMatrix3f(ref Matrix transform, Matrix matrix)
        {
            float scale = transform.TempSVD();
            transform.FromOtherMatrix(matrix, 3, 3);
            transform.Multiply(scale, 3, 3);
        }

        public void SetViewMode(SharpGL.SceneGraph.Effects.ViewMode viewMode)
        {
            switch (viewMode)
            {
                case SharpGL.SceneGraph.Effects.ViewMode.VO:
                    transformView(new Vertex(0, 1, 0), new Vertex(0, 0, 1), new Vertex(0, 0, 0), 0, 0, true);
                    break;
                case SharpGL.SceneGraph.Effects.ViewMode.SX:
                    transformView(new Vertex(0, 0, 1), new Vertex(-1, 0, 0), new Vertex(0, 0, 0), 0, 0, true);
                    break;
                case SharpGL.SceneGraph.Effects.ViewMode.SY:
                    transformView(new Vertex(0, 0, 1), new Vertex(0, -1, 0), new Vertex(0, 0, 0), 0, 0, true);
                    break;
                case SharpGL.SceneGraph.Effects.ViewMode.NO:
                    transformView(new Vertex(-1, -1, 1), new Vertex(1, 1, 1), new Vertex(0, 0, 0), 0, 0, true);
                    break;
                case SharpGL.SceneGraph.Effects.ViewMode.NW:
                    transformView(new Vertex(1, -1, 1), new Vertex(-1, 1, 1), new Vertex(0, 0, 0), 0, 0, true);
                    break;
                case SharpGL.SceneGraph.Effects.ViewMode.SO:
                    transformView(new Vertex(-1, 1, 1), new Vertex(1, -1, 1), new Vertex(0, 0, 0), 0, 0, true);
                    break;
                case SharpGL.SceneGraph.Effects.ViewMode.SW:
                    transformView(new Vertex(1, 1, 1), new Vertex(-1, -1, 1), new Vertex(0, 0, 0), 0, 0, true);
                    break;
            }
        }
    }
}
