using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TreeBird : MonoBehaviour {
	public GameObject img;
	bool flying = false;
	public Transform player;
	bool rotate = false;

	void Update () {
		if (Vector3.Distance (transform.position, player.position) <= 16f && flying == false) {
			flying = true;
			StartCoroutine (Fly());
		}
	
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (player.position - transform.position), 0.1f);

	}

	IEnumerator Fly () {
		yield return new WaitForSeconds (1.5f);
		Transform target = GameObject.Find ("Birdy (1)").transform;
		rotate = true;
		while (Vector3.Distance (target.position, transform.position) >= 1f) {
			yield return new WaitForSeconds (Time.deltaTime/2f);
			transform.position = Vector3.Lerp (transform.position, target.position, 0.035f);
		}
		img.SetActive (true);
		print("works");
		while (Vector3.Distance (player.position, transform.position) >= 2f) {
			yield return new WaitForSeconds (Time.deltaTime/2f);
			transform.position = Vector3.Lerp (transform.position, player.position, 0.13f);
		}
	}

}
