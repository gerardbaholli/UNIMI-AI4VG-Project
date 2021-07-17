using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    private int count = 0;

    void FixedUpdate()
    {
        Debug.Log("Counter: " + count);
        AddCount();
    }

    private void AddCount()
    {
        count++;
    }

}
