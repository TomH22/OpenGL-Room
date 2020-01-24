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
    [Serializable()]
    public class Wall :
        SceneElement,
        IHasObjectSpace,
        /*IHasOpenGLContext,*/
        /*IRenderable,*/
        IHasMaterial,
        IVolumeBound
    {

        Polygon polyFill;
        /// <summary>
        /// Initializes a new instance of the <see cref="Quadric"/> class.
        /// </summary>
        public Wall(Vertex[] vertex1, string name, OpenGL gl)
        {
            Name = name;

            SharpGL.SceneGraph.Primitives.Polygon polyFill = new Polygon(this.Name);
            polyFill.Material = Materials.Pink(gl);
            polyFill.PolygonMode = OpenGL.GL_FILL;
            polyFill.PolygonOffset = 0f;
            polyFill.AddFaceFromVertexData(vertex1);
            polyFill.EnableCullFace = true;
            polyFill.CullFace = FrontBack.BACK;
            //polyFill.DepthFunc = DepthFunc.EQUAL;
            this.Children.Add(polyFill);

            SharpGL.SceneGraph.Primitives.Polygon polyBorder = new Polygon(this.Name);
            polyBorder.Material = Materials.DarkGrey(gl);
            polyBorder.PolygonMode = OpenGL.GL_LINE;
            polyBorder.PolygonOffset = -1f;
            polyBorder.AddFaceFromVertexData(vertex1);
            polyBorder.EnableCullFace = true;
            polyBorder.CullFace = FrontBack.BACK;
            //polyFill.DepthFunc = DepthFunc.EQUAL;
            this.Children.Add(polyBorder);

            this.polyFill = polyFill;
        }

        /// <summary>
        /// Pushes us into Object Space using the transformation into the specified OpenGL instance.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        public void PushObjectSpace(OpenGL gl)
        {
            //  Use the helper to push us into object space.
            hasObjectSpaceHelper.PushObjectSpace(gl);
        }

        /// <summary>
        /// Pops us from Object Space using the transformation into the specified OpenGL instance.
        /// </summary>
        /// <param name="gl">The gl.</param>
        public void PopObjectSpace(OpenGL gl)
        {
            //  Use the helper to pop us from object space.
            hasObjectSpaceHelper.PopObjectSpace(gl);
        }

        /// <summary>
        /// The draw style, can be filled, line, silouhette or points.
        /// </summary>
        protected DrawStyle drawStyle = DrawStyle.Fill;
        protected Orientation orientation = Orientation.Outside;
        protected Normals normals = Normals.Smooth;
        protected bool textureCoords = false;

        /// <summary>
        /// The IHasObjectSpace helper.
        /// </summary>
        private HasObjectSpaceHelper hasObjectSpaceHelper = new HasObjectSpaceHelper();

        /// <summary>
        /// Gets or sets the normal orientation.
        /// </summary>
        /// <value>
        /// The normal orientation.
        /// </value>
        [Description("What way normals face."), Category("Quadric")]
        public Orientation NormalOrientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        /// <summary>
        /// Gets the bounding volume.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public BoundingVolume BoundingVolume
        {
            get
            {
                //  todo; only create bv when vertices changed.
                polyFill.boundingVolumeHelper.BoundingVolume.FromVertices(polyFill.Vertices);
                polyFill.boundingVolumeHelper.BoundingVolume.Pad(0.1f);
                return polyFill.boundingVolumeHelper.BoundingVolume;
            }
        }

        /// <summary>
        /// Gets or sets the normal generation.
        /// </summary>
        /// <value>
        /// The normal generation.
        /// </value>
        [Description("How normals are generated."), Category("Quadric")]
        public Normals NormalGeneration
        {
            get { return normals; }
            set { normals = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [texture coords].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [texture coords]; otherwise, <c>false</c>.
        /// </value>
        [Description("Should OpenGL generate texture coordinates for the Quadric?"), Category("Quadric")]
        public bool TextureCoords
        {
            get { return textureCoords; }
            set { textureCoords = value; }
        }

        /// <summary>
        /// Gets the transformation that pushes us into object space.
        /// </summary>
        [Description("The Quadric Object Space Transformation"), Category("Quadric")]
        public LinearTransformation Transformation
        {
            get { return hasObjectSpaceHelper.Transformation; }
        }

        /// <summary>
        /// Gets the current OpenGL that the object exists in context.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public OpenGL CurrentOpenGLContext
        {
            get;
            protected set;
        }

        /// <summary>
        /// Material to be used when rendering the quadric in lighted mode.
        /// </summary>
        /// <value>
        /// The material.
        /// </value>
        [XmlIgnore]
        public Material Material
        {
            get;
            set;
        }
    }
}
