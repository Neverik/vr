using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour {

	bool started = false;

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player" && started == false) {
			Ask.StartAsking ();
			started = true;
		}
	}
}
