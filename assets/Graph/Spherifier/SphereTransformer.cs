/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using W1LGraph.Common;

namespace Assets.Graph.Spherifier
{
    internal class SphereTransformer
    {
        #region Public attributes
        public IGraph graph { get; set; }
        #endregion

        #region Private attributes
        private Dictionary<INode, List<INode>> nodeMap = new Dictionary<INode, List<INode>>();
        private Dictionary<INode, HNode> _nodeMap = new Dictionary<INode, HNode>();
        private const float elementSize = .1f;
        #endregion

        #region Constructors
        public SphereTransformer(IGraph graph, INode root)
        {
            this.graph = graph;
            this.CollectChilds(root);
        }
        #endregion

        #region Public members

        public void CollectChilds(INode root)
        {
            foreach(IEdge edge in graph.Edges)
            {
                var childList = nodeMap.GetValueOrDefault(edge.Source, new List<INode>());
                childList.Add(edge.Target);
                nodeMap[edge.Source] = childList;
            }
            HNode _root = _CollectChilds(new HNode(null, root, 0));
            _nodeMap.Add(root, _root);
        }
        private HNode _CollectChilds(HNode parent)
        {
            if (!nodeMap.ContainsKey(parent.Node)) return parent;
            foreach (INode node in nodeMap[parent.Node])
            {
                HNode child = new HNode(parent, node, parent.Depth+1);
                _nodeMap.Add(node, child);
                parent.Children.Add(child);
                _CollectChilds(child);
            }
            return parent;
        }
        public void Transform()
        {
            foreach (INode parent in nodeMap.Keys)
            {
                Vector3 parentPos = ((Node)parent).transform.position;
                List<INode> nodes = nodeMap[parent];
                float radius = ((float)nodes.Count) * elementSize;
                float currSize = 0f;
                for (int i = 0; i < nodes.Count; i++)
                {
                    INode child = nodes[i];
                    radius = _nodeMap[parent].Size;
                    float theta = ((2f * (float)Math.PI) / (float)nodes.Count)*(float)i;
                    currSize += (_nodeMap[child].Size / 2f);
                    theta = ((2f * (float)Math.PI) / (float)_nodeMap[parent].Size) * currSize;
                    currSize += (_nodeMap[child].Size / 2f);
                    float p1 = radius * (float)Math.Cos(theta) + 0f;
                    float p2 = radius * (float)Math.Sin(theta) + 0f;
                    Transform ctf = ((Node)child).transform;
                    float y = .5f*(float)_nodeMap[child].Depth;
                    ctf.SetPositionAndRotation(
                        parentPos + new Vector3(p1, y, p2), 
                        UnityEngine.Quaternion.identity);
                }
            }
        }
        #endregion

        #region Private members

        #endregion
    }
}
