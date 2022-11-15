using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

// credits to Sebastian Lague for the map mesh generator
// https://forum.unity.com/threads/procedural-cave-generation.296986/
// https://github.com/SebLague/Procedural-Cave-Generation
// https://github.com/SebLague
// Modified to get only the closed domains

namespace DelaunayGenericTriangulation
{
    public class GenerateMap
    {
        #region fields

        private bool mapGenerated = false; // map generated [yes or no]
        public int cWidth;
        public int cHeight;
        public string seed;
        public int spacing = 18; // square size
        public int customSpacing = 5;
        public int randomFillPercent = 48; // 0 to 100 more like 45 to 55
        public int smoothingIteration = 10; // 1 and above 
        public int[,] map;
        public SquareGrid theSquareGrid;

        #endregion

        public GenerateMap()
        {
            mapGenerated = false;
        }

        public void DetectSurfaces(ref List<PSLGDataStructure.SurfaceStore> setSurfaces)
        {
            // Find the closed loop which are not self intersecting
            // transfer to temporary nodes and temporary edges
            // List<map_node> temp_border_nodes = new List<map_node>();
            List<map_edge> temp_border_edges = new List<map_edge>();

            //temp_border_nodes = theSquareGrid.border_nodes;
            temp_border_edges.AddRange(theSquareGrid.border_edges);

            //bool is_closed = false;
            //int i, j;
            // temporary surfaces
            List<PSLGDataStructure.SurfaceStore> temp_surfaces = new List<PSLGDataStructure.SurfaceStore>();

            while (temp_border_edges.Count > 0) // loop until all the border edges are visited
            {
                List<map_edge> temp_border_edges_1 = new List<map_edge>(); // border edge sublist 1
                temp_border_edges_1.AddRange(temp_border_edges); // add the main list to sublist 1
                temp_border_edges_1.RemoveAt(0); // remove the first edge


                List<map_edge> temp_border_edges_2 = new List<map_edge>(); // border edge sublist 2
                temp_border_edges_2.Add(temp_border_edges[0]); // add only the first edge frpm mail list
                int current_edge_end_node = temp_border_edges_2[0].index_1; // only one object is at the list

                while (temp_border_edges_1.Count > 0)
                {
                    map_edge connected_edge = temp_border_edges_1.Find(obj => obj.index_0 == current_edge_end_node || obj.index_1 == current_edge_end_node); // find the connected edge
                    if (connected_edge != null)
                    {
                        if (connected_edge.index_1 == current_edge_end_node) // if the edge end
                        {
                            // then the orientation is towards this point so reverse orientation
                            connected_edge.change_orientation();
                        }

                        current_edge_end_node = connected_edge.index_1; // index 0 is connected
                        temp_border_edges_2.Add(connected_edge); // add to the sub list
                        temp_border_edges_1.RemoveAt(temp_border_edges_1.FindIndex(obj => obj.Equals(connected_edge) == true)); //remove from the sub list 1
                    }
                    else
                    {
                        break; // exit because there is no edge connected to the current edge end node
                    }
                }
                 
                if (temp_border_edges_2[0].index_0 == current_edge_end_node) // we started with index_0 so check whether index_1 is connected to the last in the list
                {
                    // PSLGDataStructure Edges and Points
                    List<PSLGDataStructure.edge2d> surf_edges = new List<PSLGDataStructure.edge2d>();
                    List<PSLGDataStructure.point2d> surf_points = new List<PSLGDataStructure.point2d>();

                    // closed boundary is confirmed
                    List<map_node> temp_border_nodes = new List<map_node>();
                    foreach (map_edge edg in temp_border_edges_2)
                    {
                        map_node the_node_1 = theSquareGrid.all_nodes[edg.index_0];
                        map_node the_node_2 = theSquareGrid.all_nodes[edg.index_1];

                        // Add first node
                        if (temp_border_nodes.Exists(obj => obj.Equals(the_node_1) == true) == false)
                        {
                            temp_border_nodes.Add(the_node_1);
                            surf_points.Add(new PSLGDataStructure.point2d(surf_points.Count, the_node_1.x, the_node_1.y));
                        }

                        // Add second node
                        if (temp_border_nodes.Exists(obj => obj.Equals(the_node_2) == true) == false)
                        {
                            temp_border_nodes.Add(the_node_2);
                            surf_points.Add(new PSLGDataStructure.point2d(surf_points.Count, the_node_2.x, the_node_2.y));
                        }

                        // Add the edges
                        PSLGDataStructure.point2d pt1 = surf_points.Find(obj => obj.Equals(new PSLGDataStructure.point2d(-1, the_node_1.x, the_node_1.y)) == true);
                        PSLGDataStructure.point2d pt2 = surf_points.Find(obj => obj.Equals(new PSLGDataStructure.point2d(-1, the_node_2.x, the_node_2.y)) == true);

                        surf_edges.Add(new PSLGDataStructure.edge2d(surf_edges.Count, pt1, pt2));
                    }

                    // check the surface orientation
                    PSLGDataStructure.SurfaceStore t_surf = new PSLGDataStructure.SurfaceStore(temp_surfaces.Count,surf_points, surf_edges, temp_surfaces.Count);
                    temp_surfaces.Add(t_surf);
                }

                foreach (map_edge edg in temp_border_edges_2)
                {
                    //remove from the main edge
                    temp_border_edges.RemoveAt(temp_border_edges.FindIndex(obj => obj.Equals(edg) == true));
                }
            }

            // sort the surfaces with surface area
            temp_surfaces = temp_surfaces.OrderBy(obj => obj.surface_area).ToList();

            // Set the polygon inside polygon
            List<int> skip_index = new List<int>();
            for (int i = 1; i < temp_surfaces.Count; i++)
            {
                for (int j = i - 1; j >= 0; j--) // cycle through all the other surface except this one
                {
                    if (skip_index.Contains(j) == true)
                    {
                        continue; // skip the nested surfaces
                    }

                    bool is_contain = true; // variable to store the point is inside true or false
                    foreach (PSLGDataStructure.point2d pt in temp_surfaces[j].surface_nodes) // cycle thro all the point of j_th polygon
                    {
                        if (temp_surfaces[i].pointinpolygon(pt.x, pt.y) == false)// check whether outter polygon contains the inner polygon
                        {
                            is_contain = false;
                            break;
                        }
                    }

                    if (is_contain == true)
                    {
                        skip_index.Add(j); // skip index is used to avoid adding nested surfaces
                        temp_surfaces[i].set_inner_surfaces(temp_surfaces[j]);
                        temp_surfaces[j].set_encapsulating_surface(temp_surfaces[i]); // also set the encapsulating surface id
                    }
                }
            }

            // Surface detection complete add to the main list
            setSurfaces = new List<PSLGDataStructure.SurfaceStore>();
            setSurfaces.AddRange(temp_surfaces);
        }

