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
        CarStatus carStatus = collision.gameObject.GetComponentInParent<CarStatus>();
        if (carController != null)
        {
            if (speedLimiter == Sensor.entranceSpeedLimiter){
                carController.SetMaxSpeed(1.2f);
                carStatus.SetActualLocation(CarStatus.ActualLocation.Pitstop);
            }
            else if (speedLimiter == Sensor.exitSpeedLimiter)
            {
                carController.SetMaxSpeed(2f);
                carStatus.SetActualLocation(CarStatus.ActualLocation.Track);
            }
        }
    }


}
