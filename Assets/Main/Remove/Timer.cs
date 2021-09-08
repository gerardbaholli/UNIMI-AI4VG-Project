using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textTimer;
    [SerializeField] TextMeshProUGUI textTempo;
    [SerializeField] TextMeshProUGUI textBestTempo;
    private float timer;
    private float tempo;
    private bool timerStarted = false;
    private bool firstCollision = true;
    private float bestTempo;

    private void Start()
    {
        bestTempo = 60.0f;
        textTimer.SetText("");
        textTempo.SetText("");
        textBestTempo.SetText("");
    }

    private void FixedUpdate()
    {
        if (timerStarted)
        {
            ComputeTime();
            textTimer.SetText(timer.ToString().Substring(0, 1));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!timerStarted)
        {
            StartTime();
            firstCollision = false;
        }

        if (!firstCollision)
        {
            ResetTempo();
            ResetTimer();
            tempo = timer;
            ComputeBestTempo();
            timer = 0;
            textTempo.SetText(tempo.ToString());
        }
        
    }

    private void ComputeBestTempo()
    {
        if (bestTempo > tempo && bestTempo != 0 && tempo != 0)
        {
            bestTempo = tempo;
            textBestTempo.SetText(bestTempo.ToString());
        }
    }

    private void StartTime()
    {
        timerStarted = true;
    }

    private void ComputeTime()
    {
        timer += Time.deltaTime;
    }

    private void ResetTimer()
    {
        textTimer.SetText("");
    }

    private void ResetTempo()
    {
        textTempo.SetText("");
    }

}