        public class SquareGrid
        {
            public map_square[,] the_squares;
            public List<map_node> all_nodes = new List<map_node>();
            public List<map_triangle> all_triangle = new List<map_triangle>();

            // main output
            public List<map_node> border_nodes = new List<map_node>();
            public List<map_edge> border_edges = new List<map_edge>();

            public SquareGrid(int[,] map, int square_size)
            {
                int nodecountX = map.GetLength(0);
                int nodecountY = map.GetLength(1);

                float mapwidth = nodecountX * square_size;
                float mapheight = nodecountY * square_size;
                int i, j;


                control_node[,] the_controlnodes = new control_node[nodecountX, nodecountY];

                // set the control nodes
                for (int x = 0; x < nodecountX; x++)
                {
                    for (int y = 0; y < nodecountY; y++)
                    {
                        double pos_x = -(mapwidth / 2) + (x * square_size) + (square_size / 2);
                        double pos_y = -(mapheight / 2) + (y * square_size) + (square_size / 2); // -mapheight/2 is orgin

                        the_controlnodes[x, y] = new control_node(pos_x, pos_y, map[x, y] == 1, square_size); // active only if solid
                    }
                }

                // Re-initialize the list
                all_nodes = new List<map_node>();
                all_triangle = new List<map_triangle>();
                border_nodes = new List<map_node>();
                border_edges = new List<map_edge>();

                the_squares = new map_square[nodecountX - 1, nodecountY - 1];

                // set the squares
                for (int x = 0; x < nodecountX - 1; x++)
                {
                    for (int y = 0; y < nodecountY - 1; y++)
                    {
                        the_squares[x, y] = new map_square(the_controlnodes[x, y + 1], the_controlnodes[x + 1, y + 1], the_controlnodes[x + 1, y], the_controlnodes[x, y]);
                        triangulate_square(the_squares[x, y]);
                    }
                }

                 
                List<map_edge> temp_border_edges = new List<map_edge>();

                // Set the border edges
                for (i = 0; i < all_triangle.Count; i++)
                {
                    bool edge1_found = false;
                    bool edge2_found = false;
                    bool edge3_found = false;

                    for (j = 0; j < all_triangle.Count; j++)
                    {
                        if (i == j)
                            continue;

                        // Edge 1
                        if ((all_triangle[i].index_0 == all_triangle[j].index_0 &&
                            all_triangle[i].index_1 == all_triangle[j].index_1) ||
                            (all_triangle[i].index_0 == all_triangle[j].index_1 &&
                            all_triangle[i].index_1 == all_triangle[j].index_2) ||
                             (all_triangle[i].index_0 == all_triangle[j].index_2 &&
                            all_triangle[i].index_1 == all_triangle[j].index_0) ||
                            (all_triangle[i].index_0 == all_triangle[j].index_1 &&
                            all_triangle[i].index_1 == all_triangle[j].index_0) ||
                            (all_triangle[i].index_0 == all_triangle[j].index_2 &&
                            all_triangle[i].index_1 == all_triangle[j].index_1) ||
                             (all_triangle[i].index_0 == all_triangle[j].index_0 &&
                            all_triangle[i].index_1 == all_triangle[j].index_2))
                        {
                            edge1_found = true;
                        }

                        // Edge 2
                        if ((all_triangle[i].index_1 == all_triangle[j].index_0 &&
                            all_triangle[i].index_2 == all_triangle[j].index_1) ||
                            (all_triangle[i].index_1 == all_triangle[j].index_1 &&
                            all_triangle[i].index_2 == all_triangle[j].index_2) ||
                             (all_triangle[i].index_1 == all_triangle[j].index_2 &&
                            all_triangle[i].index_2 == all_triangle[j].index_0) ||
                            (all_triangle[i].index_1 == all_triangle[j].index_1 &&
                            all_triangle[i].index_2 == all_triangle[j].index_0) ||
                            (all_triangle[i].index_1 == all_triangle[j].index_2 &&
                            all_triangle[i].index_2 == all_triangle[j].index_1) ||
                             (all_triangle[i].index_1 == all_triangle[j].index_0 &&
                            all_triangle[i].index_2 == all_triangle[j].index_2))
                        {
                            edge2_found = true;
                        }

                        // Edge 3
                        if ((all_triangle[i].index_2 == all_triangle[j].index_0 &&
                            all_triangle[i].index_0 == all_triangle[j].index_1) ||
                            (all_triangle[i].index_2 == all_triangle[j].index_1 &&
                            all_triangle[i].index_0 == all_triangle[j].index_2) ||
                             (all_triangle[i].index_2 == all_triangle[j].index_2 &&
                            all_triangle[i].index_0 == all_triangle[j].index_0) ||
                            (all_triangle[i].index_2 == all_triangle[j].index_1 &&
                            all_triangle[i].index_0 == all_triangle[j].index_0) ||
                            (all_triangle[i].index_2 == all_triangle[j].index_2 &&
                            all_triangle[i].index_0 == all_triangle[j].index_1) ||
                             (all_triangle[i].index_2 == all_triangle[j].index_0 &&
                            all_triangle[i].index_0 == all_triangle[j].index_2))
                        {
                            edge3_found = true;
                        }

                        if (edge1_found == true && edge2_found == true && edge3_found == true)
                            break; // exit the loop if all the edges are connected to some other triangle
                    }

                    if (edge1_found == false)
                    {
                        temp_border_edges.Add(new map_edge(all_triangle[i].index_0, all_triangle[i].index_1));
                    }

                    if (edge2_found == false)
                    {
                        temp_border_edges.Add(new map_edge(all_triangle[i].index_1, all_triangle[i].index_2));
                    }

                    if (edge3_found == false)
                    {
                        temp_border_edges.Add(new map_edge(all_triangle[i].index_2, all_triangle[i].index_0));
                    }
                } 

                List<map_edge> temp_border_edges_0 = new List<map_edge>(); // border edge sub list 1
                temp_border_edges_0.Add(temp_border_edges[0]); // add the first edges
                while (temp_border_edges.Count > 0)
                {
                    // step 1 is to find a sublist of edges with same slope
                    List<map_edge> temp_border_edges_1 = new List<map_edge>(); // border edge sub list 2
                    temp_border_edges_1.AddRange(temp_border_edges.FindAll(obj => compare_slope(temp_border_edges_0[0], obj) == true));
                    temp_border_edges_1.Remove(temp_border_edges_0[0]); //Remove the only duplicate

                    // step 2 is to find the connected edges
                    j = 0;
                    while (temp_border_edges_1.Count > 0) // Exit if there is no edges with the same slope
                    {
                        List<map_edge> temp_border_edges_2 = new List<map_edge>();
                        for (i = j; i < temp_border_edges_0.Count; i++)
                        {
                            // find the connectivity to first list
                            temp_border_edges_2.AddRange(temp_border_edges_1.FindAll(obj => obj.is_connected_to(temp_border_edges_0[i]) == true));
                        }

                        // j is the variable to avoid checking the same edges again and again
                        j = temp_border_edges_0.Count;
                        if (temp_border_edges_2.Count == 0) // Exit if no connected edge is found
                            break;

                        // add the new found edges to the list
                        temp_border_edges_0.AddRange(temp_border_edges_2);

                        // Remove from the first list  (where the slope is identical)
                        foreach (map_edge ed in temp_border_edges_2)
                        {
                            temp_border_edges_1.Remove(ed);
                        }
                    }

                    // create a temporary border node list
                    List<map_node> temp_border_nodes = new List<map_node>();
                    foreach (map_edge ed in temp_border_edges_0)
                    {
                        temp_border_edges.Remove(ed);// Remove from the master edge list
                                                     // Also add the nodes to a new list
                        if (temp_border_nodes.Exists(obj => obj.Equals(all_nodes[ed.index_0])) == false)
                        {
                            temp_border_nodes.Add(all_nodes[ed.index_0]);
                        }
                        if (temp_border_nodes.Exists(obj => obj.Equals(all_nodes[ed.index_1])) == false)
                        {
                            temp_border_nodes.Add(all_nodes[ed.index_1]);
                        }
                    }

                    // sort the border nodes
                    temp_border_nodes = temp_border_nodes.OrderBy(obj => obj.x).ThenBy(obj => obj.y).ToList(); // first by x and then by y

                    // now the first and last nodes are the final result (intermediate nodes are not required)
                    if (border_nodes.Exists(obj => obj.Equals(temp_border_nodes[0])) == false)
                    {
                        border_nodes.Add(temp_border_nodes[0]);
                    }
                    if (border_nodes.Exists(obj => obj.Equals(temp_border_nodes[temp_border_nodes.Count - 1])) == false)
                    {
                        border_nodes.Add(temp_border_nodes[temp_border_nodes.Count - 1]);
                    }

                    border_edges.Add(new map_edge(temp_border_nodes[0].vertex_index, temp_border_nodes[temp_border_nodes.Count - 1].vertex_index));
                    if (temp_border_edges.Count > 0)
                    {
                        // renew the border edge sub list 1
                        temp_border_edges_0 = new List<map_edge>(); // border edge sub list 1
                        temp_border_edges_0.Add(temp_border_edges[0]); // add the first edges
                    }
                }
            }

