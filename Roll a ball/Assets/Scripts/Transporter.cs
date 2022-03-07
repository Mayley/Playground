using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour {
	
	public float antiTeleportTimer = 5f;
	public bool teleported = true;
	public float teleportTimer = 5f;
	public GameObject[] otherTeleporters;

	private Camera fpscamera;
	private GameObject teleport;
	private GameObject[] teleporter;
	private Vector3 teleportRotation;


	// Use this for initialization
	void Start () {
		fpscamera = Camera.main;
		otherTeleporters = GameObject.FindGameObjectsWithTag ("Teleporter");

		foreach (GameObject teleporter in otherTeleporters) {
			teleporter.GetComponent<Transporter> ().teleportTimer = 10f;
			teleporter.GetComponent<Transporter> ().teleported = true;
			teleporter.GetComponent<Transporter> ().antiTeleportTimer = 5f;
		}

	}

	// Update is called once per frame
	void Update () {
		if ( teleported && teleportTimer <= antiTeleportTimer ) {
			teleportTimer += Time.deltaTime;

			foreach (GameObject teleporter in otherTeleporters) {
				teleporter.GetComponent<Transporter> ().teleportTimer = teleportTimer;
			}
				
		}
	}
		
	void OnTriggerEnter(Collider character) {

		if (teleportTimer >= antiTeleportTimer) {
			if (character.CompareTag ("Player")) {
				
				teleportTimer = 0f;
				teleported = !teleported;
				foreach (GameObject teleporter in otherTeleporters) {
					teleporter.GetComponent<Transporter> ().teleported = true;
				}

				teleport = transform.Find ("TeleportLocation").gameObject;

				teleportRotation.x = teleport.transform.rotation.eulerAngles.x;
				teleportRotation.y = teleport.transform.rotation.eulerAngles.y;
				teleportRotation.z = teleport.transform.rotation.eulerAngles.z;

				fpscamera.gameObject.GetComponent<CamControl> ().enabled = false;

				character.transform.position = teleport.transform.position;
				character.transform.rotation = teleport.transform.rotation;
				fpscamera.gameObject.GetComponent<CamControl> ().mouseLook.x = teleportRotation.x;
				fpscamera.gameObject.GetComponent<CamControl> ().mouseLook.x = teleportRotation.y;

				fpscamera.gameObject.GetComponent<CamControl> ().enabled = true;

			}
		} else {
			
			Debug.Log ("Teleported too soon");

		}
		
	}

}



