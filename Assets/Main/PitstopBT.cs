using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRBT;

public class PitstopBT : MonoBehaviour
{
	SystemStatus systemStatus;

	[SerializeField] float reactionTime = 0.05f;
	BehaviorTree AI;

	CarAIHandler carAIHandler;
	CarController carController;
	CarStatus carStatus;

	[SerializeField] Transform pitstopEntrance;
	[SerializeField] Transform pitstopExit;


	void Start()
	{
		systemStatus = FindObjectOfType<SystemStatus>();
		carAIHandler = GetComponent<CarAIHandler>();
		carController = GetComponent<CarController>();
		carStatus = GetComponent<CarStatus>();
	}

	public void StartBehaviourTree()
	{
		BTCondition c1 = new BTCondition(IsOutsidePitlane);
		BTAction a0 = new BTAction(GoToPitstop);
		BTSequence s0 = new BTSequence(new IBTTask[] { c1, a0 });
		BTDecorator d0 = new BTDecoratorUntilFail(s0);


		BTCondition c2 = new BTCondition(IsNotInPitstopPosition);
		BTAction a4 = new BTAction(FollowPitlane);
		BTSequence s1 = new BTSequence(new IBTTask[] { c2, a4 });
		BTDecorator d1 = new BTDecoratorUntilFail(s1);


		BTAction a1 = new BTAction(TeleportToBox);
		BTAction a2 = new BTAction(ChangeTires);


		BTCondition c3 = new BTCondition(IsInsidePitlane);
		BTAction a3 = new BTAction(BoxExit);
		BTSequence s3 = new BTSequence(new IBTTask[] { c3, a3 });
		BTDecorator d2 = new BTDecoratorUntilFail(s3);



		BTSequence fs = new BTSequence(new IBTTask[] { d0, d1, a1, a2, d2 });

		AI = new BehaviorTree(fs);

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
	public bool IsInsidePitlane()
	{
		return carStatus.GetActualLocation() == CarStatus.ActualLocation.Pitstop;
	}

	public bool IsOutsidePitlane()
    {
		return !IsInsidePitlane();
    }

	public bool IsInPitstopPosition()
    {
		return IsInsidePitlane() & (gameObject.transform.position == carStatus.GetBoxPosition());
    }

	public bool IsNotInPitstopPosition()
    {
		return !IsInPitstopPosition();
    }



	// ---------------- ACTIONS ---------------- //
	public bool GoToPitstop()
    {
		Debug.Log("Going to pitstop");
		carAIHandler.FollowRaceWaypoints();
		//Debug.Log(IsPitstopAvailableToEnter());
		if (IsPitlaneAvailableToEnter())
        {
			carAIHandler.FollowPitstopWaypoints();
		}
		return true;
	}

	public bool FollowPitlane()
    {
		carAIHandler.FollowPitstopWaypoints();
		if ((carStatus.GetBoxPosition() - gameObject.transform.position).magnitude < 0.5f)
		{
			return false;
        }
		return true;
    }

	// change something
	public bool TeleportToBox()
	{
		Debug.Log("TELEPORTING");
		Vector3 boxPosition = carStatus.GetBoxPosition();

		gameObject.transform.position = boxPosition;
		return gameObject.transform.position == carStatus.GetBoxPosition();
	}

	public bool ChangeTires()
	{
		Debug.Log("Changing tires: " + carStatus.GetTiresCondition());
		//StartCoroutine(Wait());
		carStatus.PutNewTires();
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
		carAIHandler.FollowPitstopWaypoints();
		if (IsOutOfPitlane())
        {
			carAIHandler.FollowRaceWaypoints();
        }
		return true;
	}

	

	// OTHERS
	private bool IsPitlaneAvailableToEnter()
    {
		return 
			((pitstopEntrance.position - gameObject.transform.position).magnitude < 2f) &
			Vector3.Dot(gameObject.transform.up.normalized, pitstopEntrance.position.normalized) > 0;
	}

	private bool IsOutOfPitlane()
    {
		return Vector3.Dot(gameObject.transform.up.normalized, pitstopExit.position.normalized) < 0;
    }

}
