using System;
using System.Collections.Generic;
using System.Drawing;
using static DelaunayGenericTriangulation.DelaunayGenericAlgorithm.mesh_store;

namespace constrained_delaunay_triangulation
{
    internal class Utils
    {
        public static double CalculateArea(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            // x1, y1 e.t.c are the coordinates of the triangle
            // we use Heron's formula to calculate area
            // a = √[s(s-a)(s-b)(s-c)]
            // s =1/2 perimeter of triangle, s = (a + b + c)/2.
            // a, b and c are the sides of the triangle

            // step1, find a, b and c

            double area;

            double edge1XDiff = Math.Abs(x1 - x2);
            double edge1YDiff = Math.Abs(y1 - y2);

            double edge2XDiff = Math.Abs(x2 - x3);
            double edge2YDiff = Math.Abs(y2 - y3);

            double edge3XDiff = Math.Abs(x3 - x1);
            double edge3YDiff = Math.Abs(y3 - y1);

            double a = Math.Sqrt(Math.Pow(edge1XDiff,2) + Math.Pow(edge1YDiff,2));
            double b = Math.Sqrt(Math.Pow(edge2XDiff,2) + Math.Pow(edge2YDiff,2));
            double c = Math.Sqrt(Math.Pow(edge3XDiff,2) + Math.Pow(edge3YDiff,2));

            // step2, find s
            double s = (a + b + c) / 2;

            // step3, find area
            area = Math.Sqrt(s*(s - a)*(s - b)*(s - c));

            return area;
        }
        public static double CalculateInnerRadius(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            // x1, y1 e.t.c are the coordinates of the triangle
            // we use Heron's formula to calculate area
            // a = √[s(s-a)(s-b)(s-c)]
            // s =1/2 perimeter of triangle, s = (a + b + c)/2.
            // a, b and c are the sides of the triangle

            // step1, find a, b and c

            double area;

            double edge1XDiff = Math.Abs(x1 - x2);
            double edge1YDiff = Math.Abs(y1 - y2);

            double edge2XDiff = Math.Abs(x2 - x3);
            double edge2YDiff = Math.Abs(y2 - y3);

            double edge3XDiff = Math.Abs(x3 - x1);
            double edge3YDiff = Math.Abs(y3 - y1);

            double a = Math.Sqrt(Math.Pow(edge1XDiff, 2) + Math.Pow(edge1YDiff, 2));
            double b = Math.Sqrt(Math.Pow(edge2XDiff, 2) + Math.Pow(edge2YDiff, 2));
            double c = Math.Sqrt(Math.Pow(edge3XDiff, 2) + Math.Pow(edge3YDiff, 2));

            // step2, find s
            double s = (a + b + c) / 2;

            /*
             * Radius of the incircle = area of the triangle / half of perimeter of the triangle 
                where: 
                Area of the triangle = √(p*(p-a)*(p-b)*(p-c) 
                perimeter of the triangle = (a + b + c) 
             */

            // step3, find area
            area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));

            double innerRadius = area / s;

