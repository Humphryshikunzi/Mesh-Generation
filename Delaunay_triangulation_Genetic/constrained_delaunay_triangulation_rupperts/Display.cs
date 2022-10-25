using System;
using System.Drawing;
using System.Windows.Forms;

namespace DelaunayGenericTriangulation
{
    public partial class Display : Form
    {
        GenerateMap generateMap = new GenerateMap();
        PSLGDataStructure pslgData = new PSLGDataStructure();

        public Display()
        {
            InitializeComponent();
            triangleSize.Text = "3";
        }

        private void button_randommap_Click(object sender, EventArgs e)
        {
            StaticClass.BoundingBox = new SizeF((float)(0.8 * MainPic.Width), (float)(0.8 * MainPic.Height));
            StaticClass.BoundingMidPt = new PointF((float)(0.5 * MainPic.Width), (float)(0.5 * MainPic.Height));
            GenerateOnMap();
        }

        public void GenerateOnMap()
        {
            // generate map 
            generateMap = new GenerateMap(StaticClass.BoundingBox.Width, StaticClass.BoundingBox.Height);
            generateMap.DetectSurfaces(ref pslgData.allSurfaces);
            mtPic.Refresh();
        }
       
        public void LoadMap()
        {
            // load map 
            string filePath = MapFileName.Text; 
            generateMap = new GenerateMap(StaticClass.BoundingBox.Width, StaticClass.BoundingBox.Height, filePath);
            generateMap.DetectSurfaces(ref pslgData.allSurfaces);
            mtPic.Refresh();
        }
       
        public static class StaticClass
        {
            public static SizeF BoundingBox;
            public static PointF BoundingMidPt;

            public static bool isPaintLabel = false; // static variable to control whether to paint id or not

            public static bool isAnimateChecked = false; // static variable to control animation timing;
            public static bool IsPaintInCircle = false;
            public static bool isPaintMesh = true;

            public static int instanceCounterAt; // static variable to control instances count
            public static int inptTimerInterval = 500; // 0.5 seconds, static variable to control the interval of the timer

            public static double B_var = Math.Sqrt(2); // Ruppert's algorithm condition B_var = Math.Sqrt(2) && Paul chew's second algorithm condition (Math.Sqrt(5) * 0.5)
 
            /// <summary>
                        /// Function to convert double to single (mostly used in System.Drawing functions)
                        /// </summary>
                        /// <param name="value"></param>
                        /// <returns></returns>
            public static float ToSingle(double value)
            {
                return (float)value;
            }
      
            public static Color GetRandomColor(int hash)
            { 
                Random randomGen = new Random((hash+19)* DateTime.Now.Millisecond.GetHashCode());
                Color randomColor = Color.FromArgb(randomGen.Next(0,256), randomGen.Next(0,256), randomGen.Next(0,256));
                return randomColor;
            }

            public static System.Drawing.Drawing2D.HatchStyle GetRandomHatchStyle(int hash)
            { 
                int randomhatchindex = hash > 6 ? 0 : hash;
                System.Drawing.Drawing2D.HatchStyle style = new System.Drawing.Drawing2D.HatchStyle();

                switch (randomhatchindex)
                {
                    case 0:
                        style = System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal;
                        break;
                    case 1:
                        style = System.Drawing.Drawing2D.HatchStyle.DashedVertical;
                        break;
                    case 2:
                        style = System.Drawing.Drawing2D.HatchStyle.Cross;
                        break;
                    case 3:
                        style = System.Drawing.Drawing2D.HatchStyle.DiagonalCross;
                        break;
                    case 4:
                        style = System.Drawing.Drawing2D.HatchStyle.HorizontalBrick;
                        break;
                    case 5:
                        style = System.Drawing.Drawing2D.HatchStyle.LightDownwardDiagonal;
                        break;
                    case 6:
                        style = System.Drawing.Drawing2D.HatchStyle.LightUpwardDiagonal;
                        break;
                    default:
                        break;
                }

                return style;
            }

            // Return the angle ABC.
            // Return a value between PI and -PI.
            // Note that the value is the opposite of what you might
            // expect because Y coordinates increase downward.
            public static double GetAngle(double Ax, double Ay, double Bx, double By, double Cx, double Cy)
            {
                // Get the dot product.
                double dotProduct = DotProduct(Ax, Ay, Bx, By, Cx, Cy);

                // Get the cross product.
                double crossProduct = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);

                // Calculate the angle.
                return Math.Atan2(crossProduct, dotProduct);
            }

            // Return the cross product AB x BC.
            // The cross product is a vector perpendicular to AB
            // and BC having length |AB| * |BC| * Sin(theta) and
            // with direction given by the right-hand rule.
            // For two vectors in the X-Y plane, the result is a
            // vector with X and Y components 0 so the Z component
            // gives the vector's length and direction.
            public static double CrossProductLength(double Ax, double Ay, double Bx, double By, double Cx, double Cy)
            {
                // Get the vectors' coordinates.
                double BAx = Ax - Bx;
                double BAy = Ay - By;
                double BCx = Cx - Bx;
                double BCy = Cy - By;

                // Calculate the Z coordinate of the cross product.
                return (BAx * BCy - BAy * BCx);
            }

