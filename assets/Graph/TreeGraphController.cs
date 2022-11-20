/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using Assets.Graph;
using Assets.SSH;
using Assets.SSH.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using W1LGraph.Common;
using static Assets.Graph.Layouts.IGraphLayout;

/// <summary>
/// Represents a graph that can process tree items.
/// </summary>
public class TreeGraphController : GraphController, IXRGraph
{
    #region Private attributes
    [SerializeField] AbstractDatasource<TreeItem> datasource;
    #endregion

    #region Public attributes

    #endregion

    #region Constructor

    #endregion

    #region Public members

    #endregion

    #region IXRGraph
    /// <summary>
    /// Triggered by a node whenever it is selected.
    /// </summary>
    /// <param name="node"></param>
    public void OnNodeSelect(INode node)
    {
        this.layout.TriggerAction(LayoutAction.Select, node);
    }
    /// <summary>
    /// Triggered by a node whenever it is deselected.
    /// </summary>
    /// <param name="node"></param>
    public void OnNodeDeselect(INode node)
    {
        this.layout.TriggerAction(LayoutAction.Select, node);
    }
    /// <summary>
    /// Triggered by a node whenever it is activated.
    /// </summary>
    /// <param name="node"></param>
    public void OnNodeActivate(INode node)
    {
        List<INode> nodes = new List<INode>();
        List<IEdge> edges = new List<IEdge>();
        TreeItem item = ((TreeItem)((Node)node).Data);
        List<TreeItem> newItems = datasource.GetData<List<TreeItem>>(item)[0].Contents;
        item.Contents = newItems;
        TreeItem.PostProcessTreeItems(item);
        AddNodes_Recursive(item, (Node)node, nodes, edges);
        this.layout.Add(nodes, edges);
        this.Nodes.AddRange(nodes);
        this.Edges.AddRange(edges);
    }
    /// <summary>
    /// Triggered by a node whenever is is deactivated.
    /// </summary>
    /// <param name="node"></param>
    public void OnNodeDeactivate(INode node)
    {
        this.RemoveNodes((Node)node);
    }
    #endregion

    #region Private members
    /// <summary>
    /// Creates nodes from the given data source.
    /// </summary>
    protected override void AddNodes()
    {
        if (this.datasource == null || !this.datasource.IsReady) return;
        List<TreeItem> items = this.datasource.GetData<List<TreeItem>>(null);
        AddNodes(items, this.Nodes, this.Edges);
    }
    /// <summary>
    /// Creates nodes from the given data source.
    /// </summary>
    protected void AddNodes(List<TreeItem> items, List<INode> nodes, List<IEdge> edges)
    {
        TreeItem.PostProcessTreeItems(items);
        foreach (TreeItem item in items)
        {
            Node node = Instantiate(nodeFab).GetComponent<Node>();
            nodes.Add(node);
            node.Register(this);
            node.name = item.Name;
            node.Data = item;
            AddNodes_Recursive(item, node, nodes, edges);
        }
        ((Node)nodes[0]).transform.GetChild(0).GetComponent<MeshRenderer>().material = highlightMaterial;
    }
    /// <summary>
    /// Creates nodes recursively.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="node"></param>
    private void AddNodes_Recursive(TreeItem item, Node node, List<INode> nodes, List<IEdge> edges)
    {
        foreach (TreeItem child in item.Contents)
        {
            Node childNode = Instantiate(nodeFab).GetComponent<Node>();
            nodes.Add(childNode);
            childNode.Register(this);
            node.Children.Add(childNode);
            childNode.name = child.Name;
            childNode.Data = child;
            Edge edge = Instantiate(edgeFab).GetComponent<Edge>();
            edges.Add(edge);
            edge.Source = node;
            edge.Target = childNode;
            AddNodes_Recursive(child, childNode, nodes, edges);
            if(child.Type == "directory")
            {
                childNode.transform.GetChild(0).GetComponent<MeshRenderer>().material = highlightMaterial;
            }
        }
    }
    #endregion
}
