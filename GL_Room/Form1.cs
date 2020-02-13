using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Collections;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneGraph.Effects;
using SharpGL.Enumerations;

namespace GL_Room
{
    public partial class Form1 : Form
    {
        private ArcBallEffect arcBallEffect;

        public Form1()
        {
            InitializeComponent();

            SceneContainer sceneContainer = new SceneContainer();// paint container
            addRoomObjects(sceneContainer);

            sceneContainer.AddChild(new Axies());// paints the x,y and z axis
            arcBallEffect = new ArcBallEffect(sceneControl1.Scene.CurrentCamera as LookAtCamera);// implements rotation per mouse move
            sceneContainer.AddEffect(arcBallEffect);
            sceneControl1.Scene.SceneContainer.AddChild(sceneContainer);
            arcBallEffect.SetViewMode(ViewMode.NO);// look diagonaly on the object

            // Hide bounding box
            toolStripButtonShowBoundingVolumes_Click(this, new EventArgs());
        }

        /// <summary>
        /// Creates the room.
        /// </summary>
        private void addRoomObjects(SceneContainer sceneContainer)
        {
            Circle circle = new Circle(new Vertex(193.93f, 100.02f, 0f), new Vertex(340.03f, 196.04f, 0f), new Vertex(305.53f, 322.04f, 0f));
            RoundColumn roundColumn = new RoundColumn(sceneControl1.OpenGL, "Column 1", circle, -1090.10747004971f, 1333.73756501464f);
            
            // ===============
            // ==== Walls ====
            // ===============
            Vertex[] vertex1 = new Vertex[4] {
            new Vertex(583.469141284528f, 1051.34483191663f, 1333.73756501464f),
            new Vertex(583.469141284529f, 1051.34483191663f, -1090.10747004971f),
            new Vertex(1227.2874745031f, 1039.2873828411f, -1090.10747004971f),
            new Vertex(1227.28747450311f, 1039.2873828411f, 1333.73756501464f)};
            SharpGL.SceneGraph.Quadrics.Wall wall01 = new Wall(vertex1, "Wall 1", sceneControl1.OpenGL);

            Vertex[] vertex5 = new Vertex[4] {
            new Vertex(-1193.92231770492f, 1095.29657502782f, 1333.73756501464f),
            new Vertex(-1233.89549066894f, -756.208094725552f, 1333.73756501464f),
            new Vertex(-1233.89549066894f, -756.208094725554f, -1090.10747004971f),
            new Vertex(-1193.92231770492f, 1095.29657502782f, -1090.10747004971f)};
            SharpGL.SceneGraph.Quadrics.Wall wall05 = new Wall(vertex5, "Wall 5", sceneControl1.OpenGL);

            Vertex[] vertex6 = new Vertex[4] {
            new Vertex(-543.564905413147f, 1080.00527771998f, 1333.73756501464f),
            new Vertex(-1193.92231770492f, 1095.29657502782f, 1333.73756501464f),
            new Vertex(-1193.92231770492f, 1095.29657502783f, -1090.10747004971f),
            new Vertex(-543.564905413147f, 1080.00527771998f, -1090.10747004971f)};
            SharpGL.SceneGraph.Quadrics.Wall wall06 = new Wall(vertex6, "Wall 6", sceneControl1.OpenGL);

            Vertex[] vertex7 = new Vertex[4] {
            new Vertex(-542.588207408772f, 1189.1435831438f, 1333.73756501464f),
            new Vertex(-543.564905413147f, 1080.00527771997f, 1333.73756501464f),
            new Vertex(-543.564905413147f, 1080.00527771998f, -1090.10747004971f),
            new Vertex(-542.588207408772f, 1189.14358314381f, -1090.10747004971f)};
            SharpGL.SceneGraph.Quadrics.Wall wall07 = new Wall(vertex7, "Wall 7", sceneControl1.OpenGL);

            Vertex[] vertex8 = new Vertex[4] {
            new Vertex(587.186904540071f, 1409.48492346394f, 1333.73756501464f),
            new Vertex(-542.588207408771f, 1189.1435831438f, 1333.73756501464f),
            new Vertex(-542.588207408772f, 1189.1435831438f, -1090.10747004971f),
            new Vertex(587.186904540071f, 1409.48492346394f, -1090.10747004971f)};
            SharpGL.SceneGraph.Quadrics.Wall wall08 = new Wall(vertex8, "Wall 8", sceneControl1.OpenGL);

            Vertex[] vertex9 = new Vertex[4] {
            new Vertex(583.469141284528f, 1051.34483191662f, 1333.73756501464f),
            new Vertex(587.18690454007f, 1409.48492346394f, 1333.73756501464f),
            new Vertex(587.18690454007f, 1409.48492346394f, -1090.10747004971f),
            new Vertex(583.469141284528f, 1051.34483191662f, -1090.10747004971f)};
            SharpGL.SceneGraph.Quadrics.Wall wall09 = new Wall(vertex9, "Wall 9", sceneControl1.OpenGL);

            Vertex[] vertex10 = new Vertex[4] {
                new Vertex(1185.71341296406f, -811.267853802761f, 1333.73756501464f),
                new Vertex(1185.71341296406f, -811.267853802761f, -1090.10747004971f),
                new Vertex(-1233.89549066894f, -756.208094725554f, -1090.10747004971f),
            new Vertex(-1233.89549066894f, -756.208094725552f, 1333.73756501464f)
            };
            SharpGL.SceneGraph.Quadrics.Wall wall10 = new Wall(vertex10, "Wall 10", sceneControl1.OpenGL);

            Vertex[] vertex11 = new Vertex[4] {
            new Vertex(1185.71341296406f, -811.267853802761f, -1090.10747004971f),
            new Vertex(1185.71341296406f, -811.267853802761f, 1333.73756501464f),
            new Vertex(1227.28747450311f, 1039.2873828411f, 1333.73756501464f),
            new Vertex(1227.2874745031f, 1039.2873828411f, -1090.10747004971f)};
            SharpGL.SceneGraph.Quadrics.Wall wall11 = new Wall(vertex11, "Wall 11", sceneControl1.OpenGL);

            Vertex[] vertex12 = new Vertex[8] {
                new Vertex(587.18690454007f, 1409.48492346394f, -1090.10747004971f),
                new Vertex(-542.588207408784f, 1189.1435831438f, -1090.10747004971f),
                new Vertex(-543.564905413165f, 1080.00527771926f, -1090.10747004971f),
                new Vertex(-1193.92231770494f, 1095.2965750271f, -1090.10747004971f),
                new Vertex(-1233.89549066894f, -756.208094725533f, -1090.10747004971f),
                new Vertex(1185.71341296406f, -811.267853802761f, -1090.10747004971f),
                new Vertex(1227.28747450289f, 1039.28738284216f, -1090.10747004971f),
            new Vertex(583.469141284539f, 1051.34483191769f, -1090.10747004971f)
            };
            SharpGL.SceneGraph.Quadrics.Wall bottom01 = new Wall(vertex12, "Bottom 1", sceneControl1.OpenGL);

            Vertex[] vertex13 = new Vertex[8] {
            new Vertex(583.469141284539f, 1051.34483191769f, 1333.73756501464f),
            new Vertex(1227.28747450289f, 1039.28738284216f, 1333.73756501464f),
            new Vertex(1185.71341296406f, -811.267853802761f, 1333.73756501464f),
            new Vertex(-1233.89549066894f, -756.208094725533f, 1333.73756501464f),
            new Vertex(-1193.92231770494f, 1095.2965750271f, 1333.73756501464f),
            new Vertex(-543.564905413165f, 1080.00527771926f, 1333.73756501464f),
            new Vertex(-542.588207408784f, 1189.1435831438f, 1333.73756501464f),
            new Vertex(587.18690454007f, 1409.48492346394f, 1333.73756501464f)};
            SharpGL.SceneGraph.Quadrics.Wall top01 = new Wall(vertex13, "Top 1", sceneControl1.OpenGL);

            // ======================
            // ==== Wall objects ====
            // ======================
            WallObject window = new WallObject("Window 1");
            // On Wall
            window.AddPoly(new Vertex[4] { new Vertex(-1222.43f, -225.33f, 580.89f), new Vertex(-1206.38f, 518.49f, 580.89f), new Vertex(-1206.38f, 518.49f, -387.11f), new Vertex(-1222.43f, -225.33f, -387.11f) }, sceneControl1.OpenGL, Materials.Green(sceneControl1.OpenGL));
            // Not on Wall
            window.AddPoly(new Vertex[4] { new Vertex(-1322.41f, -223.17f, 580.89f), new Vertex(-1306.35f, 520.65f, 580.89f), new Vertex(-1306.35f, 520.65f, -387.11f), new Vertex(-1322.41f, -223.17f, -387.11f) }, sceneControl1.OpenGL, Materials.Green(sceneControl1.OpenGL));
            // Bottom
            window.AddPoly(new Vertex[4] { new Vertex(-1322.41f, -223.17f, -387.11f), new Vertex(-1222.43f, -225.33f, -387.11f), new Vertex(-1206.38f, 518.49f, -387.11f), new Vertex(-1306.35f, 520.65f, -387.11f) }, sceneControl1.OpenGL, Materials.Green(sceneControl1.OpenGL));
            // Top
            window.AddPoly(new Vertex[4] { new Vertex(-1222.43f, -225.33f, 580.89f), new Vertex(-1206.38f, 518.49f, 580.89f), new Vertex(-1306.35f, 520.65f, 580.89f), new Vertex(-1322.41f, -223.17f, 580.89f) }, sceneControl1.OpenGL, Materials.Green(sceneControl1.OpenGL));
            // Left
            window.AddPoly(new Vertex[4] { new Vertex(-1222.43f, -225.33f, -387.11f), new Vertex(-1222.43f, -225.33f, 580.89f), new Vertex(-1322.41f, -223.17f, 580.89f), new Vertex(-1322.41f, -223.17f, -387.11f) }, sceneControl1.OpenGL, Materials.Green(sceneControl1.OpenGL));
            // Right
            window.AddPoly(new Vertex[4] { new Vertex(-1206.38f, 518.49f, 580.89f), new Vertex(-1306.35f, 520.65f, 580.89f), new Vertex(-1306.35f, 520.65f, -387.11f), new Vertex(-1206.38f, 518.49f, -387.11f) }, sceneControl1.OpenGL, Materials.Green(sceneControl1.OpenGL));

            WallObject door = new WallObject("Door 1");
            // On Wall
            door.AddPoly(new Vertex[4] { new Vertex(-466.03f, 1204.07f, 959.89f), new Vertex(341.75f, 1361.62f, 959.89f), new Vertex(341.75f, 1361.62f, -1090.11f), new Vertex(-466.03f, 1204.07f, -1090.11f) }, sceneControl1.OpenGL, Materials.Blue(sceneControl1.OpenGL));
            // Not on Wall
            door.AddPoly(new Vertex[4] { new Vertex(-485.17f, 1302.23f, 959.89f), new Vertex(322.61f, 1459.77f, 959.89f), new Vertex(322.61f, 1459.77f, -1090.11f), new Vertex(-485.17f, 1302.23f, -1090.11f) }, sceneControl1.OpenGL, Materials.Blue(sceneControl1.OpenGL));
            // Bottom
            door.AddPoly(new Vertex[4] { new Vertex(-466.03f, 1204.07f, -1090.11f), new Vertex(341.75f, 1361.62f, -1090.11f), new Vertex(322.61f, 1459.77f, -1090.11f), new Vertex(-485.17f, 1302.23f, -1090.11f) }, sceneControl1.OpenGL, Materials.Blue(sceneControl1.OpenGL));
            // Top
            door.AddPoly(new Vertex[4] { new Vertex(-466.03f, 1204.07f, 959.89f), new Vertex(341.75f, 1361.62f, 959.89f), new Vertex(322.61f, 1459.77f, 959.89f), new Vertex(-485.17f, 1302.23f, 959.89f) }, sceneControl1.OpenGL, Materials.Blue(sceneControl1.OpenGL));
            // Left
            door.AddPoly(new Vertex[4] { new Vertex(-466.03f, 1204.07f, 959.89f), new Vertex(-485.17f, 1302.23f, 959.89f), new Vertex(-485.17f, 1302.23f, -1090.11f), new Vertex(-466.03f, 1204.07f, -1090.11f) }, sceneControl1.OpenGL, Materials.Blue(sceneControl1.OpenGL));
            // Right
            door.AddPoly(new Vertex[4] { new Vertex(341.75f, 1361.62f, 959.89f), new Vertex(322.61f, 1459.77f, 959.89f), new Vertex(322.61f, 1459.77f, -1090.11f), new Vertex(341.75f, 1361.62f, -1090.11f) }, sceneControl1.OpenGL, Materials.Blue(sceneControl1.OpenGL));


            WallObject electricalOutlet = new WallObject("Electrical outlet 160x80 1");
            // On Wall
            electricalOutlet.AddPoly(new Vertex[4] { new Vertex(-1225.11f, -349.31f, 1143.51f), new Vertex(-1220.23f, -123.36f, 1143.51f), new Vertex(-1220.23f, -123.36f, 1063.51f), new Vertex(-1225.11f, -349.31f, 1063.51f) }, sceneControl1.OpenGL, Materials.Red(sceneControl1.OpenGL));
            // Not on Wall
            electricalOutlet.AddPoly(new Vertex[4] { new Vertex(-1213.11f, -349.57f, 1143.51f), new Vertex(-1208.24f, -123.62f, 1143.51f), new Vertex(-1208.24f, -123.62f, 1063.51f), new Vertex(-1213.11f, -349.57f, 1063.51f) }, sceneControl1.OpenGL, Materials.Red(sceneControl1.OpenGL));
            // Bottom
            electricalOutlet.AddPoly(new Vertex[4] { new Vertex(-1213.11f, -349.57f, 1063.51f), new Vertex(-1225.11f, -349.31f, 1063.51f), new Vertex(-1220.23f, -123.36f, 1063.51f), new Vertex(-1208.24f, -123.62f, 1063.51f) }, sceneControl1.OpenGL, Materials.Red(sceneControl1.OpenGL));
            // Top
            electricalOutlet.AddPoly(new Vertex[4] { new Vertex(-1213.11f, -349.57f, 1143.51f), new Vertex(-1225.11f, -349.31f, 1143.51f), new Vertex(-1220.23f, -123.36f, 1143.51f), new Vertex(-1208.24f, -123.62f, 1143.51f) }, sceneControl1.OpenGL, Materials.Red(sceneControl1.OpenGL));
            // Left
            electricalOutlet.AddPoly(new Vertex[4] { new Vertex(-1225.11f, -349.31f, 1063.51f), new Vertex(-1213.11f, -349.57f, 1063.51f), new Vertex(-1213.11f, -349.57f, 1143.51f), new Vertex(-1225.11f, -349.31f, 1143.51f) }, sceneControl1.OpenGL, Materials.Red(sceneControl1.OpenGL));
            // Right
            electricalOutlet.AddPoly(new Vertex[4] { new Vertex(-1208.24f, -123.62f, 1143.51f), new Vertex(-1220.23f, -123.36f, 1143.51f), new Vertex(-1220.23f, -123.36f, 1063.51f), new Vertex(-1208.24f, -123.62f, 1063.51f) }, sceneControl1.OpenGL, Materials.Red(sceneControl1.OpenGL));

            addElectricalOutletSymbol(electricalOutlet);

            sceneContainer.AddChild(wall01);
            sceneContainer.AddChild(wall05);
            sceneContainer.AddChild(wall06);
            sceneContainer.AddChild(wall07);
            sceneContainer.AddChild(wall08);
            sceneContainer.AddChild(wall09);
            sceneContainer.AddChild(wall10);
            sceneContainer.AddChild(wall11);
            sceneContainer.AddChild(bottom01);
            sceneContainer.AddChild(top01);

            sceneContainer.AddChild(window);
            sceneContainer.AddChild(door);
            sceneContainer.AddChild(electricalOutlet);
            sceneContainer.AddChild(roundColumn);

        }

