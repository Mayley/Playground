using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
	
	private Camera fpscamera;
	private GameObject respawnLoc;

	// Use this for initialization
	void Start () {
		//fpscamera = this.GetComponentInChildren<Camera>();
		fpscamera = Camera.main;
		respawnLoc = GameObject.FindGameObjectWithTag ("RespawnLoc");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown ("r")) {
			fpscamera.gameObject.GetComponent<CamControl> ().enabled = false;
			transform.position = respawnLoc.transform.position;;
			transform.localRotation = Quaternion.identity;
			fpscamera.gameObject.GetComponent<CamControl> ().mouseLook.x = respawnLoc.transform.rotation.x;
			fpscamera.gameObject.GetComponent<CamControl> ().mouseLook.y = respawnLoc.transform.rotation.y;
			fpscamera.gameObject.GetComponent<CamControl> ().enabled = true;
		}
	}
}