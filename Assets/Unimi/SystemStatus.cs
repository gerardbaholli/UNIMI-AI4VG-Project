using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemStatus : MonoBehaviour
{

    [SerializeField] public bool start = false;
    [SerializeField] public bool saferyCar = false;
    [SerializeField] public bool redFlag = false;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public void DeliverSafetyCar()
    {
        Debug.Log("DeliverSafetyCar");
        saferyCar = true;
    }

    public void BringBackSafetyCar()
    {
        Debug.Log("BringBackSafetyCar");
        saferyCar = false;    
    }

    public void SendRedFlag()
    {
        Debug.Log("SendRedFlag");
        DeliverSafetyCar();
        redFlag = true;
    }

    public bool IsSafetyCarDelivered()
    {
        Debug.Log("IsSafetyCarDelivered");
        return saferyCar;
    }
}
