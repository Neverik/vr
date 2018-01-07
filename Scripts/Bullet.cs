using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Vector3 startPoint;
	public Vector3 dir = new Vector3 (5f, 0f, 0f);
	Rigidbody rigidbody;

	void Start () {
		startPoint = transform.position;
		rigidbody = GetComponent <Rigidbody> ();
	}

	void Ouch () {
		rigidbody.velocity = dir;
	}

	void Update () {
		rigidbody.velocity = 5 * (rigidbody.velocity.normalized);
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.layer == 9) {
			col.gameObject.BroadcastMessage ("Kill");
			return;
		}
		if (col.gameObject.layer != 11) {
			transform.position = startPoint;
			rigidbody.velocity = Vector3.zero;
		}
	}
}
