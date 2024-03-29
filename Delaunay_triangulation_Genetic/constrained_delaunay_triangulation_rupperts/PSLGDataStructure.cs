﻿using constrained_delaunay_triangulation;
using System;
using System.Collections.Generic; 
using System.Drawing;
using System.IO;

namespace DelaunayGenericTriangulation
{
    public class PSLGDataStructure
    {       
        public void SelectAndSetMesh(double x, double y, bool is_create, float h, bool refine)
        {
            // this is a call to set or delete the mesh
            int surf_index = -1; //variable to store the index
            foreach (SurfaceStore surf in allSurfaces)
            {
                if (surf.pointinsurface(x, y) == true) // find the surface where the point lies
                {
                    surf_index = allSurfaces.FindIndex(obj => obj.surface_id == surf.surface_id); // add the index of the surface to the main index;
                    break;
                }
            }

            List<int> inner_surf_index = new List<int>(); // variable to store inner surface index
            if (surf_index != -1) // if not equal to -1 then surface is found
            {
                foreach (SurfaceStore inner_surf in allSurfaces[surf_index].inner_surfaces)
                {
                    inner_surf_index.Add(allSurfaces.FindIndex(obj => obj.surface_id == inner_surf.surface_id)); // add the index of inner surfaces to the list
                }
            }
            else
            {
                return; //exit the function if no surfaces are found
            }

            if (is_create == true) // create the mesh
            {
                DelaunayGenericAlgorithm.create_constrained_mesh(surf_index, inner_surf_index, ref allSurfaces, h, refine);
            }
            else // delete the mesh
            {
                DelaunayGenericAlgorithm.delete_constrained_mesh(surf_index, inner_surf_index, ref allSurfaces);
            }
        }

        public void SelectAndSetFullMesh(double x, double y, bool is_create, float h, bool refine)
        {
            // this is a call to set or delete the mesh
            int surf_index = -1; //variable to store the index
            foreach (SurfaceStore surf in allSurfaces)
            {
                surf_index = allSurfaces.FindIndex(obj => obj.surface_id == surf.surface_id); // add the index of the surface to the main index;

                List<int> inner_surf_index = new List<int>(); // variable to store inner surface index
                if (surf_index != -1) // if not equal to -1 then surface is found
                {
                    foreach (SurfaceStore inner_surf in allSurfaces[surf_index].inner_surfaces)
                    {
                        inner_surf_index.Add(allSurfaces.FindIndex(obj => obj.surface_id == inner_surf.surface_id)); // add the index of inner surfaces to the list
                    }
                }
                else
                {
                    return; //exit the function if no surfaces are found
                }

                if (is_create == true) // create the mesh
                { 
                    DelaunayGenericAlgorithm.create_constrained_mesh(surf_index, inner_surf_index, ref allSurfaces, h, refine);                
                }
                else // delete the mesh
                {
                    DelaunayGenericAlgorithm.delete_constrained_mesh(surf_index, inner_surf_index, ref allSurfaces);
                }
            }
        }

        public void paint_me(ref Graphics gr1)
        {
            Graphics gr0 = gr1;
            allSurfaces.ForEach(obj => obj.paint_me(ref gr0)); // Paint the surface and its associated mesh
        }

        public List<SurfaceStore> allSurfaces = new List<SurfaceStore>(); // List of all detected surfaces (visible outside)

        public class mesh2d
        {
            List<point2d> _all_points = new List<point2d>(); // List of point object to store all the points in the drawing area
            List<edge2d> _all_edges = new List<edge2d>(); // List of edge object to store all the edges created from Delaunay triangulation
            List<triangle2d> _all_triangles = new List<triangle2d>(); // List of face object to store all the faces created from Delaunay triangulation

            public List<point2d> all_points
            {
                get { return this._all_points; }
            }

            public List<edge2d> all_edges
            {
                get { return this._all_edges; }
            }

            public List<triangle2d> all_triangles
            {
                get { return this._all_triangles; }
            }

            public mesh2d()
            {
                // Empty constructor
            }

