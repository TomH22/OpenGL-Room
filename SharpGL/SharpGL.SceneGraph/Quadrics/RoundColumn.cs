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
    public class RoundColumn:RoomObject, IRenderable, IVolumeBound
    {
        private DisplayList displayList;

        private Circle circle;
        private float bottomPoint;
        private float topPoint;

        // the red bounding box
        private BoundingVolumeHelper boundingVolumeHelper;

        public RoundColumn(OpenGL gl, string name, Circle circle, float bottomPoint, float topPoint) : base()
        {
            this.Name = name;
            this.circle = circle;
            this.bottomPoint = bottomPoint;
            this.topPoint = topPoint;

            boundingVolumeHelper = new BoundingVolumeHelper();

            Vertex startPoint = circle.StartPoint;
            Vertex secondPoint = circle.SecondPoint;
            Vertex endPoint = circle.EndPoint;

            // ==== Create bottom circle of column ====
            startPoint.Z = bottomPoint;
            secondPoint.Z = bottomPoint;
            endPoint.Z = bottomPoint;

            Circle bottomCircle = new Circle(new Vertex(startPoint), new Vertex(secondPoint), new Vertex(endPoint));
            bottomCircle.Material = Materials.DarkGrey(gl);
            bottomCircle.Offset = -1;
            bottomCircle.LineMode = OpenGL.GL_LINE;
            bottomCircle.DepthFunc = DepthFunc.LESS;
            AddChild(bottomCircle);

            // ==== Create top circle of column ====
            startPoint.Z = topPoint;
            secondPoint.Z = topPoint;
            endPoint.Z = topPoint;

            Circle topCircle = new Circle(new Vertex(startPoint), new Vertex(secondPoint), new Vertex(endPoint));
            topCircle.Material = Materials.DarkGrey(gl);
            topCircle.Offset = -1;
            topCircle.LineMode = OpenGL.GL_LINE;
            topCircle.DepthFunc = DepthFunc.LESS;
            AddChild(topCircle);

            // ==== Create column body ====
            // Create the display list. 

            this.Material = Materials.Purple(gl);

            displayList = new DisplayList();

            // Generate the display list and 
            displayList.Generate(gl);

            displayList.New(gl, DisplayList.DisplayListMode.CompileAndExecute);


            gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_FILL);
            gl.Begin(BeginMode.QuadStrip);
            double angle = 0;
            while (angle <= 2 * Math.PI*1.01)
            {
                double xTmp = circle.Radius * Math.Cos(angle);
                double yTmp = circle.Radius * Math.Sin(angle);

                gl.Vertex(circle.Center.X + xTmp, circle.Center.Y + yTmp, bottomPoint);
                gl.Vertex(circle.Center.X + xTmp, circle.Center.Y + yTmp, topPoint);
                angle += 0.1;
            }
            gl.End();

            // End the display list.
            displayList.End(gl);
        }

        /// <summary>
        /// Render to the provided instance of OpenGL.
        /// </summary>
        public void Render(OpenGL gl, RenderMode renderMode)
        {
            displayList.Call(gl);
        }

        public BoundingVolume BoundingVolume
        {
            get
            {
                //  todo; only create bv when vertices changed.
                boundingVolumeHelper.BoundingVolume.FromCylindricalVolume(new Vertex(circle.Center.X, circle.Center.Y, bottomPoint), (topPoint-bottomPoint), circle.Radius, circle.Radius);
                boundingVolumeHelper.BoundingVolume.Pad(0.1f);
                return boundingVolumeHelper.BoundingVolume;
            }
        }
    }
}
