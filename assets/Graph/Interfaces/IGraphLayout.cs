/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Graph.Layouts.ForceDirectedLayout;
using UnityEngine;
using W1LGraph.Common;

namespace Assets.Graph.Layouts
{
    /// <summary>
    /// Common layout structure used to unify the access to different graph libraries.
    /// </summary>
    public interface IGraphLayout
    {
        public enum LayoutAction { Activate, Select }
        /// <summary>
        /// Renders the currently used layout.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="edges"></param>
        public void Draw(List<INode> nodes, List<IEdge> edges);
        /// <summary>
        /// Adds new nodes or edges to the graph.
        /// </summary>
        /// <param name="nodes">Newly added nodes, may be null</param>
        /// <param name="edges">Newly added edges, may be null</param>
        public void Add(List<INode> nodes, List<IEdge> edges);
        /// <summary>
        /// Removes a set of nodes and edges from the graph.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="edges"></param>
        public void Remove(List<INode> nodes, List<IEdge> edges);
        /// <summary>
        /// Performs a certain action on the graph.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="attrs"></param>
        public void TriggerAction(LayoutAction action, params object[] attrs);
    }
}
