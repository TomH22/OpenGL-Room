using System;
using System.ComponentModel;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Helpers;
using SharpGL.SceneGraph.Assets;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Transformations;
using SharpGL.SceneGraph.Primitives;
using SharpGL.Enumerations;
using SharpGL.OpenGLAttributes;
using SharpGL.SceneGraph.Effects;

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
            DepthBufferAttributes depthBufferAttributes = new DepthBufferAttributes();
            depthBufferAttributes.DepthFunction = DepthFunction.LessThanOrEqual;
            depthBufferAttributes.EnableDepthTest = true;

            Name = name;

            // ==== Fill polygon ===
            PolygonAttributes polygonFillAttributes = new PolygonAttributes();
            polygonFillAttributes.PolygonMode = PolygonMode.Filled;
            polygonFillAttributes.CullFaces = FaceMode.Back;
            polygonFillAttributes.EnableCullFace = true;

            polygonFillAttributes.OffsetFactor = 0f;
            polygonFillAttributes.OffsetBias = 0;
            polygonFillAttributes.EnableOffsetFill = true;

            OpenGLAttributesEffect polyFillEffect = new OpenGLAttributesEffect();
            polyFillEffect.PolygonAttributes = polygonFillAttributes;
            //polyFillEffect.DepthBufferAttributes = depthBufferAttributes;

            SharpGL.SceneGraph.Primitives.Polygon polyFill = new Polygon(this.Name, vertex1);
            polyFill.Material = Materials.Pink(gl);
            polyFill.AddEffect(polyFillEffect);
            this.Children.Add(polyFill);

            // ==== Border polygon ===
            PolygonAttributes polygonBorderAttributes = new PolygonAttributes();
            polygonBorderAttributes.PolygonMode = PolygonMode.Lines;
            polygonBorderAttributes.OffsetFactor = -.5f;
            polygonBorderAttributes.OffsetBias = -.5f;
            polygonBorderAttributes.EnableOffsetLine = true;
            polygonBorderAttributes.CullFaces = FaceMode.Back;
            polygonBorderAttributes.EnableCullFace = true;

            OpenGLAttributesEffect polyBorderEffect = new OpenGLAttributesEffect();
            polyBorderEffect.PolygonAttributes = polygonBorderAttributes;
            //polyBorderEffect.DepthBufferAttributes = depthBufferAttributes;

            SharpGL.SceneGraph.Primitives.Polygon polyBorder = new Polygon(this.Name, vertex1);
            polyBorder.Material = Materials.DarkGrey(gl);
            polyBorder.AddEffect(polyBorderEffect);
            this.Children.Add(polyBorder);

            this.polyFill = polyFill;
        }

        public override void Render(OpenGL gl, RenderMode renderMode)
        {

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
                polyFill.boundingVolumeHelper.BoundingVolume.FromVertices(polyFill.Vertices);
                polyFill.boundingVolumeHelper.BoundingVolume.Pad(0.1f);
                return polyFill.boundingVolumeHelper.BoundingVolume;
            }
        }
    }
}
