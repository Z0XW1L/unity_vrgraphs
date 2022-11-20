/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
namespace W1LGraph.Common
{
    /// <summary>
    /// Common edge interface.
    /// </summary>
    public interface IEdge
    {
        #region Public attributes
        /// <summary>
        /// The ID of this object must be unique for all nodes in a graph.
        /// </summary>
        long ID { get; }
        /// <summary>
        /// The source node
        /// </summary>
        INode Source { get; }
        /// <summary>
        /// The target node
        /// </summary>
        INode Target { get; }
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
