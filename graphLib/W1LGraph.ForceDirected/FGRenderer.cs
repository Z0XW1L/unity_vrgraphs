/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using EpForceDirectedGraph.cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using W1LGraph.Common;
using IGraph = W1LGraph.Common.IGraph;

namespace W1LGraph.ForceDirected
{
    /// <summary>
    /// Renderer for the force-directed graph. Updates the position vector for all nodes.
    /// </summary>
    internal class FGRenderer : AbstractRenderer
    {
        #region Public attributes
        /// <summary>
        /// Contains the position of all nodes in the graph.
        /// </summary>
        public Dictionary<INode, Vector3> PositionVector { get; private set; } = new Dictionary<INode, Vector3>();

        #endregion

        #region Private attributes

        #endregion

        #region Constructors
        public FGRenderer(IForceDirected iForceDirected)
            : base(iForceDirected)
        {
        }
        #endregion

        #region Public members
        public override void Clear()
        {
            PositionVector.Clear();
        }

        protected override void drawEdge(Edge iEdge, AbstractVector iPosition1, AbstractVector iPosition2)
        {
            // Empty. Edge positions do not need to be determined.
        }

        protected override void drawNode(Node iNode, AbstractVector iPosition)
        {
            if(PositionVector.ContainsKey((INode)iNode.Tag))
            {
                Vector3 pos = PositionVector[(INode)iNode.Tag];
                pos.X = iPosition.x;
                pos.Y = iPosition.y;
                pos.Z = iPosition.z;
            } else {
                PositionVector[(INode)iNode.Tag] = new Vector3(iPosition.x, iPosition.y, iPosition.z);
            }
        }
        #endregion

        #region Private members

        #endregion
    }
}
