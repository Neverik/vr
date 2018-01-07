using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour {

	public GameObject player;
	public bool isRandom;
	bool eaten = false;
	public bool rotateAtStart = false;

	void Start () {
		if (rotateAtStart) {
			transform.Rotate (new Vector3 (0f, 0f, Random.Range (-180, 180)));
		}
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player" && eaten == false) {
			eaten = true;
			if (isRandom) {
				GetComponent <AudioSource> ().Play ();
			} else {
				GetComponentInChildren <AudioSource> ().Play ();
			}
			Handheld.Vibrate ();

			if (isRandom == true) {
				if (Random.Range (0f, 1f) >= 0.5f) {
					player.GetComponent <Autowalk> ().speed = 1.5f;

					if (player.GetComponent <Autowalk> ().energy <= 0f) {
						player.GetComponent <Autowalk> ().energy = 0f;
					}
				} else {
					player.GetComponent <Autowalk> ().speed = 3.75f;

				}
			} else if (isRandom == false) {
				player.GetComponent <Autowalk> ().energy = player.GetComponent <Autowalk> ().energy+5f;
				if (player.GetComponent <Autowalk> ().energy >= 30f) {
					player.GetComponent <Autowalk> ().energy = 30f;
				}
			}
			transform.localScale = Vector3.zero;

		}
	
	}

}
