using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{

	Vector3 pos;
	Vector3 vel;
	Quaternion rot;
	bool isJumping;
	bool doubleJump;
	bool onGround;


	public Vector3 Pos
	{
		get { return pos; }
		set { pos = value; }
	}

	public Quaternion Rot
	{
		get { return rot; }
		set { rot = value; }
	}

	public Vector3 Vel
	{
		get { return vel; }
		set { vel = value; }
	}

	public bool DoubleJump
	{
		get { return doubleJump; }
		set { doubleJump = value; }
	}

	public bool IsJumping
	{
		get { return isJumping; }
		set { isJumping = value; }
	}

	public bool OnGround
	{
		get { return onGround; }
		set { onGround = value; }
	}


}
