using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

	CameraController camCon;
	PlayerScript playerScript;
	Transform player;
	Vector2 rStickInput;

	public float leftDeadzone, rightDeadzone;
	public Transform cam;
	public Vector3 camForward;
	float horizontalAxis, verticalAxis;

	public float characterAcceleration;
	Vector3 characterVelocity;

	TimeTrigger timeTrigger;

	Vector2 lStickInput;

	public float moveSpeedMult;

	public Vector3 lStickMovement;

	// Use this for initialization
	void Start()
	{
		camCon = GetComponent<CameraController>();
		player = transform.root;
		playerScript = player.GetComponent<PlayerScript>();
		timeTrigger = GetComponent<TimeTrigger>();

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
			if (new Vector2(horizontalAxis, verticalAxis).magnitude > leftDeadzone)
			{
				camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;

				lStickMovement = (verticalAxis * camForward + horizontalAxis * cam.right).normalized;

				characterVelocity.x = Mathf.Lerp(characterVelocity.x, lStickMovement.x, characterAcceleration);
				characterVelocity.z = Mathf.Lerp(characterVelocity.z, lStickMovement.z, characterAcceleration);

				playerScript.Move(characterVelocity * moveSpeedMult);
			}
			else if (characterVelocity.magnitude > 0)
			{
				characterVelocity.x = Mathf.Lerp(characterVelocity.x, 0, characterAcceleration);
				characterVelocity.z = Mathf.Lerp(characterVelocity.z, 0, characterAcceleration);

				if (characterVelocity.magnitude < 0.05)
				{
					characterVelocity = Vector3.zero;
				}

				playerScript.Move(characterVelocity * moveSpeedMult);

			}
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
}
