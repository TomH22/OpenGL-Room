// MIT License
// Copyright (c) 2018 OpenGL Room Pty Ltd

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
// and associated documentation files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
// subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial 
// portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
// LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


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
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Collections;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneGraph.Effects;

namespace GL_Room
{
    public partial class Form1 : Form
    {
        private ArcBallEffect arcBallEffect = new ArcBallEffect();

        public Form1()
        {
            InitializeComponent();

            // ==== Arc ball effect ====
            sceneControl1.MouseDown += new MouseEventHandler(FormSceneSample_MouseDown);
            sceneControl1.MouseMove += new MouseEventHandler(FormSceneSample_MouseMove);
            sceneControl1.MouseUp += new MouseEventHandler(sceneControl1_MouseUp);
            sceneControl1.MouseWheel += FormSceneSample_MouseWheel;

            SceneContainer sceneContainer = new SceneContainer();
            addRoomObjects(sceneContainer);

            sceneContainer.AddChild(new Axies());
            sceneContainer.AddEffect(arcBallEffect);
            sceneControl1.Scene.SceneContainer.AddChild(sceneContainer);

            // Hide bounding box
            toolStripButtonShowBoundingVolumes_Click(this, new EventArgs());
        }

        private void addRoomObjects(SceneContainer sceneContainer)
        {
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

            Vertex[] vertex11 = new Vertex[8] {
            new Vertex(583.469141284539f, 1051.34483191769f, -1090.10747004971f),
            new Vertex(1227.28747450289f, 1039.28738284216f, -1090.10747004971f),
            new Vertex(1185.71341296406f, -811.267853802761f, -1090.10747004971f),
            new Vertex(-1233.89549066894f, -756.208094725533f, -1090.10747004971f),
            new Vertex(-1193.92231770494f, 1095.2965750271f, -1090.10747004971f),
            new Vertex(-543.564905413165f, 1080.00527771926f, -1090.10747004971f),
            new Vertex(-542.588207408784f, 1189.1435831438f, -1090.10747004971f),
            new Vertex(587.18690454007f, 1409.48492346394f, -1090.10747004971f)};
            SharpGL.SceneGraph.Quadrics.Wall bottom01 = new Wall(vertex11, "Bottom 1", sceneControl1.OpenGL);

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
            sceneContainer.AddChild(bottom01);

            sceneContainer.AddChild(window);
            sceneContainer.AddChild(door);
            sceneContainer.AddChild(electricalOutlet);
        }

