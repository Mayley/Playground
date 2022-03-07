using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour {

	public GameObject particles;
	public float duration;
	private float timer;
	private bool active = false;
	private ParticleSystem[] pSystem;

	// Use this for initialization
	void Start () {
		timer = 0;
		pSystem = particles.GetComponentsInChildren<ParticleSystem> ();
		Debug.Log (pSystem.Length);
	}
	
	// Update is called once per frame
	void Update () {
		if ((active) && (timer <= duration)) {
			Debug.Log (timer);
			timer += Time.deltaTime;
		} else {
			timer = 0;
			active = false;
			for (int i = 0; i < pSystem.Length; i++)
				pSystem [i].Stop ();
		}
	}

	void OnTriggerEnter (Collider other){
		Debug.Log ("trap active :" + active);
		active = true;

		for (int i = 0; i < pSystem.Length; i++)
			pSystem [i].Play ();
	}
}
