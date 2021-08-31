using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFSM : MonoBehaviour
{
    [SerializeField] CarAIHandler carAIHandler;
    private FSM fsm;
    public float updateTimer = 0.3f;

    public float tireCondition = 100f;


    public bool start = false;
    public bool pitstop = false;

    private void Start()
    {
        FSMState stop = new FSMState();
        stop.stayActions.Add(Stop);

        FSMState race = new FSMState();
        race.stayActions.Add(Race);

        FSMState pitstop = new FSMState();
        pitstop.stayActions.Add(Pit);

        FSMTransition t1 = new FSMTransition(StartToRace);
        FSMTransition t2 = new FSMTransition(StopRace);
        FSMTransition t3 = new FSMTransition(GoPit);
        FSMTransition t4 = new FSMTransition(ExitPit);

        stop.AddTransition(t1, race);
        race.AddTransition(t2, stop);
        race.AddTransition(t3, pitstop);
        pitstop.AddTransition(t4, race);

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
        return start;
    }

    public bool StopRace()
    {
        return !start;
    }

    public bool GoPit()
    {
        if (tireCondition <= 20f)
        {
            return true;
        }
        return false;
    }

    public bool ExitPit()
    {
        if (tireCondition >= 100f)
        {
            return true;
        }
        return false;
    }

    // ACTIONS

    public void Race()
    {
        Debug.Log("Race");
        carAIHandler.FollowRaceWaypoints();
        tireCondition -= 0.1f;
    }

    public void Stop()
    {
        Debug.Log("Stop");
    }

    public void Pit()
    {
        Debug.Log("Pit");
        carAIHandler.FollowPitstopWaypoints();
        tireCondition += 0.1f;
    }

}
