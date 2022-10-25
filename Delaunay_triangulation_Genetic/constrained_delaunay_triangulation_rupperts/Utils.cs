using System;
using System.Collections.Generic;

namespace constrained_delaunay_triangulation
{
    internal class Utils
    {
        public static double CalculateArea(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            // x1, y1 e.t.c are the coordinates of the triangle
            // we use Heron's formula to calculate area
            // s= √[s(s-a)(s-b)(s-c)]
            // s = perimeter of triangle, s = (a + b + c)/2.
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
    }
}
