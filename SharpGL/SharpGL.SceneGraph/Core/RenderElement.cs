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
using SharpGL.Enumerations;

namespace SharpGL.SceneGraph.Primitives
{
    /// <summary>
    /// Base class to draw.
    /// </summary>
    public abstract class RenderElement : SceneElement, IHasObjectSpace, IRenderable, IHasMaterial
    {
        // does transformation for this object (view)
        protected HasObjectSpaceHelper hasObjectSpaceHelper;

        public RenderElement()
        {
            hasObjectSpaceHelper = new HasObjectSpaceHelper();
        }

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
            set { hasObjectSpaceHelper.Transformation = value; }
        }

        /// <summary>
        /// Material to be used when rendering the polygon in lighted mode.
        /// This material may be overriden on a per-face basis.
        /// </summary>
        [XmlIgnore]
        public Material Material
        {
            get;
            set;
        }

        public abstract void Render(OpenGL gl, RenderMode renderMode);
    }
}