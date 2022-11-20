/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE.txt for details) 
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using W1LGraph.Common;
using W1LGraph.MSAGL;
using static Assets.Graph.Layouts.IGraphLayout;

namespace Assets.Graph.Layouts
{
    /// <summary>
    /// Layout wrapper for MSAGL graphs.
    /// </summary>
    public class MSAGL2DLayout : IGraphLayout
    {
        #region Public attributes

        #endregion

        #region Private attributes

        #endregion

        #region Constructors

        #endregion

        #region Public members
        public void Add(List<INode> nodes, List<IEdge> edges)
        {
            throw new NotImplementedException();
        }

        public void Draw(List<INode> nodes, List<IEdge> edges)
        {
            MSAGLGraph mGraph = new MSAGLGraph(MSAGLGraph.MSAGLLayout.Ranking);
            var result = mGraph.Draw(nodes, edges);
            foreach (Node node in nodes)
            {
                Vector3 pos = Vector3.Scale(GraphUtils.v2u(result[node]), new Vector3(.01f, .01f, .01f));
                pos.z = 3f * (float)new System.Random().NextDouble();
                node.transform.SetPositionAndRotation(pos, Quaternion.identity);
            }
        }

        public void Remove(List<INode> nodes, List<IEdge> edges)
        {
            throw new NotImplementedException();
        }

        public void TriggerAction(LayoutAction action, params object[] attrs)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private members

        #endregion

    }
}
