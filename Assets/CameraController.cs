using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	[SerializeField]
	protected float maxVerticalSpeed, maxHorizontalSpeed;
	protected float slowDown;

	protected Transform parent;

	protected float inpactOffset;

	RaycastHit rayHit;
	Ray rayRay;

	// Use this for initialization
	void Start()
	{
		parent = transform.parent;
	}

	// Update is called once per frame
	void Update()
	{
		//float translation = Input.GetAxis("VerticalR");
		//float rotation = Input.GetAxis("HorizontalR");

		//transform.RotateAround(parent.position, new Vector3(rotation,0,0), maxHorizontalSpeed * Time.deltaTime);
		//transform.RotateAround(parent.position, new Vector3(0, translation, 0), maxVerticalSpeed * Time.deltaTime);

	}
}
