using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : MonoBehaviour
{
    public enum NodeType { raceNode, pitstopNode };

    [Header("Set the role of the node")]
    [SerializeField] public NodeType nodeType;

    // What is the max speed allowed when passing this waypoint
    [Header("Speed set once we reach the waypoint")]
    public float maxSpeed = 0;

    [Header("This is the waipoint we are going towards, not yet reached")]
    public float minDistanceToReachWaypoint = 5;

    [SerializeField] public WaypointNode nextWaypointNode;

    private void Start()
    {
        //Check and ensure that there is a waypoint assigned
        if (nextWaypointNode == null)
        {
            //Debug.LogError($"Waypoint {gameObject.name} is missing a nextWaypointNode. Please assign one in the inspector");
        }

    }

}