            public mesh2d(List<point2d> i_all_pts, List<edge2d> i_all_edg, List<triangle2d> i_all_fcs)
            {
                this._all_points = i_all_pts;
                this._all_edges = i_all_edg;
                this._all_triangles = i_all_fcs;

                // save the vertices of triangle here 

                using (StreamWriter writer = new StreamWriter("mapFileVertices.csv"))
                {
                    for (int i = 0; i < i_all_pts.Count; i++)
                    {
                        writer.Write(i_all_pts[i].id + "," + i_all_pts[i].x + "," + i_all_pts[i].y);
                        writer.WriteLine();
                    }
                }
            }

            public void paint_me(ref Graphics gr0) // this function is used to paint the mesh
            {
                Graphics gr1 = gr0;
                List<double> triangleAreas = new List<double>();
                double q1;

                Pen temp_edge_pen = new Pen(Color.Black, 1);
                all_edges.ForEach(obj => obj.paint_me(ref gr1, ref temp_edge_pen)); // Paint the edges

                Pen temp_node_pen = new Pen(Color.DarkRed, 2);
                all_points.ForEach(obj => obj.paint_me(ref gr1, ref temp_node_pen)); // Paint the nodes

                // get triangle areas
                all_triangles.ForEach(obj => triangleAreas.Add(obj.Area));
                q1 = Utils.GetLowerQuartileArea(triangleAreas);
                
                Pen temp_tri_pen = new Pen(Color.Black, 1); 
                all_triangles.ForEach(obj => obj.paint_me(ref gr1, ref temp_tri_pen, obj.Area, q1)); // Paint the faces              
            }
        }

        public class SurfaceStore
        {
            // surface variables
            private int _surface_id;
            public List<point2d> surface_nodes = new List<point2d>();
            public List<point2d> inner_nodes = new List<point2d>();

            public List<edge2d> surface_edges = new List<edge2d>();
            private int _encapsulating_surface_id;
            public List<edge2d> encapsulating_seed_edges = new List<edge2d>();
            public List<SurfaceStore> inner_surfaces = new List<SurfaceStore>();

            // mesh variable
            public bool is_meshed;
            public mesh2d my_mesh = new mesh2d();

            // Drawing aid for the surface
            System.Drawing.Drawing2D.HatchBrush tri_brush;
            private Region surface_region = new Region();

            // Surface area
            public double surface_area;

            public int surface_id
            {
                get { return this._surface_id; }
            }

            public int encapsulating_surface_id
            {
                get { return this._encapsulating_surface_id; }
            }

            public SurfaceStore(int i_surf_id, List<point2d> i_surface_nodes, List<edge2d> i_surface_edges, int surf_count)
            {

                this._surface_id = i_surf_id; // add surface id
                surface_nodes.AddRange(i_surface_nodes);
                surface_edges.AddRange(i_surface_edges);
                is_meshed = false;
                _encapsulating_surface_id = -1;

                List<PointF> temp_sur_pts = new List<PointF>();

                temp_sur_pts.Add(this.surface_edges[0].start_pt.get_point()); // Add the first point of the surface edge
                foreach (edge2d ed in this.surface_edges)
                {
                    temp_sur_pts.Add(ed.end_pt.get_point()); // since all the surface edges are interconnected only store the end points
                }

                // Set the path of outter surface
                System.Drawing.Drawing2D.GraphicsPath surface_path = new System.Drawing.Drawing2D.GraphicsPath();
                surface_path.StartFigure();
                surface_path.AddPolygon(temp_sur_pts.ToArray());
                surface_path.CloseFigure();

                // set region
                surface_region = new Region(surface_path);

                // set the hatch brush
                Color hatch_color = Display.StaticClass.GetRandomColor(surf_count);
                System.Drawing.Drawing2D.HatchStyle hatch_style = Display.StaticClass.GetRandomHatchStyle(surf_count);
                Color trans_color = Color.FromArgb(0, 10, 10, 10);

                tri_brush = new System.Drawing.Drawing2D.HatchBrush(hatch_style, hatch_color, trans_color);

                //set the area
                surface_area = Math.Abs(this.signedpolygonarea());
            }
          
