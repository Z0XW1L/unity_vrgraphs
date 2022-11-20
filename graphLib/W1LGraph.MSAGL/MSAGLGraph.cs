/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Layout.Incremental;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Layout.MDS;
using Microsoft.Msagl.Miscellaneous;
using Microsoft.Msagl.Prototype.Ranking;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
using W1LGraph.Common;
using Point = Microsoft.Msagl.Core.Geometry.Point;

namespace W1LGraph.MSAGL
{
    /// <summary>
    /// Represents a MSAGL graph layout.
    /// </summary>
    public class MSAGLGraph
    {
        #region Public attributes
        /// <summary>
        /// Determines the layouting strategy for this graph.
        /// </summary>
        public enum MSAGLLayout
        {
            SugiyamaLayout,
            FastIncremental,
            Ranking,
            Mds
        }
        private readonly MSAGLLayout layout;
        #endregion

        #region Private attributes

        #endregion

        #region Constructors
        public MSAGLGraph(MSAGLLayout layout)
        {
            this.layout = layout;
        }
        #endregion

        #region Public members
        /// <summary>
        /// Calculates the MSAGL layout based on the given nodes and edges.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="edges"></param>
        /// <returns></returns>
        public Dictionary<INode, Vector3> Draw(List<INode> nodes, List<IEdge> edges)
        {
            return GetNodePositions(Transform(nodes, edges), nodes);
        }

        #endregion

        #region Private members
        /// <summary>
        /// Transforms the given nodes and edges into a graph.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="edges"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private GeometryGraph Transform(List<INode> nodes, List<IEdge> edges)
        {
            GeometryGraph geometryGraph = ToMSAGLGraph(nodes, edges);
            EdgeRoutingSettings edgeRoutingSettings = new EdgeRoutingSettings();
            edgeRoutingSettings.EdgeRoutingMode = EdgeRoutingMode.StraightLine;
            LayoutAlgorithmSettings layoutAlgorithmSettings;
            layoutAlgorithmSettings = layout switch
            {
                MSAGLLayout.SugiyamaLayout => new SugiyamaLayoutSettings(),
                MSAGLLayout.FastIncremental => new FastIncrementalLayoutSettings(),
                MSAGLLayout.Ranking => new RankingLayoutSettings(),
                MSAGLLayout.Mds => new MdsLayoutSettings(),
                _ => throw new ArgumentException("Invalid layout settings."),
            };
            layoutAlgorithmSettings.PackingMethod = PackingMethod.Columns;
            layoutAlgorithmSettings.NodeSeparation = 5.0;
            LayoutHelpers.CalculateLayout(geometryGraph, layoutAlgorithmSettings, null);
            return geometryGraph;
        }
        /// <summary>
        /// Calculates the MSAGL layout based on the given nodes and edges.
        /// </summary>
        /// <param name="msagGraph"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private Dictionary<INode, Vector3> GetNodePositions(GeometryGraph msagGraph, List<INode> nodes)
        {
            Dictionary<INode, Vector3> dictionary = new Dictionary<INode, Vector3>();
            foreach (INode node2 in nodes)
            {
                Node node = msagGraph.FindNodeByUserData(node2);
                dictionary.Add(node2, new Vector3((float)node.BoundingBox.Center.X, (float)node.BoundingBox.Center.Y, 0f));
            }
            return dictionary;
        }
        /// <summary>
        /// Converts the given nodes and edges into a graph.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="edges"></param>
        /// <returns></returns>
        private static GeometryGraph ToMSAGLGraph(List<INode> nodes, List<IEdge> edges)
        {
            GeometryGraph geometryGraph = new GeometryGraph();
            foreach (INode node in nodes)
            {
                Node item = new Node(CurveFactory.CreateRectangle(0.5, 0.5, default(Point)), node);
                geometryGraph.Nodes.Add(item);
            }
            foreach (IEdge edge in edges)
            {
                geometryGraph.Edges.Add(new Edge(geometryGraph.FindNodeByUserData(edge.Source), geometryGraph.FindNodeByUserData(edge.Target))
                {
                    Weight = 1,
                    UserData = edge
                });
            }
            return geometryGraph;
        }
        #endregion
    }
}