            public Tuple<double, double, double> compute_line_eqn(map_node n1, map_node n2)
            {
                // sore the point using x and then by y
                map_node temp;
                if (n1.x > n2.x)
                {
                    // swap bcoz n1.x greater
                    temp = n2;
                    n2 = n1;
                    n1 = temp;
                }
                else if (n1.x == n2.x)
                {
                    if (n1.y > n2.y)
                    {
                        // swap bcoz n1.y greater
                        temp = n2;
                        n2 = n1;
                        n1 = temp;
                    }
                }

                double a = n2.y - n1.y; // a = y2 - y1
                double b = n1.x - n2.x; // b = x1 - x2
                double c = a * n1.x + b * n1.y; // c = ax1 + by1

                return Tuple.Create(a, b, c);
            }

            public bool compare_slope1(map_edge e1, map_edge e2)
            {
                bool is_equal = false;
                double eps = 0.00001; // precision for comparison

                Tuple<double, double, double> e1_eqn = compute_line_eqn(all_nodes[e1.index_0], all_nodes[e1.index_1]); // Line equation of edge 1
                Tuple<double, double, double> e2_eqn = compute_line_eqn(all_nodes[e2.index_0], all_nodes[e2.index_1]); // Line equation of edge 2

                if (Math.Abs(e1_eqn.Item1 - e2_eqn.Item1) < eps &&
                    Math.Abs(e1_eqn.Item2 - e2_eqn.Item2) < eps &&
                    Math.Abs(e1_eqn.Item3 - e2_eqn.Item3) < eps)
                {
                    is_equal = true;
                }

                return is_equal;
            }

