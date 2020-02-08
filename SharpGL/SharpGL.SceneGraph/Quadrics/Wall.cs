using System;
using System.ComponentModel;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Helpers;
using SharpGL.SceneGraph.Assets;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Transformations;
using SharpGL.SceneGraph.Primitives;
using SharpGL.Enumerations;

namespace SharpGL.SceneGraph.Quadrics
{
    [Serializable()]
    public class Wall :
        RoomObject,
        IVolumeBound
    {
        Polygon polyFill;

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

        // The draw style, can be filled, line, silouhette or points.
        protected DrawStyle drawStyle = DrawStyle.Fill;

        protected Orientation orientation = Orientation.Outside;
        protected Normals normals = Normals.Smooth;
        protected bool textureCoords = false;

        /// <summary>
        /// Gets the bounding volume.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public BoundingVolume BoundingVolume
        {
            get
            {
                polyFill.boundingVolumeHelper.BoundingVolume.FromVertices(polyFill.Vertices);
                polyFill.boundingVolumeHelper.BoundingVolume.Pad(0.1f);
                return polyFill.boundingVolumeHelper.BoundingVolume;
            }
        }
    }
}
