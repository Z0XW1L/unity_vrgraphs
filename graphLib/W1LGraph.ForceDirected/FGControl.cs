/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using EpForceDirectedGraph.cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime;
using System.Text;
using W1LGraph.Common;
using IGraph = W1LGraph.Common.IGraph;

namespace W1LGraph.ForceDirected
{
    /// <summary>
    /// Represents a force-directed graph.
    /// </summary>
    public class FGControl
    {
        #region Public attributes
        public Dictionary<INode, Vector3> PositionVector => renderer.PositionVector;
        #endregion

        #region Private attributes
        private readonly ForceDirected3D fgLayout;
        private readonly Graph fGraph;
        private readonly FGRenderer renderer;
        private readonly Random random = new Random();
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="baseGraph">The common base graph</param>
        /// <param name="settings">Specific settings for the force-directed algorithm</param>
        public FGControl(FGSettings settings)
        {
            fGraph = new Graph();
            fgLayout = new ForceDirected3D(fGraph, settings.Stiffness, settings.Repulsion, settings.Damping);
            renderer = new FGRenderer(fgLayout);
            SetSettings(settings);
        }
        #endregion

        #region Public members
        /// <summary>
        /// Changes the settings according to the given object.
        /// </summary>
        /// <param name="settings"></param>
        public void SetSettings(FGSettings settings)
        {
            fgLayout.Stiffness = settings.Stiffness;
            fgLayout.Repulsion = settings.Repulsion;
            fgLayout.Damping = settings.Damping;
            fgLayout.Threadshold = settings.Threadshold;
        }
        /// <summary>
        /// Adds new nodes and/or edges to the graph.
        /// </summary>
        /// <param name="nodes">Newly added nodes. May be null.</param>
        /// <param name="edges">Newly added edges. May be null.</param>
        public void Add(List<INode> nodes, List<IEdge> edges)
        {
            if(nodes != null)
            {
                foreach (INode inode in nodes)
                {
                    Vector3 vector;
                    if (PositionVector.ContainsKey(inode))
                    {
                        vector = PositionVector[inode];
                    }
                    else
                    {
                        Vector3 vector3 = (PositionVector[inode] = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()));
                        vector = vector3;
                    }
                    Node node = fGraph.CreateNode(new NodeData
                    {
                        mass = 3f,
                        initialPostion = new FDGVector3(vector.X, vector.Y, vector.Z),
                        label = inode.ID.ToString(),
                        origID = inode.ID.ToString()
                    });
                    node.Tag = inode;
                }
            }
            if(edges != null)
            {
                foreach (IEdge edge in edges)
                {
                    Node node2 = fGraph.GetNode(edge.Source.ID.ToString());
                    Node node3 = fGraph.GetNode(edge.Target.ID.ToString());
                    EdgeData iData = new EdgeData
                    {
                        label = edge.ID.ToString()
                    };
                    fGraph.CreateEdge(node2, node3, iData);
                }
            }
        }
        /// <summary>
        /// Removes the given nodes or edges from the graph.
        /// </summary>
        /// <param name="nodes">nodes to be removed. May be null.</param>
        /// <param name="edges">Edges to be removed. May be null.</param>
        public void Remove(List<INode> nodes, List<IEdge> edges)
        {
            if (nodes != null)
            {
                foreach (INode node in nodes)
                {
                    fGraph.RemoveNode(fGraph.GetNode(node.ID.ToString()));
                    PositionVector.Remove(node);
                }
            }
            if (edges != null)
            {
                foreach (IEdge edge in edges)
                {
                    fGraph.RemoveEdge(fGraph.GetEdge(edge.ID.ToString()));
                }
            }
        }
        /// <summary>
        /// Updates the positions of all nodes. The positions are changed in the position vector.
        /// </summary>
        /// <param name="elapsedTimeSeconds"></param>
        public void Draw(float elapsedTimeSeconds)
        {
            renderer.Draw(elapsedTimeSeconds);
        }
        /// <summary>
        /// Updates the position of a single node. If the node is not pinned, the position will be overwritten upon calling <see cref="Draw(float)"/>.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="position"></param>
        public void UpdatePosition(INode node, Vector3 position)
        {
            Node node2 = fGraph.GetNode(node.ID.ToString());
            fgLayout.GetPoint(node2).position = new FDGVector3(position.X, position.Y, position.Z);
        }
        /// <summary>
        /// Prevents position updates for a specific node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="pinned"></param>
        public void SetPinned(INode node, bool pinned)
        {
            Node node2 = fGraph.GetNode(node.ID.ToString());
            node2.Pinned = pinned;
        }
        #endregion

        #region Private members

        #endregion
    }
}
