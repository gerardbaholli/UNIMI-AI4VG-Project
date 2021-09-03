using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatus : MonoBehaviour
{
    [SerializeField] GameObject boxAssigned;

    public float tireConditions = 100f;
    private void Start()
    {

    }

    private void FixedUpdate()
    {

    }

    public Vector2 GetBoxPosition()
    {
        return boxAssigned.transform.position;
    }
}
