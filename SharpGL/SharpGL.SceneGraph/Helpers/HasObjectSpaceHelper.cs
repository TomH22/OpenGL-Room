using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Transformations;

namespace SharpGL.SceneGraph.Helpers
{
    /// <summary>
    /// Helps with implementing IHasObjectSpace.
    /// </summary>
    public class HasObjectSpaceHelper : IDeepCloneable<HasObjectSpaceHelper>
    {
        /// <summary>
        /// Pushes us into Object Space using the transformation into the specified OpenGL instance.
        /// </summary>
        public void PushObjectSpace(OpenGL gl)
        {
            //  Push the matrix.
            gl.PushMatrix();

            //  Perform the transformation.
            transformation.Transform(gl);
        }

        /// <summary>
        /// Pops us from Object Space using the transformation into the specified OpenGL instance.
        /// </summary>
        public void PopObjectSpace(OpenGL gl)
        {
            //  Pop the matrix.
            gl.PopMatrix();
        }

        /// <summary>
        /// Deeps the clone.
        /// </summary>
        public HasObjectSpaceHelper DeepClone()
        {
            return new HasObjectSpaceHelper()
            {
                transformation = this.transformation.DeepClone()
            };
        }

        /// <summary>
        /// The linear transformation
        /// </summary>
        private LinearTransformation transformation = new LinearTransformation();

        /// <summary>
        /// Gets or sets the transformation.
        /// </summary>
        /// <value>
        /// The transformation.
        /// </value>
        public LinearTransformation Transformation
        {
            get { return transformation; }
            set { transformation = value; }
        }
    }
}