            // Return the dot product AB · BC.
            // Note that AB · BC = |AB| * |BC| * Cos(theta).
            private static double DotProduct(double Ax, double Ay, double Bx, double By, double Cx, double Cy)
            {
                // Get the vectors' coordinates.
                double BAx = Ax - Bx;
                double BAy = Ay - By;
                double BCx = Cx - Bx;
                double BCy = Cy - By;

                // Calculate the dot product.
                return (BAx * BCx + BAy * BCy);
            }
 
            // Return True if the point is in the polygon.
            public static bool IsPointInPolygon(PointF[] polygon, PointF testPoint)
            {
                // Get the angle between the point and the
                // first and last vertices.
                int maxPoint = polygon.Length - 1;
                double totalAngle = GetAngle(
                    polygon[maxPoint].X, polygon[maxPoint].Y,
                    testPoint.X, testPoint.Y,
                    polygon[0].X, polygon[0].Y);

                // Add the angles from the point
                // to each other pair of vertices.
                for (int i = 0; i < maxPoint; i++)
                {
                    totalAngle += GetAngle(
                        polygon[i].X, polygon[i].Y,
                        testPoint.X, testPoint.Y,
                        polygon[i + 1].X, polygon[i + 1].Y);
                }

                // The total angle should be 2 * PI or -2 * PI if
                // the point is in the polygon and close to zero
                // if the point is outside the polygon.
                // The following statement was changed. See the comments.
                //return (Math.Abs(totalAngle) > 0.000001);
                return (Math.Abs(totalAngle) > 1);
            }

            public static double SignedPolygonArea(PointF[] polygon)
            {
                //The total calculated area is negative if the polygon is oriented clockwise

                // Add the first point to the end.
                int numPoints = polygon.Length;
                PointF[] pts = new PointF[numPoints + 1];
                polygon.CopyTo(pts, 0);
                pts[numPoints] = polygon[0];

                // Get the areas.
                double area = 0;
                for (int i = 0; i < numPoints; i++)
                {
                    area +=
                        (pts[i + 1].X - pts[i].X) *
                        (pts[i + 1].Y + pts[i].Y) / 2;
                }

                // Return the result.
                return area;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StaticClass.BoundingBox = new SizeF((float)(0.5 * MainPic.Width), (float)(0.5 * MainPic.Height));
            StaticClass.BoundingMidPt = new PointF((float)(0.5 * MainPic.Width), (float)(0.5 * MainPic.Height)); 
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            StaticClass.BoundingBox = new SizeF((float)(0.8 * MainPic.Width), (float)(0.8 * MainPic.Height));
            StaticClass.BoundingMidPt = new PointF((float)(0.5 * MainPic.Width), (float)(0.5 * MainPic.Height));
            mtPic.Refresh();
        }

        private void MainPic_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; // Paint high quality
            e.Graphics.TranslateTransform(StaticClass.BoundingMidPt.X, StaticClass.BoundingMidPt.Y); // Translate transform to make the orgin as center
             
            Graphics gr1 = e.Graphics; 
            pslgData.paint_me(ref gr1);
        }

        private void MainPic_MouseClick(object sender, MouseEventArgs e)
        {
            // mouse click event to create or delete mesh
            if (string.IsNullOrEmpty(triangleSize.Text)) triangleSize.Text = "3";
            if (e.Button == MouseButtons.Left)
            {
                // create the mesh
                pslgData.SelectAndSetMesh(e.X - StaticClass.BoundingMidPt.X, e.Y - StaticClass.BoundingMidPt.Y, true, float.Parse(triangleSize.Text));
            }

            if (e.Button == MouseButtons.Right)
            {
                // delete the mesh
                pslgData.SelectAndSetMesh(e.X - StaticClass.BoundingMidPt.X, e.Y - StaticClass.BoundingMidPt.Y, false, float.Parse(triangleSize.Text));
            }

            mtPic.Refresh();
        }

        private void MainPic_MouseMove(object sender, MouseEventArgs e)
        {
            LabelXYLocation.Text = "(" + (e.X- StaticClass.BoundingMidPt.X).ToString() + ", " + (e.Y- StaticClass.BoundingMidPt.Y).ToString() + " )";
            LabelXYLocation.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StaticClass.BoundingBox = new SizeF((float)(0.8 * MainPic.Width), (float)(0.8 * MainPic.Height));
            StaticClass.BoundingMidPt = new PointF((float)(0.5 * MainPic.Width), (float)(0.5 * MainPic.Height));
            LoadMap();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
