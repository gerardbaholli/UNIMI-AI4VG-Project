using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRBT;

public class RaceBT : MonoBehaviour
{
	SystemStatus systemStatus;

	[SerializeField] float reactionTime = 0.1f;
	BehaviorTree AI;

	CarAIHandler carAIHandler;
	CarController carController;
	CarStatus carStatus;

	void Start()
	{
		systemStatus = FindObjectOfType<SystemStatus>();
		carAIHandler = GetComponent<CarAIHandler>();
		carController = GetComponent<CarController>();
		carStatus = GetComponent<CarStatus>();
	}

	public void StartBehaviourTree()
	{
		BTAction a0 = new BTAction(Race);

		AI = new BehaviorTree(a0);

		StartCoroutine(Execute());
	}

	public IEnumerator Execute()
	{
		while (AI.Step())
		{
			yield return new WaitForSeconds(reactionTime);
		}
	}


	// ---------------- ACTIONS ---------------- //
	public bool Race()
	{
		carAIHandler.FollowRaceWaypoints();
		return true;
	}


}