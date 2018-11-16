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
    public class WallObject :
        SceneElement,
        IHasObjectSpace,
        /*IHasOpenGLContext,*/
        /*IRenderable,*/
        IHasMaterial,
        IVolumeBound
    {

        //Polygon polyTop;
        //Polygon polyBottom;
        //Polygon polyLeft;
        //Polygon polyRight;
        Polygon polyOnWall;
        //Polygon polyNotOnWall;

        public WallObject(string name)
        {
            Name = name;
        }

        public void AddPoly(Vertex[] vertex1, OpenGL gl, Material material)
        {
            SharpGL.SceneGraph.Primitives.Polygon polyFill = new Polygon(this.Name);
            polyFill.Material = material;
            polyFill.PolygonMode = OpenGL.GL_FILL;
            polyFill.PolygonOffset = false;
            polyFill.AddFaceFromVertexData(vertex1);
            this.Children.Add(polyFill);

            SharpGL.SceneGraph.Primitives.Polygon polyBorder = new Polygon(this.Name);
            polyBorder.Material = Materials.DarkGrey(gl);
            polyBorder.PolygonMode = OpenGL.GL_LINE;
            polyBorder.PolygonOffset = true;
            polyBorder.AddFaceFromVertexData(vertex1);
            this.Children.Add(polyBorder);

            this.polyOnWall = polyFill;
        }

        public void AddSymbolPoly(Vertex[] vertex1, OpenGL gl)
        {
            SharpGL.SceneGraph.Primitives.Polygon polyBorder = new Polygon(this.Name);
            polyBorder.Material = Materials.DarkGrey(gl);
            polyBorder.PolygonMode = OpenGL.GL_LINE;
            polyBorder.PolygonOffset = true;
            polyBorder.AddFaceFromVertexData(vertex1);
            this.Children.Add(polyBorder);
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
                polyOnWall.boundingVolumeHelper.BoundingVolume.FromVertices(polyOnWall.Vertices);
                polyOnWall.boundingVolumeHelper.BoundingVolume.Pad(0.1f);
                return polyOnWall.boundingVolumeHelper.BoundingVolume;
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
