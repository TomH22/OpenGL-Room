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
    public abstract class ShapeLineBased: SceneElement, IHasObjectSpace, IRenderable, IFreezable, IHasMaterial
    {
        protected Vertex startPoint;
        protected Vertex endPoint;

        // for gl.PolygonOffset(Offset, Offset)
        public float Offset;
        // OpenGL.GL_FILL or OpenGL.GL_LINE
        public uint LineMode;

        public DepthFunc DepthFunc;

        // The faces to draw.
        protected List<Face> faces;

        // The vertices to draw.
        protected List<Vertex> vertices;

        protected HasObjectSpaceHelper hasObjectSpaceHelper;
        protected FreezableHelper freezableHelper;

        public ShapeLineBased()
        {
            faces = new List<Face>();
            vertices = new List<Vertex>();
            hasObjectSpaceHelper = new HasObjectSpaceHelper();
            freezableHelper = new FreezableHelper();
        }

        public Vertex StartPoint
        {
            get { return startPoint; }
        }

        public Vertex EndPoint
        {
            get { return endPoint; }
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
        /// Freezes this instance using the provided OpenGL instance.
        /// </summary>
        public void Freeze(OpenGL gl)
        {
            //  Freeze using the helper.
            freezableHelper.Freeze(gl, this);
        }

        /// <summary>
        /// Unfreezes this instance using the provided OpenGL instance.
        /// </summary>
        public void Unfreeze(OpenGL gl)
        {
            //  Unfreeze using the helper.
            freezableHelper.Unfreeze(gl);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is frozen.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is frozen; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        [XmlIgnore]
        public bool IsFrozen
        {
            get { return freezableHelper.IsFrozen; }
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

        /// <summary>
        /// This function is cool, just stick in a set of points, it'll add them to the
        /// array, and create a face. It will take account of duplicate vertices too!
        /// </summary>
        /// <param name="vertexData">A set of vertices to make into a face.</param>
        public virtual void SetFacesFromVertexData(Vertex[] vertexData)
        {
            //	Create a face.
            Face newFace = new Face();

            //	Go through the vertices...
            foreach (Vertex v in vertexData)
            {
                //	Do we have this vertex already?
                int at = VertexSearch.Search(vertices, 0, v, 0.00001f);

                //	Add the vertex, and index it.
                if (at == -1)
                {
                    newFace.Indices.Add(new Index(vertices.Count));
                    vertices.Add(v);
                }
                else
                {
                    newFace.Indices.Add(new Index(at));
                }
            }

            //	Add the face.
            faces.Add(newFace);
        }

        public abstract void Render(OpenGL gl, RenderMode renderMode);
    }
}