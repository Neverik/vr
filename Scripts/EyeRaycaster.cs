using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.VR;

public class EyeRaycaster : MonoBehaviour
{

	public RectTransform active;
	float counter = 0f;
	GameObject last = null;
	public float iterations;
	float eachSecond;
	public GameObject cam;

	void Start()
	{
		eachSecond = 1f / iterations;
		active.localScale = Vector3.zero;
	}

	void Update()
	{

		active.localScale = Vector3.one * counter;

		Ray ray = new Ray (cam.transform.position, cam.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider.tag == "UI") {
				if (last == hit.collider.gameObject) {
					counter += eachSecond;
					if (counter >= 1f) {
						active.localScale = Vector3.zero;
						hit.collider.BroadcastMessage ("Ouch");
						counter = 0f;
						last = null;
					}
				} else {
					counter = 0f;
					last = hit.collider.gameObject;
				}
			} else {
				last = null;
				counter = 0f;
			}
		}

	}
}
