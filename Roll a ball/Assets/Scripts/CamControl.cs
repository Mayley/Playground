using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {

	public Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;

	public CharacterController character;

	// Use this for initialization
	void Start () {
		character.transform.position = this.transform.position;
	}
		
	void OnDisable(){

	}

	// Update is called once per frame
	void Update () {
		var mousedelta = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		mousedelta = Vector2.Scale (mousedelta, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp (smoothV.x, mousedelta.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp (smoothV.y, mousedelta.y, 1f / smoothing);
		mouseLook += smoothV;
		mouseLook.y = Mathf.Clamp (mouseLook.y, -90f, 90f);

		transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, character.transform.up);
	}


}
