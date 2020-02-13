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
    /// Base class of all line or arc classes.
    /// </summary>
    public abstract class ShapeLineBased: RenderElement
    {
        protected Vertex startPoint;
        protected Vertex endPoint;

        #region draw variables (are set)
        // The faces to draw.
        protected List<Face> faces;

        // The vertices to draw.
        protected List<Vertex> vertices;
        #endregion

        public ShapeLineBased():base()
        {
            faces = new List<Face>();
            vertices = new List<Vertex>();
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
    }
}