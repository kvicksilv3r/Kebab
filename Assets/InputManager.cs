using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

	CameraController camCon;
	PlayerScript playerScript;
	Transform player;
	Vector2 rStickInput;

	public float rotSpeed = 360f;
	public float leftDeadzone, rightDeadzone;
	public Transform cam;
	public Vector3 camForward;
	float horizontalAxis, verticalAxis;

	public float characterAcceleration;
	Vector3 characterVelocity;

	TimeTrigger timeTrigger;

	Vector2 lStickInput;

	Transform mummy;

	control_script animationControlls;

	public float moveSpeedMult;

	float inputVectorMag;

	public Vector3 lStickMovement;

	// Use this for initialization
	void Start()
	{
		camCon = GetComponent<CameraController>();
		player = transform.root;
		playerScript = player.GetComponent<PlayerScript>();
		timeTrigger = GetComponent<TimeTrigger>();
		mummy = GameObject.Find("Mummy_char").transform;
		animationControlls = mummy.GetComponent<control_script>();

		if (!cam)
		{
			if (Camera.main != null)
			{
				cam = Camera.main.transform;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{

		horizontalAxis = Input.GetAxis("Horizontal");
		verticalAxis = Input.GetAxis("Vertical");

		if (Input.GetButton("Reversal") && timeTrigger.CanReverse())
		{
			playerScript.StopVelocity();
			timeTrigger.ReverseTime();
		}
		else
		{
			inputVectorMag = new Vector2(horizontalAxis, verticalAxis).magnitude;
			if (inputVectorMag > leftDeadzone)
			{
				RotateMummy();
				Run();
			}
			else if (characterVelocity.magnitude > 0)
			{
				Walk();
			}
			else
			{
				Idle();
			}
		}

		if (playerScript.JumpStates()[0] && Input.GetButtonDown("Jump"))
		{
			Jump();
		}

		else if (!playerScript.JumpStates()[0] && !playerScript.JumpStates()[2] && Input.GetButtonDown("Jump"))
		{
			Jump();
		}

		rStickInput = new Vector2(Input.GetAxis("HorizontalR"), Input.GetAxis("VerticalR"));

		if (rStickInput.magnitude > rightDeadzone)
		{
			camCon.CameraRotation(rStickInput[0], rStickInput[1]);
		}

		if (Input.GetButtonUp("Reversal"))
		{
			timeTrigger.StopReversal();
		}

	}

	void Run()
	{
		//Makes the character run in the desired direction, correct to the camera rotation
		//Moves the character and triggers the run animation
		camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;

		lStickMovement = (verticalAxis * camForward + horizontalAxis * cam.right).normalized;

		characterVelocity.x = Mathf.Lerp(characterVelocity.x, lStickMovement.x, characterAcceleration);
		characterVelocity.z = Mathf.Lerp(characterVelocity.z, lStickMovement.z, characterAcceleration);

		playerScript.Move(characterVelocity * moveSpeedMult);

		animationControlls.MovementAnimation(inputVectorMag);
	}

	void Walk()
	{
		//Slow the character down towards zero movementspeed, while still moving it. 
		//Trigger the walk animation

		characterVelocity.x = Mathf.Lerp(characterVelocity.x, 0, characterAcceleration);
		characterVelocity.z = Mathf.Lerp(characterVelocity.z, 0, characterAcceleration);

		if (characterVelocity.magnitude < 0.05)
		{
			characterVelocity = Vector3.zero;
		}

		playerScript.Move(characterVelocity * moveSpeedMult);

		animationControlls.Walk();
	}

	void Jump()
	{
		playerScript.Jump();
		animationControlls.Damage();
	}

	void Idle()
	{
		animationControlls.OtherIdle();
	}

	void RotateMummy()
	{
		// calculate the Quaternion for the rotation
		Quaternion rot = Quaternion.Slerp(mummy.rotation, Quaternion.LookRotation(-lStickMovement), rotSpeed * Time.deltaTime);

		mummy.rotation = rot;
		mummy.eulerAngles = new Vector3(0, mummy.eulerAngles.y, 0);
	}
}
