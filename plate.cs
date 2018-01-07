using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate : MonoBehaviour {

	public GameObject sphere;
	public Transform teleportTo;
	public ParticleSystem effect;
	public Renderer toDestroy;

	void Kill () {
		sphere.transform.position = teleportTo.position;
		sphere.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		sphere.GetComponent<Bullet> ().startPoint = teleportTo.position;
		effect.Play ();
		toDestroy.enabled = false;
		toDestroy.gameObject.GetComponent <Collider> ().enabled = false;
	}
}