            public bool compare_slope(map_edge e1, map_edge e2)
            {
                bool is_equal = false;

                double slope_e1 = (all_nodes[e1.index_1].y - all_nodes[e1.index_0].y) / (all_nodes[e1.index_1].x - all_nodes[e1.index_0].x); // slope of edge 1
                double slope_e2 = (all_nodes[e2.index_1].y - all_nodes[e2.index_0].y) / (all_nodes[e2.index_1].x - all_nodes[e2.index_0].x); // slope of edge 2

                if ((slope_e1) == (slope_e2))
                {
                    is_equal = true;
                }

                return is_equal;
            }

            public void triangulate_square(map_square i_squares)
            {
                switch (i_squares.configuration)
                {
                    case 0:
                        break;

                    // 1 points:
                    case 1:
                        MeshFromPoints(i_squares.centrebottom, i_squares.bottomleft, i_squares.centreleft);
                        break;
                    case 2:
                        MeshFromPoints(i_squares.centreright, i_squares.bottomright, i_squares.centrebottom);
                        break;
                    case 4:
                        MeshFromPoints(i_squares.centretop, i_squares.topright, i_squares.centreright);
                        break;
                    case 8:
                        MeshFromPoints(i_squares.topleft, i_squares.centretop, i_squares.centreleft);
                        break;

                    // 2 points:
                    case 3:
                        MeshFromPoints(i_squares.centreright, i_squares.bottomright, i_squares.bottomleft, i_squares.centreleft);
                        break;
                    case 6:
                        MeshFromPoints(i_squares.centretop, i_squares.topright, i_squares.bottomright, i_squares.centrebottom);
                        break;
                    case 9:
                        MeshFromPoints(i_squares.topleft, i_squares.centretop, i_squares.centrebottom, i_squares.bottomleft);
                        break;
                    case 12:
                        MeshFromPoints(i_squares.topleft, i_squares.topright, i_squares.centreright, i_squares.centreleft);
                        break;
                    case 5:
                        MeshFromPoints(i_squares.centretop, i_squares.topright, i_squares.centreright, i_squares.centrebottom, i_squares.bottomleft, i_squares.centreleft);
                        break;
                    case 10:
                        MeshFromPoints(i_squares.topleft, i_squares.centretop, i_squares.centreright, i_squares.bottomright, i_squares.centrebottom, i_squares.centreleft);
                        break;

                    // 3 point:
                    case 7:
                        MeshFromPoints(i_squares.centretop, i_squares.topright, i_squares.bottomright, i_squares.bottomleft, i_squares.centreleft);
                        break;
                    case 11:
                        MeshFromPoints(i_squares.topleft, i_squares.centretop, i_squares.centreright, i_squares.bottomright, i_squares.bottomleft);
                        break;
                    case 13:
                        MeshFromPoints(i_squares.topleft, i_squares.topright, i_squares.centreright, i_squares.centrebottom, i_squares.bottomleft);
                        break;
                    case 14:
                        MeshFromPoints(i_squares.topleft, i_squares.topright, i_squares.bottomright, i_squares.centrebottom, i_squares.centreleft);
                        break;

                    // 4 point:
                    case 15:
                        MeshFromPoints(i_squares.topleft, i_squares.topright, i_squares.bottomright, i_squares.bottomleft);
                        break;
                }
            }

