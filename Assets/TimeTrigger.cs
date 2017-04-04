using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrigger : MonoBehaviour
{

	public List<PlayerState> playerStates = new List<PlayerState>();
	PlayerScript player;
	bool reversing = false;

	// Use this for initialization
	void Start()
	{
		InvokeRecord();
		player = GetComponent<PlayerScript>();
	}

	public bool CanReverse()
	{
		if (playerStates.Count > 0 && !reversing)
			return true;

		else return false;
	}

	public void ReverseTime()
	{
		reversing = true;
		CancelInvoke("RecordTime");
		InvokeReversal();
	}

	void RecordTime()
	{
		if (playerStates.Count > 500)
		{
			playerStates.RemoveAt(0);
		}

		PlayerState p = new PlayerState();
		bool[] playerBools = new bool[3];
		p.Pos = transform.position;
		p.Rot = transform.rotation;
		p.Vel = player.Velocity();
		playerBools = player.JumpStates();
		p.OnGround = playerBools[0];
		p.IsJumping = playerBools[1];
		p.DoubleJump = playerBools[2];
		playerStates.Add(p);
		//print(PosRots.Count);
	}

	public void StopReversal()
	{
		reversing = false;
		CancelInvoke("Reversal");
		//PosRots.Clear();
		InvokeRecord();
	}

	void Reversal()
	{
		if (playerStates.Count > 0)
		{
			player.SetState(playerStates[playerStates.Count - 1]);
			playerStates.RemoveAt(playerStates.Count - 1);
		}
		else
		{
			CancelInvoke("Reversal");
		}
	}

	void InvokeReversal()
	{
		InvokeRepeating("Reversal", Time.deltaTime, Time.deltaTime);
	}

	void InvokeRecord()
	{
		InvokeRepeating("RecordTime", Time.deltaTime, Time.deltaTime);
	}
}

