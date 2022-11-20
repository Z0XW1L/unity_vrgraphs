/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using W1LGraph.Common;
using W1LGraph.ForceDirected;
using static Assets.Graph.Layouts.IGraphLayout;

namespace Assets.Graph.Layouts
{
    /// <summary>
    /// Layout for a force-directed 3d graph.
    /// See https://github.com/juhgiyo/EpForceDirectedGraph.cs for more information.
    /// </summary>
    public class ForceDirectedLayout : IGraphLayout
    {
        #region Public attributes
        #endregion

        #region Private attributes
        private readonly FGControl fGControl;
        private readonly HashSet<Node> pinnedNodes = new HashSet<Node>();
        #endregion

        #region Constructors
        public ForceDirectedLayout(FGSettings settings)
        {
            this.fGControl = new FGControl(settings);
        }
        #endregion

        #region Public members
        /// <summary>
        /// Applies the forced-directed graph method.
        /// </summary>
        public void Draw(List<INode> nodes, List<IEdge> edges)
        {
            fGControl.Draw(Time.deltaTime);
            foreach (Node node in nodes)
            {
                if (pinnedNodes.Contains(node))
                {
                    fGControl.UpdatePosition(node, GraphUtils.u2v(node.transform.position));
                }
                else
                {
                    float scale = 1f;
                    Vector3 pos = Vector3.Scale(GraphUtils.v2u(fGControl.PositionVector[node]), new Vector3(scale, scale, scale));
                    node.transform.SetPositionAndRotation(pos, Quaternion.identity);
                }
            }
        }
        /// <summary>
        /// Adds new nodes or edges to the graph.
        /// </summary>
        /// <param name="nodes">May be null</param>
        /// <param name="edges">May be null</param>
        public void Add(List<INode> nodes, List<IEdge> edges)
        {
            fGControl.Add(nodes, edges);
        }
        /// <summary>
        /// Removes edges and nodes from the graph.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="edges"></param>
        public void Remove(List<INode> nodes, List<IEdge> edges)
        {
            fGControl.Remove(nodes, edges);
        }
        /// <summary>
        /// Triggers a layout specific action.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="attrs"></param>
        public void TriggerAction(LayoutAction action, params object[] attrs)
        {
            List<Node> nodes = attrs.Cast<Node>().ToList();
            foreach (Node node in nodes)
            {
                switch (action)
                {
                    case LayoutAction.Activate:
                        break;
                    case LayoutAction.Select:
                        if(pinnedNodes.Contains(node))
                        {
                            fGControl.SetPinned((Node)node, false);
                            pinnedNodes.Remove((Node)node);
                        } else {
                            fGControl.SetPinned((Node)node, true);
                            pinnedNodes.Add((Node)node);
                        }
                        break;
                }
            }
        }
        #endregion

        #region Private members

        #endregion
    }
}
