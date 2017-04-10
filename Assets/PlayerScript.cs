using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

	Rigidbody rBody;

	float deadZone = 0.19f;
	public float xVelCur, zVelCur, xVelGoal, zVelGoal;
	float horizontalAxis, verticalAxis;
	public Vector3 velocity;
	protected Vector3 velocityGoal;

	protected Transform child;

	protected TimeTrigger timeTrigger;

	float lastX, lastZ;

	float fallVelocity;

	//Jumpstuff
	RaycastHit groundHit;
	Ray groundRay;
	public bool onGround, isJumping, doubleJump;

	[SerializeField]
	protected float jumpForce, airTime, maxAirTime, extendedJumpForce;


	[SerializeField]
	float axisMulti, acceleration, goalAcceleration, rotationSpeed;

	// Use this for initialization
	void Start()
	{
		rBody = GetComponent<Rigidbody>();
		timeTrigger = GetComponent<TimeTrigger>();

		velocityGoal = Vector3.zero;

		groundRay = new Ray(transform.position, Vector3.down);
		child = transform.FindChild("PlayerBoy");

	}

	// Update is called once per frame
	void Update()
	{

		#region JumpStuff

		groundRay.origin = transform.position;

		if (rBody.velocity.y < 1)
		{
			if (Physics.Raycast(groundRay, out groundHit, child.GetComponent<Renderer>().bounds.size.y / 2 + 0.1f))
			{
				onGround = true;
				airTime = maxAirTime;
				isJumping = false;
				doubleJump = false;
			}
			else
			{
				onGround = false;
			}
		}

		if (onGround && Input.GetButtonDown("Jump"))
		{
			Jump();
			print("Jump");
		}

		else if (!onGround && !doubleJump && Input.GetButtonDown("Jump"))
		{
			Jump();
			print("DoubleJump");
		}

		#endregion

	}

	void Jump()
	{
		if (isJumping)
		{
			doubleJump = true;
		}
		else
		{
			isJumping = true;
		}

		rBody.velocity = new Vector3(rBody.velocity.x, 0, rBody.velocity.z);

		rBody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
		onGround = false;
	}

	void ExtendedJump()
	{
		airTime -= Time.deltaTime;
		rBody.AddForce(Vector3.up * extendedJumpForce, ForceMode.Force);
	}

	public Vector3[] VecVelocity()
	{
		Vector3[] returnVec = new Vector3[2] { velocity, rBody.velocity };
		return returnVec;
	}

	public void EnableVelocity()
	{
		rBody.useGravity = true;
		rBody.velocity = velocity;
	}

	public void Velocity(Vector3 IncomingVelocity)
	{
		velocity = IncomingVelocity;
		rBody.velocity = velocity;
		rBody.useGravity = true;
	}

	public void Move(Vector3 inputVelocity)
	{
		fallVelocity = rBody.velocity.y;
		rBody.velocity = inputVelocity + new Vector3(0, fallVelocity, 0);
	}

	public void StopVelocity()
	{
		velocity = Vector3.zero;
		rBody.velocity = velocity;
		rBody.useGravity = false;
	}

	public void SetState(PlayerState p)
	{
		transform.rotation = p.Rot;
		transform.position = p.Pos;
		velocity = p.Vel;
		rBody.velocity = Vector3.zero;
		child.rotation = p.Rot;

		onGround = p.OnGround;
		isJumping = p.IsJumping;
		doubleJump = p.DoubleJump;

		xVelCur = p.XVelCur;
		xVelGoal = p.XVelGoal;
		zVelCur = p.ZVelGoal;
		zVelGoal = p.ZVelGoal;

	}

	public bool[] JumpStates()
	{
		bool[] returnBools = new bool[] { onGround, isJumping, doubleJump };

		return returnBools;
	}

	public float[] Velocities()
	{
		float[] returnVels = new float[4] { xVelCur, xVelGoal, zVelCur, zVelGoal };
		return returnVels;
	}

	public Transform Child()
	{
		return child;
	}
}
