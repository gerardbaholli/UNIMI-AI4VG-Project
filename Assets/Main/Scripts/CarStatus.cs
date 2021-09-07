using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatus : MonoBehaviour
{
    public enum ActualLocation { Track, Pitlane };

    [SerializeField] GameObject boxAssigned;
    [SerializeField] float tiresWear;

    public ActualLocation actualLocation = ActualLocation.Track;
    public float tiresCondition = 100f;
    public bool needToPit = false;

    private void FixedUpdate()
    {
        UpdatePitStatus();
    }

    private void UpdatePitStatus()
    {
        if (tiresCondition < 20)
            needToPit = true;
        else
            needToPit = false;
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
        return tiresCondition;
    }

    public void PutNewTires()
    {
        tiresCondition = 100f;
    }

    public void ConsumesTires()
    {
        tiresCondition -= tiresWear;
    }

}