            private void MeshFromPoints(params map_node[] points)
            {
                AssignVertices(points);

                if (points.Length >= 3)
                    all_triangle.Add(new map_triangle(points[0].vertex_index, points[1].vertex_index, points[2].vertex_index));
                if (points.Length >= 4)
                    all_triangle.Add(new map_triangle(points[0].vertex_index, points[2].vertex_index, points[3].vertex_index));
                if (points.Length >= 5)
                    all_triangle.Add(new map_triangle(points[0].vertex_index, points[3].vertex_index, points[4].vertex_index));
                if (points.Length >= 6)
                    all_triangle.Add(new map_triangle(points[0].vertex_index, points[4].vertex_index, points[5].vertex_index));
            }

            void AssignVertices(map_node[] points)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    if (points[i].vertex_index == -1)
                    {
                        points[i].vertex_index = all_nodes.Count;
                        all_nodes.Add(points[i]);
                    }
                }
            }
        }

        public class map_square
        {
            public control_node topleft, topright, bottomright, bottomleft;
            public map_node centretop, centrebottom, centreleft, centreright;
            public int configuration;

            public map_square(control_node i_topleft, control_node i_topright, control_node i_bottomright, control_node i_bottomleft)
            {
                topleft = i_topleft;
                topright = i_topright;
                bottomleft = i_bottomleft;
                bottomright = i_bottomright;

                centretop = topleft.right;
                centreright = bottomright.above;
                centrebottom = bottomleft.right;
                centreleft = bottomleft.above;

                if (topleft.active)
                    configuration += 8;
                if (topright.active)
                    configuration += 4;
                if (bottomright.active)
                    configuration += 2;
                if (bottomleft.active)
                    configuration += 1;
            }
        }

        public class map_triangle
        {
            public int index_0;
            public int index_1;
            public int index_2;

            public map_triangle(int i_i0, int i_i1, int i_i2)
            {
                index_0 = i_i0;
                index_1 = i_i1;
                index_2 = i_i2;
            }
        }

        public class map_edge
        {
            public int index_0;
            public int index_1;

            public map_edge(int i_i0, int i_i1)
            {
                index_0 = i_i0;
                index_1 = i_i1;
            }

            public bool is_connected_to(map_edge other_edge)
            {
                bool is_connected = false;
                if (index_0 == other_edge.index_0 || index_0 == other_edge.index_1 || index_1 == other_edge.index_0 || index_1 == other_edge.index_1)
                    is_connected = true;

                return is_connected;
            }

            public bool Equals(map_edge other_edge)
            {
                bool is_equal = false;
                if ((index_0 == other_edge.index_0 && index_1 == other_edge.index_1) || (index_0 == other_edge.index_1 && index_1 == other_edge.index_0))
                    is_equal = true;

                return is_equal;
            }

            public void change_orientation()
            {
                // reverse the orientation
                int temp = index_0;
                index_0 = index_1;
                index_1 = temp;
            }
        }

        public class map_node
        {
            public double x;
            public double y;
            public int vertex_index = -1;

            public PointF get_pt
            {
                get { return new PointF((float)x, (float)y); }
            }

            private PointF get_rectpt(double x_size, double y_size)
            {
                return new PointF((float)(x - x_size), (float)(y - y_size));
            }

            public RectangleF get_rectangle(SizeF rect_size)
            {
                return new RectangleF(get_rectpt(rect_size.Width * 0.5, rect_size.Height * 0.5), rect_size);
            }


            public map_node(double i_x, double i_y)
            {
                // constructor
                x = i_x;
                y = i_y;
            }

            public bool Equals(map_node other_nd)
            {
                bool return_var = false;
                if (this.vertex_index == other_nd.vertex_index)
                {
                    return_var = true;
                }
                return return_var;
            }

        }

        public class control_node : map_node
        {
            public bool active;
            public map_node above, right;

            public control_node(double x, double y, bool i_active, int square_size) : base(x, y)
            {
                active = i_active;
                above = new map_node(x, (y + (square_size / 2.0f))); // above node
                right = new map_node((x + (square_size / 2.0f)), y); // right node
            }
        }

        public GenerateMap(float i_width, float i_height)
        {
            cWidth = (int)(Math.Floor((double)i_width) / spacing);
            cHeight = (int)(Math.Floor((double)i_height) / spacing);

            map = new int[cWidth, cHeight]; // map grid

            // fill map
            random_fill_map();

            // smooth map 
            for (int i = 0; i < smoothingIteration; i++)
            {
                // smooth map x times
                smooth_map();
            }

            theSquareGrid = new SquareGrid(map, spacing);
            mapGenerated = true;             
        }
      
        public GenerateMap(float i_width, float i_height, string filePath)
        {
            cWidth = (int)(Math.Floor((double)i_width) / customSpacing);
            cHeight = (int)(Math.Floor((double)i_height) / customSpacing);          

            map = new int[cWidth, cHeight]; // map grid

            string fileName = "MapFileOutPut.csv";
            if (!string.IsNullOrEmpty(filePath)) fileName = filePath;
            string[] lines = File.ReadAllLines(fileName);

            int[,] readMap = new int[cWidth, cHeight];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(',');
                for (int j = 0; j < line.Length - 1; j++)
                {
                    string strValue = line[j];
                    readMap[i, j] = Convert.ToInt32(strValue);
                    map[i,j] = Convert.ToInt32(strValue);
                }
            }
             
            // smooth map 
            for (int i = 0; i < smoothingIteration; i++)
            {
                // smooth map x times
                smooth_map();
            }

            theSquareGrid = new SquareGrid(map, customSpacing);
            mapGenerated = true;
        }

        public void random_fill_map()
        {
            seed = DateTime.Now.ToString();
            Random pseudoRandom = new Random(seed.GetHashCode());

            using (StreamWriter writer = new StreamWriter("mapFile.csv"))
            {
                for (int x = 0; x < cWidth; x++)
                {
                    for (int y = 0; y < cHeight; y++)
                    {
                        if (x == 0 || x == cWidth - 1 || y == 0 || y == cHeight - 1)
                        {
                            // set the edges as solid
                            map[x, y] = 1; //1 is solid 


                            // save this values
                            //writer.Write($"{x}_{y}" + ",");
                            writer.Write(1 + ",");
                        }
                        else
                        {
                            // set the other block as solid or void through random
                            map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;


                            // save this values
                            writer.Write(map[x,y] + ",");
                        }                  
                           
                    }

                    writer.WriteLine();
                }
            }
        }

        public void smooth_map()
        {
            for (int x = 0; x < cWidth; x++)
            {
                for (int y = 0; y < cHeight; y++)
                {
                    int neighbourwalltiles = get_surrounded_wallcount(x, y); // value of the surrounded wall

                    if (neighbourwalltiles > 4) // if most of the neighbour walls are solid
                    {
                        // set the edges as solid
                        map[x, y] = 1; //1 is solid 
                    }
                    else if (neighbourwalltiles < 4) // if most of the neighbour walls are void
                    {
                        // set the edges as void
                        map[x, y] = 0; //0 is void
                    }
                }
            }
        }

        public int get_surrounded_wallcount(int gridx, int gridy)
        {
            int wallcount = 0;
            for (int neighbourX = gridx - 1; neighbourX <= gridx + 1; neighbourX++)
            {
                for (int neighboutY = gridy - 1; neighboutY <= gridy + 1; neighboutY++)
                {
                    if (neighbourX >= 0 && neighbourX < cWidth && neighboutY >= 0 && neighboutY < cHeight)
                    {
                        if (neighbourX != gridx || neighboutY != gridy) // ignore the grid itself bcoz we only need the boundary values
                        {
                            wallcount += map[neighbourX, neighboutY];
                        }
                    }
                    else
                    {
                        wallcount++; // edge is always solid so add 1
                    }
                }
            }
            return wallcount;
        }

        public void paint_me(ref Graphics gr0)
        {
            if (mapGenerated == true)
            { 
                // Paint Border Edges
                foreach (map_edge edge in theSquareGrid.border_edges)
                {
                    PointF pt1 = theSquareGrid.all_nodes[edge.index_0].get_pt;
                    PointF pt2 = theSquareGrid.all_nodes[edge.index_1].get_pt;
                    PointF mid_pt = return_mid_pt(pt1, pt2);

                    System.Drawing.Drawing2D.GraphicsPath end_path = new System.Drawing.Drawing2D.GraphicsPath();
                    end_path.AddLine(0, 0, -2, -2);
                    end_path.AddLine(0, 0, 2, -2);

                    System.Drawing.Drawing2D.CustomLineCap end_cap = new System.Drawing.Drawing2D.CustomLineCap(null, end_path);

                    Pen edge_pen_capped = new Pen(Color.DarkGreen, 2);
                    edge_pen_capped.CustomEndCap = end_cap;
                    gr0.DrawLine(edge_pen_capped, pt1, mid_pt);

                    Pen edge_pen = new Pen(Color.DarkGreen, 2);
                    gr0.DrawLine(edge_pen, mid_pt, pt2);
                }

                // Paint Border Nodes
                foreach (map_node nd in theSquareGrid.border_nodes)
                {
                    SolidBrush nd_brush = new SolidBrush(Color.DarkOrange);

                    gr0.FillEllipse(nd_brush, nd.get_rectangle(new SizeF(4, 4)));
                } 
            }
        }

        private PointF return_mid_pt(PointF pt1, PointF pt2)
        {
            double mid_x = (pt1.X + pt2.X) * 0.5;
            double mid_y = (pt1.Y + pt2.Y) * 0.5;

            return new PointF((float)mid_x, (float)mid_y);
        }
    }
}
