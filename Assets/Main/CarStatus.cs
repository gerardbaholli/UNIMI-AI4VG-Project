using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatus : MonoBehaviour
{
    public enum ActualLocation { Track, Pitstop };

    [SerializeField] GameObject boxAssigned;

    public ActualLocation actualLocation = ActualLocation.Track;
    public float tireConditions = 100f;
    public bool needToPit = false;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (tireConditions < 20)
        {
            needToPit = true;
        }
        else
        {
            needToPit = false;
        }
    }

    public Vector3 GetBoxPosition()
    {
        return boxAssigned.transform.position;
    }

    public ActualLocation GetActualLocation()
    {
        return actualLocation;
    }

    public void SetActualLocation(ActualLocation value)
    {
        actualLocation = value;
    }
    
    public float GetTiresCondition()
    {
        return tireConditions;
    }

    public void PutNewTires()
    {
        tireConditions = 100f;
        Debug.Log("Putted new tires!!!");
    }
}