        private void addElectricalOutletSymbol(WallObject electricalOutlet)
        {
            Circle circle1 = new Circle(
                new Vertex(-1224.53188886458f, -322.498404426762f, 1122.06118324471f),
                new Vertex(-1223.86485851964f, -291.602438283998f, 1138.49268794222f),
                new Vertex(-1223.25055842396f, -263.148867765258f, 1122.06118324471f));

            Circle circle2 = new Circle(
                new Vertex(-1222.20027814054f, -214.501269760972f, 1098.64295046295f),
                new Vertex(-1221.32098761231f, -173.773691779122f, 1137.98228035197f),
                new Vertex(-1220.69855328763f, -144.943354497504f, 1105.95548557759f));

            electricalOutlet.AddSymbol(circle1, sceneControl1.OpenGL);
            electricalOutlet.AddSymbol(circle2, sceneControl1.OpenGL);
        }

        void sceneControl1_MouseUp(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.MouseUp(e.X, e.Y);
        }

        void FormSceneSample_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                arcBallEffect.ArcBall.MouseMove(e.X, e.Y);
            }
        }

        void FormSceneSample_MouseDown(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.MouseDown(e.X, e.Y, sceneControl1.OpenGL);
        }

        private void FormSceneSample_MouseWheel(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.MouseWheel(e.Delta);
        }


        /// <summary>
        /// Called when [selected scene element changed].
        /// </summary>
        private void OnSelectedSceneElementChanged()
        {
            propertyGrid1.SelectedObject = SelectedSceneElement;
        }

        /// <summary>
        /// The selected scene element.
        /// </summary>
        private SceneElement selectedSceneElement = null;

        /// <summary>
        /// Gets or sets the selected scene element.
        /// </summary>
        /// <value>
        /// The selected scene element.
        /// </value>
        public SceneElement SelectedSceneElement
        {
            get { return selectedSceneElement; }
            set
            {
                selectedSceneElement = value;
                OnSelectedSceneElementChanged();
            }
        }

        private void toolStripButtonShowBoundingVolumes_Click(object sender, EventArgs e)
        {
            sceneControl1.Scene.RenderBoundingVolumes = !sceneControl1.Scene.RenderBoundingVolumes;
            toolStripButtonShowBoundingVolumes.Checked = !toolStripButtonShowBoundingVolumes.Checked;
        }

        private void sceneControl1_MouseClick(object sender, MouseEventArgs e)
        {
            //  Do a hit test.
            SelectedSceneElement = null;
            listBox1.Items.Clear();
            var itemsHit = sceneControl1.Scene.DoHitTest(e.X, e.Y);
            foreach (var item in itemsHit)
            {
                List<SceneElement> sceneElements = new List<SceneElement>();
                foreach (var sceneElement in listBox1.Items)
                    sceneElements.Add(sceneElement as SceneElement);
                if(!sceneElements.Exists(sE => sE.ToString().Equals(item.ToString())))
                    listBox1.Items.Add(item);
            }
            if (listBox1.Items.Count > 0)
            {
                listBox1.SetSelected(0, true);
                // listBox1_SelectedIndexChanged(this, null);
            }

            //sceneControl1.Scene.AddPoint(e.X, e.Y);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedSceneElement = listBox1.SelectedItem as SceneElement;
        }
    }
}
