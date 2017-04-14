using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrigger : MonoBehaviour
{

	public List<PlayerState> playerStates = new List<PlayerState>();
	PlayerScript player;
	GameObject mummyRig;
	bool reversing = false;

	// Use this for initialization
	void Start()
	{
		InvokeRecord();
		player = transform.root.GetComponent<PlayerScript>();
		mummyRig = GameObject.Find("mummy_rig");
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
		p.Pos = player.transform.position;
		p.Rot = player.Mummy().rotation;
		p.Vel = player.VecVelocity()[0];
		p.RBodyVel = player.VecVelocity()[1];
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
		player.EnableVelocity();
		CancelInvoke("Reversal");
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

