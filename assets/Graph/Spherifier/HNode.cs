/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using System;
using System.Collections.Generic;
using W1LGraph.Common;

namespace Assets.Graph.Spherifier
{
    internal class HNode
    {
        #region Public attributes
        public INode Node { get; set; }
        public List<HNode> Children { get; set; } = new List<HNode>();
        public float Size
        {
            get
            {
                if (Children.Count == 0) return SIZE;
                float _size = 0f;
                foreach(var child in Children)
                {
                    _size += child.Size;
                }
                return _size / 2f; // ((float)Math.PI)
            }
        }
        public int Depth { get; set; }
        public HNode Parent { get; set; }
        #endregion

        #region Private attributes
        private float SIZE = .15f;
        #endregion

        #region Constructors
        public HNode(HNode parent, INode node, int depth)
        {
            Node = node;
            Depth = depth;
            Parent = parent;
        }
        #endregion

        #region Public members

        #endregion

        #region Private members

        #endregion
    }
}
