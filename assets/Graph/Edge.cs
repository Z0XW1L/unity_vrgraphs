/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using UnityEngine;
using W1LGraph.Common;

/// <summary>
/// Represents a directed edge between two nodes of a graph.
/// </summary>
public class Edge : MonoBehaviour, IEdge
{
    #region Private attributes
    private LineRenderer lineRenderer;
    private static long GLOBAL_ID = 0;
    #endregion

    #region public attributes
    #endregion

    #region IEdge interface
    [SerializeField] public Node source;
    [SerializeField] public Node target;
    public long ID { get; private set; }
    public INode Source { get { return source; } set { source = (Node)value; } }
    public INode Target { get { return target; } set { target = (Node)value; } }
    #endregion

    #region Constructors
    public Edge()
    {
        this.ID = GLOBAL_ID++;
    }
    #endregion

    #region Public members
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }
    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, ((Node)Source).transform.position);
        lineRenderer.SetPosition(1, ((Node)Target).transform.position);
    }
    #endregion
}
