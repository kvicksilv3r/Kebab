using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{

	Vector3 pos;
	Vector3 vel;
	Vector3 rBodyVel;
	Quaternion rot;
	bool isJumping;
	bool doubleJump;
	bool onGround;
	AnimationState animState;

	float[] floatVelocities = new float[4];

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


	public Vector3 RBodyVel
	{
		get { return rBodyVel; }
		set { rBodyVel = value; }
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

	public AnimationState AnimState
	{
		get { return animState; }
		set { animState = value; }
	}

}
