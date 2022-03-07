using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour {

	public float climbSpeed = 0.05f;
	private bool isClimbing = false;
	public Movement1 movement;
	private Collider character;
	private float gravity;


	// Use this for initialization
	void Start () {
		isClimbing = false;
		gravity = movement.gravity;
	}
	
	// Update is called once per frame
	void Update () {
		if (isClimbing) {
			if (Input.GetAxis ("Vertical") > 0) {
				movement.moveDirection.y = climbSpeed;
			} else if (Input.GetButton("Crouch")) {
				movement.moveDirection.y = -climbSpeed/1.5f;
			} else {
				movement.moveDirection.y = 0;
			}
		}
	}
				
	void OnTriggerStay(Collider other){
		if (other.CompareTag("Player")){
			character = other;
			isClimbing = true;
			movement.gravity = 0;
		}
	}

	void OnTriggerExit(Collider other){
		Debug.Log ("Not climbing");
			isClimbing = false;
			movement.gravity = gravity;
	}
		
}