            public void set_inner_surfaces(SurfaceStore i_inner_surface)
            {
                inner_surfaces.Add(i_inner_surface);
   
                // Set the path of inner surface
                List<PointF> temp_sur_pts = new List<PointF>();
                foreach (edge2d ed in inner_surfaces[inner_surfaces.Count - 1].surface_edges)
                {
                    temp_sur_pts.Add(ed.end_pt.get_point());
                }

                System.Drawing.Drawing2D.GraphicsPath inner_surface = new System.Drawing.Drawing2D.GraphicsPath();
                inner_surface.StartFigure();
                inner_surface.AddPolygon(temp_sur_pts.ToArray());
                inner_surface.CloseFigure();

                // set region
                surface_region.Exclude(inner_surface); // exclude the inner surface region
            }

            public void set_encapsulating_surface(SurfaceStore outter_surface)
            {
                this._encapsulating_surface_id = outter_surface.surface_id;
            }

            public void paint_me(ref Graphics gr0) // this function is used to paint the points
            { 
                Graphics gr1 = gr0;

                Pen temp_edge_pen = new Pen(Color.DarkBlue, 2);
                surface_edges.ForEach(obj => obj.paint_me(ref gr1, ref temp_edge_pen)); // Paint the edges

                Pen temp_node_pen = new Pen(Color.BlueViolet, 2);
                surface_nodes.ForEach(obj => obj.paint_me(ref gr1, ref temp_node_pen)); // Paint the faces

                my_mesh.paint_me(ref gr1);
            }

            // Return the polygon's area in "square units."
            // The value will be negative if the polygon is
            // oriented clockwise.
            public double signedpolygonarea()
            {
                //The total calculated area is negative if the polygon is oriented clockwise

                // Extract point
                List<PointF> polygon_pt = new List<PointF>();
                foreach (edge2d ed in this.surface_edges)
                {
                    polygon_pt.Add(ed.end_pt.get_point());
                }

                // Return the result.
                return Display.StaticClass.SignedPolygonArea(polygon_pt.ToArray());
            }

            // Return True if the point is in the polygon (outside edges of the surface).
            // Note this will return if the point is inside the outside edges of the surface (use point in surface to find the points in surface)
            public bool pointinpolygon(double X, double Y)
            {
                // Extract point
                List<PointF> polygon_pt = new List<PointF>(); 
                foreach (edge2d ed in this.surface_edges)
                {
                    polygon_pt.Add(ed.end_pt.get_point());
                }

                PointF test_pt = new PointF((float)X, (float)Y);

                return Display.StaticClass.IsPointInPolygon(polygon_pt.ToArray(), test_pt);
            }

