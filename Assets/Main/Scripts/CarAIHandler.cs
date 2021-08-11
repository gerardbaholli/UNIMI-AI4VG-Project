using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarAIHandler : MonoBehaviour
{
    public enum AIMode { followPlayer, followWaypoints};

    [Header("AI Settings")]
    public AIMode aiMode;
    public float maxSpeed = 16;

    // Local variables
    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;

    // Waypoints
    WaypointNode currentWaypoint = null;
    WaypointNode[] allWaypoints;

    // Components
    CarController carController;

    private void Awake()
    {
        carController = GetComponent<CarController>();
        allWaypoints = FindObjectsOfType<WaypointNode>();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        switch (aiMode)
        {
            case AIMode.followPlayer:
                FollowPlayer();
                break;
            case AIMode.followWaypoints:
                FollowWaypoints();
                break;
        }

        inputVector.x = TurnTowardTarget();
        inputVector.y = 1.0f;

        //Send the input to the car controller.
        carController.SetInputVector(inputVector);
    }


    private void FollowPlayer()
    {
        if (targetTransform == null)
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (targetTransform != null)
            targetPosition = targetTransform.position;
    }


    private void FollowWaypoints()
    {
        //Pick the cloesest waypoint if we don't have a waypoint set.
        if (currentWaypoint == null)
            currentWaypoint = FindClosestWaypoint();

        //Set the target on the waypoints position
        if (currentWaypoint != null)
        {
            //Set the target position of for the AI. 
            targetPosition = currentWaypoint.transform.position;

            //Store how close we are to the target
            float distanceToWayPoint = (targetPosition - transform.position).magnitude;

            //Check if we are close enough to consider that we have reached the waypoint
            if (distanceToWayPoint <= currentWaypoint.minDistanceToReachWaypoint)
            {
                //If we are close enough then follow to the next waypoint, if there are multiple waypoints then pick one at random.
                currentWaypoint = currentWaypoint.nextWaypointNode[Random.Range(0, currentWaypoint.nextWaypointNode.Length)];
            }
        }
    }

    private WaypointNode FindClosestWaypoint()
    {
        return allWaypoints
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }

    private float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        //Calculate an angle towards the target 
        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        //We want the car to turn as much as possible if the angle is greater than 45 degrees and we wan't it to smooth out so if the angle is small we want the AI to make smaller corrections. 
        float steerAmount = angleToTarget / 45.0f;

        //Clamp steering to between -1 and 1.
        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }


}
