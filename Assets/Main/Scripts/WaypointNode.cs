using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : MonoBehaviour
{
    public enum NodeType { raceNode, pitstopNode };

    [Header("Set the role of the node")]
    [SerializeField] NodeType nodeType;

    // What is the max speed allowed when passing this waypoint
    [Header("Speed set once we reach the waypoint")]
    public float maxSpeed = 0;

    [Header("This is the waipoint we are going towards, not yet reached")]
    public float minDistanceToReachWaypoint = 5;

    public WaypointNode[] nextWaypointNode;

}
