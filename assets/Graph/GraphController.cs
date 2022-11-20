/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Graph.Layouts;
using W1LGraph.Common;
using W1LGraph.ForceDirected;

/// <summary>
/// This class is responsible for buidling and maintaining a graph.
/// </summary>
public class GraphController : MonoBehaviour, IGraph
{
    #region Private attributes
    protected readonly HashSet<Node> pinnedNodes = new HashSet<Node>();
    [SerializeField] protected GameObject nodeFab;
    [SerializeField] protected GameObject edgeFab;
    [SerializeField] protected Material nodeMaterial;
    [SerializeField] protected Material highlightMaterial;
    protected readonly IGraphLayout layout = new ForceDirectedLayout(new FGSettings()
    {
        Stiffness = 81.67f,
        Repulsion = 0.000081f * 40000.0f,
        Damping = 1f * 0.8f
    });
    #endregion

    #region Public attributes
    public List<INode> Nodes { get; set; } = new List<INode>();
    public List<IEdge> Edges { get; set; } = new List<IEdge>();
    #endregion

    #region Constructor
    public GraphController()
    {
        // Empty
    }
    public GraphController(IGraphLayout layout)
    {
        this.layout = layout;
    }
    #endregion

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        // The data source may not be ready at the start.
        // Therefore, nodes are created on update.
    }

    // Update is called once per frame
    void Update()
    {
        if (this.Nodes.Count == 0)
        {
            AddNodes();
            this.layout.Add(Nodes, Edges);
        }
        else
        {
            this.layout.Draw(Nodes, Edges);
        }
    }
    #endregion

    #region Public members
    /// <summary>
    /// Clears the graph from all of íts contents.
    /// </summary>
    public void Clear()
    {
        Nodes.Clear();
        Edges.Clear();
    }
    #endregion

    #region Private members
    /// <summary>
    /// Creates nodes from the given data source.
    /// </summary>
    protected virtual void AddNodes()
    {
        // Empty. May be implemented in subclasses.
    }
    protected Dictionary<long, INode> RemoveNodes(Node node)
    {
        Dictionary<long, INode> nodes = new Dictionary<long, INode>();
        _CollectNodeChildren(node, nodes);
        nodes.Remove(node.ID);
        List<IEdge> edges = this.Edges.Where(e => nodes.ContainsKey(e.Source.ID) || nodes.ContainsKey(e.Target.ID)).ToList();
        this.layout.Remove(new List<INode>(nodes.Values), edges);
        for (int i = 0; i < this.Edges.Count; i++)
        {
            IEdge edge = this.Edges[i];
            if (nodes.ContainsKey(edge.Source.ID) || nodes.ContainsKey(edge.Target.ID))
            {
                Destroy(((Edge)this.Edges[i]).gameObject);
                this.Edges.RemoveAt(i--);
            }
        }
        for (int i = 0; i < this.Nodes.Count; i++)
        {
            if (nodes.ContainsKey(this.Nodes[i].ID) && this.Nodes[i].ID != node.ID)
            {
                Destroy(((Node)this.Nodes[i]).gameObject);
                this.Nodes.RemoveAt(i--);
            }
        }
        node.Children.Clear();
        return nodes;
    }
    /// <summary>
    /// Gathers all children nodes starting from the given node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="nodes"></param>
    /// <param name="edges"></param>
    private void _CollectNodeChildren(Node node, Dictionary<long, INode> nodes)
    {
        List<Node> visit = new List<Node> { node };
        while (visit.Count > 0)
        {
            Node _node = visit[0];
            visit.RemoveAt(0);
            visit.AddRange(_node.Children);
            nodes[_node.ID] = _node;
        }
    }
    #endregion
}
