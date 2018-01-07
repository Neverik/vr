using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTriggerBehaviour : MonoBehaviour {

	public GameObject teleportTo;
	public GameObject boat;
	public GameObject filter;

	void Start () {
		filter.SetActive (false);
	}

	void Update () {
		if (transform.parent == boat.transform && boat.GetComponent <MoveBoat> ().finish == false) {
			//transform.position = teleportTo.transform.position;
			filter.SetActive (false);
			GetComponent <Rigidbody> ().mass = 1f;
			GetComponent <Autowalk> ().dieSpeed = 3f;
			GetComponent <Autowalk> ().speed = 0f;
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Less") {
			GetComponent <Autowalk> ().dieSpeed = 6f;
		}
		if (other.gameObject.tag == "WaterTrigger") {
			StartCoroutine ("Toboat");
		}
		if (other.gameObject.name == "Cube (2)") {
			GetComponent <Autowalk> ().dieSpeed = 6f;
			GetComponent <Autowalk> ().speed = 0.1f;
			filter.SetActive (true);
			GetComponent <Rigidbody> ().mass = 0.5f;
		}
	}

	IEnumerator Toboat () {
		transform.parent = boat.transform;
		GetComponent <Autowalk> ().speed = 0f;
		GetComponent <Rigidbody> ().isKinematic = true;
		//GetComponent <SimpleSmoothMouseLook> ().enabled = false;
		transform.rotation.Set (0f, 0f, 0f, transform.rotation.w);
		while (Vector3.Distance (transform.position, teleportTo.transform.position) >= 0.001f) {
			transform.rotation.Set (0f, 0f, 0f, transform.rotation.w);
			yield return new WaitForSeconds (0.01f);
			transform.rotation.Set (0f, 0f, 0f, transform.rotation.w);
			transform.position = Vector3.Lerp (transform.position, teleportTo.transform.position, 0.10f);
			//Debug.Log (transform.rotation.y.ToString());

		}


		transform.position = teleportTo.transform.position;


		boat.GetComponent <MoveBoat> ().started = true;
		GetComponent <Autowalk> ().dieSpeed = 3f;
		//GetComponent <SimpleSmoothMouseLook> ().enabled = true;
	}
}
