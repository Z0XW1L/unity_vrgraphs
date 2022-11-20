/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W1LGraph.Common;

namespace Assets.Graph
{
    /// <summary>
    /// Interface for accessing XR related interaction events on a graph.
    /// </summary>
    public interface IXRGraph
    {
        void OnNodeSelect(INode node);
        void OnNodeDeselect(INode node);
        void OnNodeActivate(INode node);
        void OnNodeDeactivate(INode node);
    }
}