            return innerRadius;
        }
        public static double CalculateOuterRadius(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            // x1, y1 e.t.c are the coordinates of the triangle
            // we use Heron's formula to calculate area
            // a = √[s(s-a)(s-b)(s-c)]
            // s =1/2 perimeter of triangle, s = (a + b + c)/2.
            // a, b and c are the sides of the triangle

            // step1, find a, b and c

            double area;

            double edge1XDiff = Math.Abs(x1 - x2);
            double edge1YDiff = Math.Abs(y1 - y2);

            double edge2XDiff = Math.Abs(x2 - x3);
            double edge2YDiff = Math.Abs(y2 - y3);

            double edge3XDiff = Math.Abs(x3 - x1);
            double edge3YDiff = Math.Abs(y3 - y1);

            double a = Math.Sqrt(Math.Pow(edge1XDiff, 2) + Math.Pow(edge1YDiff, 2));
            double b = Math.Sqrt(Math.Pow(edge2XDiff, 2) + Math.Pow(edge2YDiff, 2));
            double c = Math.Sqrt(Math.Pow(edge3XDiff, 2) + Math.Pow(edge3YDiff, 2));

            // step2, find s
            double s = (a + b + c) / 2;

            /*
              radius = abc / (4*√[s(s-a)(s-b)(s-c)]
 
             */

            // step3, find area
            area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));

            double outerRadius = (a * b * c) / (4 * area);

            return outerRadius;
        }
        public static double AspectRatio(double innerRadius, double outerRadius)
        {
            double aspectRatio = (2 * innerRadius) / outerRadius;
            return aspectRatio;
        }
        public static double GetLowerQuartileArea(List<double> triangleAreas)
        {
            if (triangleAreas.Count == 0) return 0;

            double minArea;
            double maxArea;
            double cutOffArea;
            double rangeArea;

            triangleAreas.Sort();

            minArea = triangleAreas[0];
            maxArea = triangleAreas[triangleAreas.Count - 1];
            rangeArea = maxArea - minArea;
            cutOffArea = minArea + (rangeArea * 0.25); // o.25 is the lower quatile

            return cutOffArea;
        }
        public static (double Alpha, double Betta, double Gamma) GetTriangleAngles(PointF A, PointF B, PointF C)
        {
            // Square of lengths be a2, b2, c2
            float a2 = LengthSquare(B, C);
            float b2 = LengthSquare(A, C);
            float c2 = LengthSquare(A, B);

            // length of sides be a, b, c
            float a = (float)Math.Sqrt(a2);
            float b = (float)Math.Sqrt(b2);
            float c = (float)Math.Sqrt(c2);

            // From Cosine law
            float alpha = (float)Math.Acos((b2 + c2 - a2) /
                                               (2 * b * c));
            float betta = (float)Math.Acos((a2 + c2 - b2) /
                                               (2 * a * c));
            float gamma = (float)Math.Acos((a2 + b2 - c2) /
                                               (2 * a * b));

            // Converting to degree
            alpha = (float)(alpha * 180 / Math.PI);
            betta = (float)(betta * 180 / Math.PI);
            gamma = (float)(gamma * 180 / Math.PI);

            return (alpha,betta,gamma);             
        }
        public static (double Alpha, double Betta, double Gamma) GetTriangleAngles(point_store A, point_store B, point_store C)
        {
            // Square of lengths be a2, b2, c2
            double a2 = LengthSquare(B, C);
            double b2 = LengthSquare(A, C);
            double c2 = LengthSquare(A, B);

            // length of sides be a, b, c
            float a = (float)Math.Sqrt(a2);
            float b = (float)Math.Sqrt(b2);
            float c = (float)Math.Sqrt(c2);

            // From Cosine law
            float alpha = (float)Math.Acos((b2 + c2 - a2) /
                                               (2 * b * c));
            float betta = (float)Math.Acos((a2 + c2 - b2) /
                                               (2 * a * c));
            float gamma = (float)Math.Acos((a2 + b2 - c2) /
                                               (2 * a * b));

            // Converting to degree
            alpha = (float)(alpha * 180 / Math.PI);
            betta = (float)(betta * 180 / Math.PI);
            gamma = (float)(gamma * 180 / Math.PI);

            return (alpha, betta, gamma);
        }
        static float LengthSquare(PointF p1, PointF p2)
        {
            // returns square of distance b/w two points
            float xDiff = p1.X - p2.X;
            float yDiff = p1.Y - p2.Y;
           
            return xDiff * xDiff + yDiff * yDiff;
        }
        static double LengthSquare(point_store p1, point_store p2)
        {
            // returns square of distance b/w two points
            double xDiff = p1.x - p2.x;
            double yDiff = p1.y - p2.y;

            return xDiff * xDiff + yDiff * yDiff;
        }
        public class Point
        {
            public int x, y;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
 
        private static bool refineChecked;

        public static bool  RefineChecked
        {
            get { return refineChecked; }
            set { refineChecked = value; }
        }

    }
}
