using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimiter : MonoBehaviour
{
    public enum Sensor { entranceSpeedLimiter, exitSpeedLimiter };

    [SerializeField] Sensor speedLimiter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CarController carController = collision.gameObject.GetComponentInParent<CarController>();
        if (carController != null)
        {
            if (speedLimiter == Sensor.entranceSpeedLimiter){
                carController.SetMaxSpeed(0.3f);
            }
            else if (speedLimiter == Sensor.exitSpeedLimiter)
            {
                carController.SetMaxSpeed(2.0f);
            }
        }
    }


}
