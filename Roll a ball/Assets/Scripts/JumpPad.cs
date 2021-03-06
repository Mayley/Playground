using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad: MonoBehaviour {

	public float speed;
	public float maxHeight;
	private float bobHeight;
	private ParticleSystem pSystem; 
	private BoxCollider boxCollider;
	private Collider character;
	private bool active = false;
	private float negSpeed;
	private float posSpeed;
	private float bobSpeed;
	private float size;
	private bool bobbing = false;
	// Use this for initialization
	void Start () {
		pSystem = this.GetComponentInChildren<ParticleSystem>();
		boxCollider = this.GetComponent<BoxCollider>();

		boxCollider.size = new Vector3 (boxCollider.size.x, maxHeight, boxCollider.size.z);
		boxCollider.center = new Vector3 (0f, (maxHeight / 2), 0f);

		size = boxCollider.size.y;
		var main = pSystem.main;
		main.startSpeed = size;

		maxHeight += transform.position.y;
		bobHeight = maxHeight;
		negSpeed = -speed /2.5f ;
		posSpeed = speed;
		bobSpeed = posSpeed / 2.5f;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (active) {
			if (character.transform.position.y >= maxHeight) {
				bobbing = true;
				speed = negSpeed;
				var x = Random.Range (0.5f, 1f);
				bobHeight = maxHeight - x;
			} else if (character.transform.position.y < bobHeight) {
				if (!bobbing) {
					speed = posSpeed;
				} else {
					speed = bobSpeed;
				}

			}	

			Movement ();
		}
	}

	void Movement(){
		
		character.GetComponent<Movement1> ().moveDirection.y = speed;
		character.GetComponent<Movement1> ().moveDirection.y += character.GetComponent<Movement1> ().gravity * Time.deltaTime;

	}

		
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			character = other;
			character.GetComponent<CharacterController> ().Move (new Vector3 (0,0.001f,0));
			active = true;
		}

	}
		
	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player")) {
			active = false;
			bobbing = false;
			Movement ();
		}
	}
}


//height = force * jumpTime + 1/2 (gravity) (jumptime^2)