        private void addElectricalOutletSymbol(WallObject electricalOutlet)
        {
            electricalOutlet.AddSymbolPoly(
            new Vertex[90] { new Vertex(-1223.1357650527f, -257.831790155614f, 1103.51400899655f),
                new Vertex(-1223.13760531186f, -257.91702853364f, 1101.0725324155f),
                new Vertex(-1223.14311712378f, -258.172328394911f, 1098.64295046295f),
                new Vertex(-1223.15227363547f, -258.596445944164f, 1096.23709981793f),
                new Vertex(-1223.16503023732f, -259.187314923332f, 1093.86670154295f),
                new Vertex(-1223.18132478046f, -259.942056678142f, 1091.54330398015f),
                new Vertex(-1223.2010778795f, -260.856994182631f, 1089.27822648889f),
                new Vertex(-1223.22419329933f, -261.927669953247f, 1087.08250429904f),
                new Vertex(-1223.25055842396f, -263.148867765258f, 1084.96683474839f),
                new Vertex(-1223.28004480514f, -264.514638065679f, 1082.94152516631f),
                new Vertex(-1223.31250878819f, -266.018326958894f, 1081.01644265752f),
                new Vertex(-1223.34779221184f, -267.652608623764f, 1079.20096603048f),
                new Vertex(-1223.38572317881f, -269.409521004297f, 1077.50394010484f),
                new Vertex(-1223.42611689323f, -271.280504599977f, 1075.93363262031f),
                new Vertex(-1223.468776561f, -273.256444166799f, 1074.49769395712f),
                new Vertex(-1223.51349434848f, -275.327713125812f, 1073.20311986409f),
                new Vertex(-1223.56005239513f, -277.484220462852f, 1072.05621737608f),
                new Vertex(-1223.60822387481f, -279.715459890944f, 1071.06257408671f),
                new Vertex(-1223.65777410093f, -282.010561035877f, 1070.22703092622f),
                new Vertex(-1223.70846166975f, -284.358342395578f, 1069.55365857689f),
                new Vertex(-1223.76003963654f, -286.747365815258f, 1069.04573764112f),
                new Vertex(-1223.81225671863f, -289.165992212962f, 1068.70574265866f),
                new Vertex(-1223.86485851964f, -291.602438283998f, 1068.53533005088f),
                new Vertex(-1223.9175887689f, -294.044833908021f, 1068.53533005088f),
                new Vertex(-1223.97019056991f, -296.481279979058f, 1068.70574265866f),
                new Vertex(-1224.022407652f, -298.899906376762f, 1069.04573764112f),
                new Vertex(-1224.07398561879f, -301.288929796442f, 1069.55365857689f),
                new Vertex(-1224.12467318761f, -303.636711156142f, 1070.22703092622f),
                new Vertex(-1224.17422341373f, -305.931812301076f, 1071.06257408671f),
                new Vertex(-1224.22239489341f, -308.163051729168f, 1072.05621737608f),
                new Vertex(-1224.26895294006f, -310.319559066208f, 1073.20311986409f),
                new Vertex(-1224.31367072754f, -312.390828025221f, 1074.49769395712f),
                new Vertex(-1224.35633039531f, -314.366767592042f, 1075.93363262031f),
                new Vertex(-1224.39672410973f, -316.237751187723f, 1077.50394010484f),
                new Vertex(-1224.4346550767f, -317.994663568255f, 1079.20096603048f),
                new Vertex(-1224.46993850035f, -319.628945233126f, 1081.01644265752f),
                new Vertex(-1224.5024024834f, -321.13263412634f, 1082.94152516631f),
                new Vertex(-1224.53188886458f, -322.498404426762f, 1084.96683474839f),
                new Vertex(-1224.55825398921f, -323.719602238773f, 1087.08250429904f),
                new Vertex(-1224.58136940904f, -324.790278009388f, 1089.27822648889f),
                new Vertex(-1224.60112250808f, -325.705215513877f, 1091.54330398015f),
                new Vertex(-1224.61741705122f, -326.459957268687f, 1093.86670154295f),
                new Vertex(-1224.63017365307f, -327.050826247855f, 1096.23709981793f),
                new Vertex(-1224.63933016476f, -327.474943797108f, 1098.64295046295f),
                new Vertex(-1224.64484197668f, -327.730243658379f, 1101.0725324155f),
                new Vertex(-1224.64668223584f, -327.815482036406f, 1103.51400899655f),
                new Vertex(-1224.64484197668f, -327.730243658379f, 1105.95548557759f),
                new Vertex(-1224.63933016476f, -327.474943797108f, 1108.38506753015f),
                new Vertex(-1224.63017365307f, -327.050826247855f, 1110.79091817517f),
                new Vertex(-1224.61741705122f, -326.459957268687f, 1113.16131645014f),
                new Vertex(-1224.60112250808f, -325.705215513877f, 1115.48471401295f),
                new Vertex(-1224.58136940904f, -324.790278009388f, 1117.7497915042f),
                new Vertex(-1224.55825398921f, -323.719602238773f, 1119.94551369405f),
                new Vertex(-1224.53188886458f, -322.498404426762f, 1122.06118324471f),
                new Vertex(-1224.5024024834f, -321.13263412634f, 1124.08649282678f),
                new Vertex(-1224.46993850035f, -319.628945233126f, 1126.01157533558f),
                new Vertex(-1224.4346550767f, -317.994663568255f, 1127.82705196261f),
                new Vertex(-1224.39672410973f, -316.237751187723f, 1129.52407788826f),
                new Vertex(-1224.35633039531f, -314.366767592042f, 1131.09438537278f),
                new Vertex(-1224.31367072754f, -312.390828025221f, 1132.53032403597f),
                new Vertex(-1224.26895294006f, -310.319559066208f, 1133.824898129f),
                new Vertex(-1224.22239489341f, -308.163051729168f, 1134.97180061702f),
                new Vertex(-1224.17422341373f, -305.931812301076f, 1135.96544390639f),
                new Vertex(-1224.12467318761f, -303.636711156142f, 1136.80098706688f),
                new Vertex(-1224.07398561879f, -301.288929796442f, 1137.47435941621f),
                new Vertex(-1224.022407652f, -298.899906376762f, 1137.98228035197f),
                new Vertex(-1223.97019056991f, -296.481279979058f, 1138.32227533444f),
                new Vertex(-1223.9175887689f, -294.044833908021f, 1138.49268794222f),
                new Vertex(-1223.86485851964f, -291.602438283998f, 1138.49268794222f),
                new Vertex(-1223.81225671863f, -289.165992212962f, 1138.32227533444f),
                new Vertex(-1223.76003963654f, -286.747365815258f, 1137.98228035197f),
                new Vertex(-1223.70846166975f, -284.358342395578f, 1137.47435941621f),
                new Vertex(-1223.65777410093f, -282.010561035877f, 1136.80098706688f),
                new Vertex(-1223.60822387481f, -279.715459890944f, 1135.96544390639f),
                new Vertex(-1223.56005239513f, -277.484220462852f, 1134.97180061702f),
                new Vertex(-1223.51349434848f, -275.327713125812f, 1133.824898129f),
                new Vertex(-1223.468776561f, -273.256444166799f, 1132.53032403597f),
                new Vertex(-1223.42611689323f, -271.280504599977f, 1131.09438537278f),
                new Vertex(-1223.38572317881f, -269.409521004297f, 1129.52407788826f),
                new Vertex(-1223.34779221184f, -267.652608623764f, 1127.82705196261f),
                new Vertex(-1223.31250878819f, -266.018326958894f, 1126.01157533558f),
                new Vertex(-1223.28004480514f, -264.514638065679f, 1124.08649282678f),
                new Vertex(-1223.25055842396f, -263.148867765258f, 1122.06118324471f),
                new Vertex(-1223.22419329933f, -261.927669953247f, 1119.94551369405f),
                new Vertex(-1223.2010778795f, -260.856994182631f, 1117.7497915042f),
                new Vertex(-1223.18132478046f, -259.942056678142f, 1115.48471401295f),
                new Vertex(-1223.16503023732f, -259.187314923332f, 1113.16131645014f),
                new Vertex(-1223.15227363547f, -258.596445944164f, 1110.79091817517f),
                new Vertex(-1223.14311712378f, -258.172328394911f, 1108.38506753015f),
                new Vertex(-1223.13760531186f, -257.91702853364f, 1105.95548557759f)}, sceneControl1.OpenGL);

            electricalOutlet.AddSymbolPoly(
            new Vertex[90] { new Vertex(-1220.69671302847f, -144.858116119478f, 1103.51400899655f),
                new Vertex(-1220.69855328763f, -144.943354497504f, 1101.0725324155f),
                new Vertex(-1220.70406509955f, -145.198654358775f, 1098.64295046295f),
                new Vertex(-1220.71322161124f, -145.622771908028f, 1096.23709981793f),
                new Vertex(-1220.7259782131f, -146.213640887196f, 1093.86670154295f),
                new Vertex(-1220.74227275623f, -146.968382642006f, 1091.54330398015f),
                new Vertex(-1220.76202585527f, -147.883320146495f, 1089.27822648889f),
                new Vertex(-1220.78514127511f, -148.95399591711f, 1087.08250429904f),
                new Vertex(-1220.81150639973f, -150.175193729122f, 1084.96683474839f),
                new Vertex(-1220.84099278091f, -151.540964029543f, 1082.94152516631f),
                new Vertex(-1220.87345676396f, -153.044652922758f, 1081.01644265752f),
                new Vertex(-1220.90874018762f, -154.678934587628f, 1079.20096603048f),
                new Vertex(-1220.94667115458f, -156.435846968161f, 1077.50394010484f),
                new Vertex(-1220.98706486901f, -158.306830563841f, 1075.93363262031f),
                new Vertex(-1221.02972453677f, -160.282770130662f, 1074.49769395712f),
                new Vertex(-1221.07444232426f, -162.354039089676f, 1073.20311986409f),
                new Vertex(-1221.1210003709f, -164.510546426715f, 1072.05621737608f),
                new Vertex(-1221.16917185059f, -166.741785854807f, 1071.06257408671f),
                new Vertex(-1221.2187220767f, -169.036886999741f, 1070.22703092622f),
                new Vertex(-1221.26940964552f, -171.384668359441f, 1069.55365857689f),
                new Vertex(-1221.32098761231f, -173.773691779122f, 1069.04573764112f),
                new Vertex(-1221.3732046944f, -176.192318176826f, 1068.70574265866f),
                new Vertex(-1221.42580649542f, -178.628764247862f, 1068.53533005088f),
                new Vertex(-1221.47853674467f, -181.071159871885f, 1068.53533005088f),
                new Vertex(-1221.53113854568f, -183.507605942922f, 1068.70574265866f),
                new Vertex(-1221.58335562777f, -185.926232340625f, 1069.04573764112f),
                new Vertex(-1221.63493359456f, -188.315255760306f, 1069.55365857689f),
                new Vertex(-1221.68562116339f, -190.663037120006f, 1070.22703092622f),
                new Vertex(-1221.7351713895f, -192.95813826494f, 1071.06257408671f),
                new Vertex(-1221.78334286918f, -195.189377693032f, 1072.05621737608f),
                new Vertex(-1221.82990091583f, -197.345885030072f, 1073.20311986409f),
                new Vertex(-1221.87461870332f, -199.417153989085f, 1074.49769395712f),
                new Vertex(-1221.91727837108f, -201.393093555906f, 1075.93363262031f),
                new Vertex(-1221.9576720855f, -203.264077151587f, 1077.50394010484f),
                new Vertex(-1221.99560305247f, -205.020989532119f, 1079.20096603048f),
                new Vertex(-1222.03088647613f, -206.655271196989f, 1081.01644265752f),
                new Vertex(-1222.06335045917f, -208.158960090204f, 1082.94152516631f),
                new Vertex(-1222.09283684035f, -209.524730390626f, 1084.96683474839f),
                new Vertex(-1222.11920196498f, -210.745928202637f, 1087.08250429904f),
                new Vertex(-1222.14231738481f, -211.816603973252f, 1089.27822648889f),
                new Vertex(-1222.16207048386f, -212.731541477741f, 1091.54330398015f),
                new Vertex(-1222.17836502699f, -213.486283232551f, 1093.86670154295f),
                new Vertex(-1222.19112162885f, -214.077152211719f, 1096.23709981793f),
                new Vertex(-1222.20027814054f, -214.501269760972f, 1098.64295046295f),
                new Vertex(-1222.20578995246f, -214.756569622243f, 1101.0725324155f),
                new Vertex(-1222.20763021162f, -214.84180800027f, 1103.51400899655f),
                new Vertex(-1222.20578995246f, -214.756569622243f, 1105.95548557759f),
                new Vertex(-1222.20027814054f, -214.501269760972f, 1108.38506753015f),
                new Vertex(-1222.19112162885f, -214.077152211719f, 1110.79091817517f),
                new Vertex(-1222.17836502699f, -213.486283232551f, 1113.16131645014f),
                new Vertex(-1222.16207048386f, -212.731541477741f, 1115.48471401295f),
                new Vertex(-1222.14231738481f, -211.816603973252f, 1117.7497915042f),
                new Vertex(-1222.11920196498f, -210.745928202637f, 1119.94551369405f),
                new Vertex(-1222.09283684035f, -209.524730390626f, 1122.06118324471f),
                new Vertex(-1222.06335045917f, -208.158960090204f, 1124.08649282678f),
                new Vertex(-1222.03088647613f, -206.655271196989f, 1126.01157533558f),
                new Vertex(-1221.99560305247f, -205.020989532119f, 1127.82705196261f),
                new Vertex(-1221.9576720855f, -203.264077151587f, 1129.52407788826f),
                new Vertex(-1221.91727837108f, -201.393093555906f, 1131.09438537278f),
                new Vertex(-1221.87461870332f, -199.417153989085f, 1132.53032403597f),
                new Vertex(-1221.82990091583f, -197.345885030072f, 1133.824898129f),
                new Vertex(-1221.78334286918f, -195.189377693032f, 1134.97180061702f),
                new Vertex(-1221.7351713895f, -192.95813826494f, 1135.96544390639f),
                new Vertex(-1221.68562116339f, -190.663037120006f, 1136.80098706688f),
                new Vertex(-1221.63493359456f, -188.315255760306f, 1137.47435941621f),
                new Vertex(-1221.58335562777f, -185.926232340625f, 1137.98228035197f),
                new Vertex(-1221.53113854568f, -183.507605942922f, 1138.32227533444f),
                new Vertex(-1221.47853674467f, -181.071159871885f, 1138.49268794222f),
                new Vertex(-1221.42580649542f, -178.628764247862f, 1138.49268794222f),
                new Vertex(-1221.3732046944f, -176.192318176826f, 1138.32227533444f),
                new Vertex(-1221.32098761231f, -173.773691779122f, 1137.98228035197f),
                new Vertex(-1221.26940964552f, -171.384668359441f, 1137.47435941621f),
                new Vertex(-1221.2187220767f, -169.036886999741f, 1136.80098706688f),
                new Vertex(-1221.16917185059f, -166.741785854807f, 1135.96544390639f),
                new Vertex(-1221.1210003709f, -164.510546426715f, 1134.97180061702f),
                new Vertex(-1221.07444232426f, -162.354039089676f, 1133.824898129f),
                new Vertex(-1221.02972453677f, -160.282770130662f, 1132.53032403597f),
                new Vertex(-1220.98706486901f, -158.306830563841f, 1131.09438537278f),
                new Vertex(-1220.94667115458f, -156.435846968161f, 1129.52407788826f),
                new Vertex(-1220.90874018762f, -154.678934587628f, 1127.82705196261f),
                new Vertex(-1220.87345676396f, -153.044652922758f, 1126.01157533558f),
                new Vertex(-1220.84099278091f, -151.540964029543f, 1124.08649282678f),
                new Vertex(-1220.81150639973f, -150.175193729122f, 1122.06118324471f),
                new Vertex(-1220.78514127511f, -148.95399591711f, 1119.94551369405f),
                new Vertex(-1220.76202585527f, -147.883320146495f, 1117.7497915042f),
                new Vertex(-1220.74227275623f, -146.968382642006f, 1115.48471401295f),
                new Vertex(-1220.7259782131f, -146.213640887196f, 1113.16131645014f),
                new Vertex(-1220.71322161124f, -145.622771908028f, 1110.79091817517f),
                new Vertex(-1220.70406509955f, -145.198654358775f, 1108.38506753015f),
                new Vertex(-1220.69855328763f, -144.943354497504f, 1105.95548557759f)}, sceneControl1.OpenGL);
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
            arcBallEffect.ArcBall.SetBounds(sceneControl1.Width, sceneControl1.Height);
            arcBallEffect.ArcBall.MouseDown(e.X, e.Y);
        }

        private void FormSceneSample_MouseWheel(object sender, MouseEventArgs e)
        {
            //arcBallEffect.ArcBall.SetBounds(sceneControl1.Width, sceneControl1.Height);
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

            sceneControl1.Scene.AddPoint(e.X, e.Y);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedSceneElement = listBox1.SelectedItem as SceneElement;
        }
    }
}
