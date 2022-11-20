/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using Assets.Graph;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using W1LGraph.Common;

/// <summary>
/// Represents a single node in a given graph.
/// </summary>
public class Node : MonoBehaviour, INode
{
    #region Private attributes
    private static long GLOBAL_ID = 0;
    private bool isActivated = false;
    private IXRGraph xrGraph;
    [SerializeField] private TextMeshPro tmp;
    #endregion

    #region Public attributes
    public Node Parent { get; set; }
    public object Data { get; set; }
    public List<Node> Children { get; set; } = new List<Node>();
    #endregion

    #region INode
    public long ID {get; private set;}
    #endregion

    #region Constructor
    public Node()
    {
        this.ID = GLOBAL_ID++;
    }
    #endregion

    #region Public Members
    // Start is called before the first frame update
    void Start()
    {
        tmp.text = name;
        XRGrabInteractable xrGrab = GetComponent<XRGrabInteractable>();
        xrGrab.selectEntered.AddListener(onNodeSelectEntered);
        xrGrab.selectExited.AddListener(onNodeSelectExited);
        xrGrab.activated.AddListener(onNodeActivated);
        xrGrab.deactivated.AddListener(onNodeDeactivated);
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
    void OnDestroy()
    {
        XRGrabInteractable xrGrab = GetComponent<XRGrabInteractable>();
        xrGrab.selectEntered.RemoveListener(onNodeSelectEntered);
        xrGrab.selectExited.RemoveListener(onNodeSelectExited);
        xrGrab.activated.RemoveListener(onNodeActivated);
        xrGrab.deactivated.RemoveListener(onNodeDeactivated);
        this.xrGraph = null;
    }
    /// <summary>
    /// Informs the given element of event functions. 
    /// The events (select, activate) are not passed to the graph instance directly as 
    /// this object may be destroyed which could lead to potential issues.
    /// </summary>
    /// <param name="xrGraph"></param>
    public void Register(IXRGraph xrGraph)
    {
        this.xrGraph = xrGraph;
    }
    #endregion
    #region Private Members
    /// <summary>
    /// Fired multiple times whenever this object is being grabbed.
    /// </summary>
    /// <param name="args"></param>
    private void onNodeSelectEntered(SelectEnterEventArgs args)
    {
        xrGraph.OnNodeSelect(this);
    }
    /// <summary>
    /// Fired multiple times whenever this object is not grabbed anymore.
    /// </summary>
    /// <param name="args"></param>
    private void onNodeSelectExited(SelectExitEventArgs args)
    {
        if (!args.interactorObject.isSelectActive)
        {
            xrGraph.OnNodeDeselect(this);
        }
    }
    /// <summary>
    /// Fired when the action 'activate' is performed.
    /// </summary>
    /// <param name="args"></param>
    private void onNodeActivated(ActivateEventArgs args)
    {
        if (!isActivated)
        {
            isActivated = true;
            xrGraph.OnNodeActivate(this);
        }
        else
        {
            isActivated = false;
            xrGraph.OnNodeDeactivate(this);
        }
    }
    /// <summary>
    /// Fired when the action 'activate' is released.
    /// </summary>
    /// <param name="args"></param>
    private void onNodeDeactivated(DeactivateEventArgs args)
    {
        // Empty
    }
    #endregion
}
