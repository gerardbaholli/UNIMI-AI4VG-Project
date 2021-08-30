using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DrawPathHandler : MonoBehaviour
{
    public enum GizmosColor { Red, Blue, Yellow, Green };

    [SerializeField] GizmosColor gizmosColor;
    [SerializeField] Transform transformRootObject;

    private WaypointNode[] waypointNodes;

    void OnDrawGizmos()
    {
        switch (gizmosColor)
        {
            case GizmosColor.Red:
                Gizmos.color = Color.red;
                break;
            case GizmosColor.Blue:
                Gizmos.color = Color.blue;
                break;
            case GizmosColor.Yellow:
                Gizmos.color = Color.yellow;
                break;
            case GizmosColor.Green:
                Gizmos.color = Color.green;
                break;
        }
        
        if (transformRootObject == null)
            return;

        //Get all Waypoints
        waypointNodes = transformRootObject.GetComponentsInChildren<WaypointNode>();

        //Iterate the list
        foreach (WaypointNode waypoint in waypointNodes)
        {
       
            foreach (WaypointNode nextWayPoint in waypoint.nextWaypointNode)
            {
                if (nextWayPoint != null)
                    Gizmos.DrawLine(waypoint.transform.position, nextWayPoint.transform.position);

            }

        }
    }

}
