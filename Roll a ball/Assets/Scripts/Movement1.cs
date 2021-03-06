using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]

public class Movement1 : MonoBehaviour {
	
	public float walkSpeed = 6;
	public float jumpHeight = 1;
	public float runSpeed = 8;
	public float gravity;
	public float antiBunnyHopFactor = 1;
	public bool toggleRun = false;
	public bool airControl = false;
	public bool limitDiagonalSpeed = true;

	private float jumpForce;
	public Vector3 moveDirection = Vector3.zero;
	private float speed;
	private bool cursorLocked;
	private CharacterController controller;
	private float jumpTimer;
	private bool grounded;

	public Camera fpscamera;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		fpscamera = GetComponentInChildren<Camera> ();
		speed = walkSpeed;
		jumpTimer = antiBunnyHopFactor;
		Cursor.lockState = CursorLockMode.Locked;
		cursorLocked = true;
		gravity = 20;
	}

	void FixedUpdate()
	{
		float inputModifyFactor = (Input.GetAxis("Horizontal") != 0.0f && Input.GetAxis("Vertical") != 0.0f && limitDiagonalSpeed)? .7071f : 1.0f;
		if (controller.isGrounded)
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
			moveDirection = transform.TransformDirection(moveDirection);

			if (!toggleRun) {
				speed = Input.GetButton ("Run") ? runSpeed : walkSpeed;
			}
 				
			if (!Input.GetButton ("Jump")) {
				jumpTimer++;
			}
			else if (jumpTimer >= antiBunnyHopFactor) {
				GetJumpForce (jumpHeight);
				moveDirection.y = jumpForce;
				jumpTimer = 0;
			}
				
		}

		// If air control is allowed, check movement but don't touch the y component
		if (airControl) {
			moveDirection.x = Input.GetAxis("Horizontal") * speed * inputModifyFactor;
			moveDirection.z = Input.GetAxis("Vertical") * speed * inputModifyFactor;
			moveDirection = transform.TransformDirection(moveDirection);
		}


		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	void Update ()
	{
		if (toggleRun && controller.isGrounded && Input.GetButtonDown ("Run")) {
			speed = (speed == walkSpeed) ? runSpeed : walkSpeed;
			Debug.Log ("Running / Toggled run" + speed);
		}

		if (Input.GetButtonDown ("escape")) {
			cursorLocked = !cursorLocked;
			Cursor.lockState = (cursorLocked) ? CursorLockMode.None : CursorLockMode.Locked;
			Debug.Log ("Is cursor locked?: "+cursorLocked);
		}
	}

	public float GetJumpForce(float height){
		jumpForce = Mathf.Sqrt (2 * height*gravity);
		return jumpForce;
	}
}