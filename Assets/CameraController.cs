using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	[SerializeField]
	protected float maxVerticalSpeed, maxHorizontalSpeed;

	[SerializeField]
	bool horInvert, vertInvert;

	int horInv = 1, vertInv = 1;

	protected float slowDown;

	protected Transform target;

	protected float inpactOffset;

	RaycastHit rayHit;
	Ray rayRay;

	// Use this for initialization
	void Start()
	{
		target = transform.parent;

		if (horInvert)
			horInv *= -1;

		if (vertInvert)
			vertInv *= -1;
	}

	public void HorizontalMovement(float inputValue)
	{
		transform.RotateAround(target.position, Vector3.up, -inputValue * maxHorizontalSpeed * Time.deltaTime);
		print(-inputValue);
	}

	public void VerticalMovement(float inputValue)
	{
		transform.RotateAround(target.position, transform.TransformDirection(Vector3.right), maxVerticalSpeed * inputValue * Time.deltaTime);
	}

	public void CameraRotation(float horizontal, float vertical)
	{
		transform.RotateAround(target.position, Vector3.up, horInv * horizontal * maxHorizontalSpeed * Time.deltaTime);
		transform.RotateAround(target.position, transform.TransformDirection(Vector3.right), vertInv * vertical * maxVerticalSpeed * Time.deltaTime);
	}


}
