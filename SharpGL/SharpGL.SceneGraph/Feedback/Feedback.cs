using System;
using System.Collections;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;

namespace SharpGL.SceneGraph.Feedback
{
    /// <summary>
    /// The feedback class handles feedback easily and well. It is 100% dependant on
    /// OpenGL so use it with care!
    /// </summary>
    public class Feedback
    {
        public Feedback()
        {
        }

        /// <summary>
        /// This function begins feedback, recording the scene etc. End finishes
        /// feedback, and parses the feedback buffer.
        /// </summary>
        public virtual void Begin(OpenGL gl)
        {
            //	Set the feedback buffer.
            gl.FeedbackBuffer(feedbackBuffer.Length, OpenGL.GL_3D_COLOR_TEXTURE, feedbackBuffer);

            //	Set the rendermode to feedback.
            gl.RenderMode(OpenGL.GL_FEEDBACK);
        }

        /// <summary>
        /// This function stops the collection of feedback data.
        /// </summary>
        /// <returns>The feedback array.</returns>
        public virtual float[] End(OpenGL gl)
        {
            //	End feedback mode.
            int values = gl.RenderMode(OpenGL.GL_RENDER);

            //	Check for buffer size.
            if (values == -1)
            {
                System.Windows.Forms.MessageBox.Show("The scene contained too much data! The data buffer has been doubled in size now, please try again.");
                feedbackBuffer = new float[feedbackBuffer.Length * 2];
                return new float[] { -1 };
            }

            //	Parse the data.
            ParseData(gl, values);

            return feedbackBuffer;
        }

        /// <summary>
        /// Override this function to do custom functions on the data.
        /// </summary>
        /// <param name="values">Number of values.</param>
        protected virtual void ParseData(OpenGL gl, int values) { }

        protected float[] feedbackBuffer = new float[40960];

        public string FeedbackBufferSize
        {
            get { return (feedbackBuffer.Length * 4) + " bytes"; }
        }
    }
}