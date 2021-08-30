using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFSM : MonoBehaviour
{
    [SerializeField] CarAIHandler carAIHandler;
    private FSM fsm;
    public float updateTimer = 0.3f;

    public bool stop = false;


    private void Start()
    {
        FSMState race = new FSMState();
        race.stayActions.Add(Race);

        FSMState stop = new FSMState();

        FSMTransition t1 = new FSMTransition(StartToRace);
        FSMTransition t2 = new FSMTransition(StopRace);

        stop.AddTransition(t1, race);
        race.AddTransition(t2, stop);

        fsm = new FSM(stop);

        StartCoroutine(Patrol());
    }

    public IEnumerator Patrol()
    {
        while (true)
        {
            fsm.Update();
            yield return new WaitForSeconds(updateTimer);
        }
    }

    // CONDITIONS

    public bool StartToRace()
    {
        return stop;
    }

    public bool StopRace()
    {
        return !stop;
    }

    // ACTIONS

    public void Race()
    {
        Debug.Log("Race");
        carAIHandler.FollowWaypoints();
        carAIHandler.Move();
    }

    public void Stop()
    {
        Debug.Log("Stop");
    }

}
