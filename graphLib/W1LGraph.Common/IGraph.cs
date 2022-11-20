/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using System.Collections.Generic;

namespace W1LGraph.Common
{
    /// <summary>
    /// Common graph interface.
    /// </summary>
    public interface IGraph
    {
        #region Public attributes
        /// <summary>
        /// A collection of all nodes contained in this graph.
        /// </summary>
        List<INode> Nodes { get; }
        /// <summary>
        /// A collection of all edges contained in this graph.
        /// </summary>
        List<IEdge> Edges { get; }
        #endregion

        #region Private attributes

        #endregion

        #region Constructors

        #endregion

        #region Public members

        #endregion

        #region Private members

        #endregion
    }
}
