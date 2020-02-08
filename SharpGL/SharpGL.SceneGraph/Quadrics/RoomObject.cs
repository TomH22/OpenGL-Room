using System;
using System.ComponentModel;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Helpers;
using SharpGL.SceneGraph.Assets;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Transformations;
using SharpGL.SceneGraph.Primitives;

namespace SharpGL.SceneGraph.Quadrics
{
    public abstract class RoomObject: 
        SceneElement,
        IHasObjectSpace,        
        /*IHasOpenGLContext,*/     
        /*IRenderable,*/
        IHasMaterial
        /*IVolumeBound*/
    {
        private HasObjectSpaceHelper hasObjectSpaceHelper = new HasObjectSpaceHelper();

        /// <summary>
        /// Pushes us into Object Space using the transformation into the specified OpenGL instance.
        /// </summary>
        public void PushObjectSpace(OpenGL gl)
        {
            //  Use the helper to push us into object space.
            hasObjectSpaceHelper.PushObjectSpace(gl);
        }

        /// <summary>
        /// Pops us from Object Space using the transformation into the specified OpenGL instance.
        /// </summary>
        public void PopObjectSpace(OpenGL gl)
        {
            //  Use the helper to pop us from object space.
            hasObjectSpaceHelper.PopObjectSpace(gl);
        }

        /// <summary>
        /// Gets the transformation that pushes us into object space.
        /// </summary>
        public LinearTransformation Transformation
        {
            get { return hasObjectSpaceHelper.Transformation; }
        }

        [XmlIgnore]
        public Material Material
        {
            get;
            set;
        }
    }
}
