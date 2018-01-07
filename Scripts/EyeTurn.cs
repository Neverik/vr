using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTurn : MonoBehaviour {

	public bool isTurner;
	public GameObject toRot;

	void Ouch () {
		if (isTurner) {
			toRot.transform.Rotate (new Vector3 (0f, 45f, 0f));
		} else {
			toRot.transform.Rotate (new Vector3 (0f, 90f, 0f));
		}
	}
}
