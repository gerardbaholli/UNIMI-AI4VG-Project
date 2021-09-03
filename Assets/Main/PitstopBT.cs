using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRBT;

public class PitstopBT : MonoBehaviour
{
	SystemStatus systemStatus;

	CarAIHandler carAIHandler;
	CarController carController;
	CarStatus carStatus;

	[SerializeField] float reactionTime = 0.02f;
	BehaviorTree AI;

	void Start()
	{
		systemStatus = FindObjectOfType<SystemStatus>();
		carAIHandler = GetComponent<CarAIHandler>();
		carController = GetComponent<CarController>();
		carStatus = GetComponent<CarStatus>();
	}

	public void StartBehaviourTree()
	{
		BTAction a0 = new BTAction(GoToEnterPitbox);
		BTAction a1 = new BTAction(BoxEntrance);
		BTAction a2 = new BTAction(ChangeTires);
		BTAction a3 = new BTAction(BoxExit);


		BTSequence s1 = new BTSequence(new IBTTask[] { a0, a1, a2 });

		AI = new BehaviorTree(s1);

		StartCoroutine(Execute());
	}

	public IEnumerator Execute()
	{
		while (AI.Step())
		{
			yield return new WaitForSeconds(reactionTime);
		}
	}

	// CONDITIONS
	public bool IsInsidePitbox()
	{
		if ((Vector2)gameObject.transform.position != carStatus.GetBoxPosition())
		{
			return false;
		}
		return true;
	}

	// ACTIONS

	public bool GoToEnterPitbox()
    {
		Debug.Log("Going to pitstop");
		carAIHandler.FollowPitstopWaypoints();
		return true;
	}

	public bool BoxEntrance()
	{
		//TODO: SEEK POS

		ChangeTires();

		Debug.Log("Box Entrance");
		return true;
	}

	public bool ChangeTires()
	{
		StartCoroutine(Wait());
		return true;
	}

	private IEnumerator Wait()
	{
		int time = Random.Range(2, 6);
		Debug.Log("Pitstop Time: " + time);
		yield return new WaitForSeconds(time);
	}

	public bool BoxExit()
	{
		Debug.Log("Box Exit");
		return true;
	}


}
