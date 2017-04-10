using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

	CameraController camCon;
	PlayerScript player;
	Vector2 rStickInput;

	public float leftDeadzone, rightDeadzone;
	public Transform cam;
	private Vector3 camForward;
	float horizontalAxis, verticalAxis;

	Vector2 lStickInput;

	Vector3 lStickMovement;

	// Use this for initialization
	void Start()
	{
		camCon = GetComponent<CameraController>();
		player = transform.parent.GetComponent<PlayerScript>();


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

		if (new Vector2(horizontalAxis, verticalAxis).magnitude > leftDeadzone)
		{
			camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;

			lStickMovement = (verticalAxis * camForward + horizontalAxis * camForward).normalized;

			print(horizontalAxis);

			player.Move(lStickMovement);
		}

		rStickInput = new Vector2(Input.GetAxis("HorizontalR"), Input.GetAxis("VerticalR"));

		if (rStickInput.magnitude > rightDeadzone)
		{
			camCon.CameraRotation(rStickInput[0], rStickInput[1]);
		}

	}
}
