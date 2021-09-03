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

	[SerializeField] float reactionTime = 0.05f;
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
		BTAction a0 = new BTAction(GoToPitbox);
		BTCondition c1 = new BTCondition(IsOutsidePitstop);
		BTDecorator d0 = new BTDecoratorUntilFail(c1);

		BTSequence s2 = new BTSequence(new IBTTask[] { c1, a0 });

		BTAction a1 = new BTAction(TeleportToBox);
		BTAction a2 = new BTAction(ChangeTires);


		BTSequence s1 = new BTSequence(new IBTTask[] { d0, a1, a2 });

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




	// ---------------- CONDITIONS ---------------- //
	public bool IsInsidePitstop()
	{
		return carStatus.GetActualLocation() == CarStatus.ActualLocation.Pitstop;
	}

	public bool IsOutsidePitstop()
    {
		return !IsInsidePitstop();
    }



	// ---------------- ACTIONS ---------------- //
	public bool GoToPitbox()
    {
		Debug.Log("Going to pitstop");
		carAIHandler.FollowPitstopWaypoints();
		return true;
	}

	// TODO
	public bool BoxEntrance()
	{
		//TODO: SEEK POS

		ChangeTires();

		Debug.Log("Box Entrance");
		return true;
	}

	public bool ChangeTires()
	{
		Debug.Log("Changing tires, NOW: " + carStatus.GetTiresCondition());
		//StartCoroutine(Wait());
		carStatus.PutNewTires();
		Debug.Log("Tires changed, NOW " + carStatus.GetTiresCondition());
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

	//TO BE DELETED
	public bool TeleportToBox()
    {
		Debug.Log("TELEPORTING");
		Vector3 boxPosition = carStatus.GetBoxPosition();

		gameObject.transform.position = boxPosition;
		return gameObject.transform.position == carStatus.GetBoxPosition();
    }

}
