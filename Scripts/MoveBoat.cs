using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoat : MonoBehaviour {

	public float speed;
	public bool started = false;
	public bool finish = false;
	public GameObject cameran;
	private bool dead = false;
	public GameObject cam;

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Obstacle") {
			if (!dead) {
				speed = 0f;
				cameran.GetComponent <Rigidbody> ().isKinematic = false;

				cameran.GetComponent <Autowalk> ().Invoke ("Reload", 4f);
				dead = true;
				cameran.GetComponent <Rigidbody> ().AddForce (cameran.transform.up * 1200f);
				GetComponent <Rigidbody> ().Sleep ();
			}
		} else if (col.gameObject.tag == "Finish") {
			if (!finish) {
				cameran.GetComponent <Rigidbody> ().isKinematic = false;
				cameran.GetComponent <Rigidbody> ().AddForce (cameran.transform.up * 840f);
				cameran.GetComponent <Rigidbody> ().AddForce (Vector3.right * -25f);
				cameran.GetComponent <Autowalk> ().speed = 2.5f;
				cameran.GetComponent <Autowalk> ().dieSpeed = 0f;
				GetComponent <Rigidbody> ().Sleep ();
				started = false;
				finish = true;
				Destroy (col.gameObject);
				//cameran.transform.parent = null;
			}
		}

	}



	void FixedUpdate () {
		if (Input.GetMouseButtonDown (0)) {
			//cameran.GetComponent <Autowalk> ().Invoke ("Reload", 0f);
		}
		if (started && finish == false) {
			GetComponent <Rigidbody> ().MovePosition (transform.TransformPoint(transform.right*0.03f * speed * 5f));

			float rotx = cam.transform.localRotation.y;
			Debug.Log (rotx.ToString ());
			if (rotx >0.2f) {
				transform.Rotate (new Vector3 (0f, 1f, 0f));
			} if (rotx < -0.2f) {
				transform.Rotate (new Vector3 (0f, -1f, 0f));
			}



		}



	}

}