            // Return true if the point is in the surface
            public bool pointinsurface(double X, double Y)
            {
                if (pointinpolygon(X, Y) == true)
                {
                    foreach (SurfaceStore inner_surf in inner_surfaces)
                    {
                        if (inner_surf.pointinpolygon(X, Y) == true) // the point lies in inner surface so not the selected surface
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public class point2d // class to store the points
        {
            int _id;
            double _x;
            double _y;

            public int id
            {
                get { return this._id; }
            }

            public double x
            {
                get { return this._x; }
            }
            public double y
            {
                get { return this._y; }
            }
            public point2d(int i_id, double i_x, double i_y)
            {
                // constructor 1
                this._id = i_id;
                this._x = i_x;
                this._y = i_y;
            }

            public void paint_me(ref Graphics gr0, ref Pen node_pen) // this function is used to paint the points
            {
                gr0.FillEllipse(node_pen.Brush, new RectangleF(get_point_for_ellipse(), new SizeF(4, 4)));

                if (Display.StaticClass.isPaintLabel == true)
                {
                    string my_string = (this.id + 1).ToString() + "(" + this._x.ToString("F2") + ", " + this._y.ToString("F2") + ")";
                    SizeF str_size = gr0.MeasureString(my_string, new Font("Cambria", 6)); // Measure string size to position the dimension

                    gr0.DrawString(my_string, new Font("Cambria", 6),
                                                                       new Pen(Color.DarkBlue, 2).Brush,
                                                                       get_point_for_ellipse().X + 3 + Display.StaticClass.ToSingle(-str_size.Width * 0.5),
                                                                       Display.StaticClass.ToSingle(str_size.Height * 0.5) + get_point_for_ellipse().Y + 3);
                }
            }

            public PointF get_point_for_ellipse()
            {
                return (new PointF(Display.StaticClass.ToSingle(this._x) - 2,
               Display.StaticClass.ToSingle((this._y) - 2))); // return the point as PointF as edge of an ellipse
            }

            public PointF get_point()
            {
                return (new PointF(Display.StaticClass.ToSingle(this._x),
               Display.StaticClass.ToSingle(this._y))); // return the point as PointF as edge of an ellipse
            }

            public bool Equals(point2d other)
            {
                return (this._x == other.x && this._y == other.y); // Equal function is used to check the uniqueness of the points added
            }

            public bool is_close_enough(point2d other)
            {
                double eps = 0.001;
                return (Math.Abs(this._x - other.x) < eps && Math.Abs(this._y - other.y) < eps);
            }
        }

        public class points_equality_comparer : IEqualityComparer<point2d>
        {
            public bool Equals(point2d a, point2d b)
            {
                return (a.Equals(b));
            }

            public int GetHashCode(point2d other)
            {
                return (other.x.GetHashCode() * 17 + other.y.GetHashCode() * 19);    // 17,19 are just random prime numbers
            }
        }

        public class edge2d
        {
            int _edge_id;
            point2d _start_pt;
            point2d _end_pt;
            point2d _mid_pt; // not stored in point list

            public int edge_id
            {
                get { return this._edge_id; }
            }

            public point2d start_pt
            {
                get { return this._start_pt; }
            }

            public point2d end_pt
            {
                get { return this._end_pt; }
            }

            public point2d mid_pt
            {
                get { return this._mid_pt; }
            }

            public double edge_length
            {
                get { return Math.Sqrt(Math.Pow(_start_pt.x - _end_pt.x, 2) + Math.Pow(_start_pt.y - _end_pt.y, 2)); }
            }

            public edge2d(int i_edge_id, point2d i_start_pt, point2d i_end_pt)
            {
                // constructor 1
                this._edge_id = i_edge_id;
                this._start_pt = i_start_pt;
                this._end_pt = i_end_pt;
                this._mid_pt = new point2d(-1, (i_start_pt.x + i_end_pt.x) * 0.5, (i_start_pt.y + i_end_pt.y) * 0.5);
            }

            public void paint_me(ref Graphics gr0, ref Pen edge_pen) // this function is used to paint the points
            {
                gr0.DrawLine(edge_pen, start_pt.get_point(), end_pt.get_point());
            }

            public bool Equals(edge2d other)
            {
                return (other.start_pt.Equals(this._start_pt) && other.end_pt.Equals(this._end_pt));
            }

            public bool Equals_without_orientation(edge2d other)
            {
                return (other.start_pt.Equals(this._start_pt) && other.end_pt.Equals(this._end_pt) || other.start_pt.Equals(this._end_pt) && other.end_pt.Equals(this._start_pt));
            }

            public bool vertex_exists(point2d other)
            {

                if (start_pt.Equals(other) == true || end_pt.Equals(other) == true)
                {
                    return true;
                }
                return false;
            }

        }

        public class triangle2d
        { 
            int _face_id;
            public point2d[] vertices { get; } = new point2d[3];

            point2d _mid_pt;
            double shrink_factor = 0.6f;

            private int  parentIndex;

            public int  ParentIndex
            {
                get { return  parentIndex; }
                set {  parentIndex = value; }
            }



            public int face_id
            {
                get { return this._face_id; }
            }

            public point2d face_mid_pt
            {
                get
                {
                    return this._mid_pt;
                }
            }

            public PointF get_p1
            {
                get
                {
                    return new PointF(Display.StaticClass.ToSingle(_mid_pt.get_point().X * (1 - shrink_factor) + (vertices[0].get_point().X * shrink_factor)),
                                       Display.StaticClass.ToSingle(_mid_pt.get_point().Y * (1 - shrink_factor) + (vertices[0].get_point().Y * shrink_factor)));
                }
            }

            public PointF get_p2
            {
                get
                {
                    return new PointF(Display.StaticClass.ToSingle(_mid_pt.get_point().X * (1 - shrink_factor) + (vertices[1].get_point().X * shrink_factor)),
                                      Display.StaticClass.ToSingle(_mid_pt.get_point().Y * (1 - shrink_factor) + (vertices[1].get_point().Y * shrink_factor)));
                }
            }

            public PointF get_p3
            {
                get
                {
                    return new PointF(Display.StaticClass.ToSingle(_mid_pt.get_point().X * (1 - shrink_factor) + (vertices[2].get_point().X * shrink_factor)),
                                      Display.StaticClass.ToSingle(_mid_pt.get_point().Y * (1 - shrink_factor) + (vertices[2].get_point().Y * shrink_factor)));
                }
            }

            public double Area
            {
                get
                {
                    return Utils.CalculateArea(get_p1.X, get_p1.Y, get_p2.X, get_p2.Y, get_p3.X, get_p3.Y); 
                }
            }

            public double InnerRadius
            {
                get
                {
                    return Utils.CalculateInnerRadius(get_p1.X, get_p1.Y, get_p2.X, get_p2.Y, get_p3.X, get_p3.Y);
                }
            }
           
            public double OuterRadius
            {
                get
                {
                    return Utils.CalculateOuterRadius(get_p1.X, get_p1.Y, get_p2.X, get_p2.Y, get_p3.X, get_p3.Y);
                }
            }

            public double AspectRatio
            {
                get
                {
                    return Utils.AspectRatio(InnerRadius,OuterRadius);
                }
            }

            public (double, double, double) Angles
            {
                get
                {
                    return Utils.GetTriangleAngles(get_p1, get_p2, get_p3);
                }
            }

            public triangle2d(int i_face_id, point2d i_p1, point2d i_p2, point2d i_p3)
            {
                this._face_id = i_face_id;
                if (!IsCounterClockwise(i_p1, i_p2, i_p3))
                {
                    this.vertices[0] = i_p1;
                    this.vertices[1] = i_p3;
                    this.vertices[2] = i_p2;
                }
                else
                {
                    this.vertices[0] = i_p1;
                    this.vertices[1] = i_p2;
                    this.vertices[2] = i_p3;
                }

                this._mid_pt = new point2d(-1, (i_p1.x + i_p2.x + i_p3.x) / 3.0f, (i_p1.y + i_p2.y + i_p3.y) / 3.0f);
            }

            private bool IsCounterClockwise(point2d point1, point2d point2, point2d point3)
            {
                double result = (point2.x - point1.x) * (point3.y - point1.y) -
                    (point3.x - point1.x) * (point2.y - point1.y);
                return result > 0;
            }

            public bool edge_exists(edge2d other)
            {
                edge2d edge_1 = new edge2d(-1, this.vertices[0], this.vertices[1]);
                edge2d edge_2 = new edge2d(-1, this.vertices[1], this.vertices[2]);
                edge2d edge_3 = new edge2d(-1, this.vertices[2], this.vertices[0]);

                if (edge_1.Equals_without_orientation(other) == true || edge_2.Equals_without_orientation(other) == true || edge_3.Equals_without_orientation(other) == true)
                {
                    return true;
                }
                return false;
            }

            public void paint_me(ref Graphics gr0, ref Pen face_pen, double triangleArea, double q1Area) // this function is used to paint the points
            {               
                if (Display.StaticClass.isPaintMesh == true)
                {
                    if (!Utils.RefineChecked) q1Area = 0;
                    
                    if (triangleArea <= q1Area)
                    {
                        face_pen.Color = Color.White;
                    }
                    else
                    {
                        if (Utils.IsOuterSurface) face_pen.Color = Color.Black;
                        else face_pen.Color = Color.White;
                    }

                    PointF[] curve_pts = { get_p1, get_p2, get_p3 };
                    gr0.FillPolygon(face_pen.Brush, curve_pts); // Fill the polygon

                    if (Display.StaticClass.isPaintLabel == true)
                    {
                        string my_string = this._face_id.ToString();
                        SizeF str_size = gr0.MeasureString(my_string, new Font("Cambria", 6)); // Measure string size to position the dimension

                        gr0.DrawString(my_string, new Font("Cambria", 6), new Pen(Color.DeepPink, 2).Brush, this._mid_pt.get_point());
                    }
                }                 
            }             
        }
    }
}
