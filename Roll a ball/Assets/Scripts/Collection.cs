using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collection : MonoBehaviour {

	public Text countText;
	public Text winText;
	public float endTimer = 10;

	private int count;
	private GameObject[] items;
	private int total;
	private bool gameOver = false;

	// Use this for initialization
	void Start () {
		items = GameObject.FindGameObjectsWithTag ("Pick Up");
		total = items.Length;
		SetCountText();
		Debug.Log ("Total boxes: " + total);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver) {
			endTimer -= Time.deltaTime;
			Debug.Log (endTimer);

			winText.text = "THATS IT YOU BEAT THE GAME!! \n Wait " + Mathf.Round (endTimer)+ "s to restart";
			winText.gameObject.SetActive (true);

			if (endTimer <= 0) {
				Scene scene = SceneManager.GetActiveScene(); 
				SceneManager.LoadScene(scene.name);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count += 1;

			if (count <= total) {
				SetCountText();	
			}

			if (count == total) {
				gameOver = true;
			}
		}
	}

	void SetCountText (){
		countText.text = "Count: " + count.ToString ();	
		//Debug.Log ("Collected: " + count + " / " + total);
	}

}
