using UnityEngine;
using System.Collections;

using CRBT;

public class SentinelBT : MonoBehaviour
{
	public float reactionTime = .2f;

	RaceStatus systemStatus;
	BehaviorTree AI;

    private void Awake()
    {
		systemStatus = FindObjectOfType<RaceStatus>();
    }

    void Start()
	{
		BTAction a1 = new BTAction(Hide);
		BTAction a2 = new BTAction(Show);

		BTCondition c1 = new BTCondition(SafetyCar);

		BTSequence s1 = new BTSequence(new IBTTask[] { c1, a1 });
		BTSelector s2 = new BTSelector(new IBTTask[] { s1, a2 });

		AI = new BehaviorTree(s2);

		StartCoroutine(Patrol());
	}

	public IEnumerator Patrol()
	{
		while (AI.Step())
		{
			yield return new WaitForSeconds(reactionTime);
		}
	}

	// CONDITIONS
	public bool SafetyCar()
    {
		if (systemStatus.IsSafetyCarDelivered())
        {
			return true;
        }
		return false;
    }

	// ACTIONS

	public bool Hide()
	{
		Debug.Log("SAFETY CAR IS ON - HIDE!");
		GetComponent<MeshRenderer>().enabled = false;
		return true;
	}


	public bool Show()
	{
		Debug.Log("SAFETY CAR IS OFF");
		GetComponent<MeshRenderer>().enabled = true;
		return true;
	}

	public void LaunchFromOutside()
    {
		BTAction a1 = new BTAction(Hide);
		BTAction a2 = new BTAction(Show);

		BTCondition c1 = new BTCondition(SafetyCar);

		BTSequence s1 = new BTSequence(new IBTTask[] { c1, a1 });
		BTSelector s2 = new BTSelector(new IBTTask[] { s1, a2 });

		//BehaviorTree RaceBT = new BehaviorTree(s2);
		AI = new BehaviorTree(s2);

		StartCoroutine(Patrol());
	}

}
