using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ask : MonoBehaviour {

	public ParticleSystem explode;
	public List<Question> riddles;
	public float DelayAfterSpeaking;
	public GameObject Container;
	public int riddlesSolved = 0;
	public static int pressedButton = 0;
	public Text[] fourButtons;
	public AudioClip roar;
	AudioSource roarer;
	public AudioClip afterAsking;
	AudioSource aftask;
	public float delayAfterAsking;
	public Text question;

	// Use this for initialization
	void Awake () {
		aftask = gameObject.AddComponent <AudioSource> ();
		aftask.clip = afterAsking;
		roarer = gameObject.AddComponent <AudioSource> ();
		roarer.clip = roar;
	}
	
	public static void StartAsking () {
		GameObject.Find ("Ask").GetComponent <Ask> ().StartCoroutine ("DoAsking");
	}



	IEnumerator DoAsking () {
		yield return null;
		Debug.Log ("Started Asking");
		GetComponent <AudioSource> ().Play ();
		yield return new WaitForSeconds (DelayAfterSpeaking);
		yield return new WaitForSeconds (1f);
		Container.SetActive (true);
		while (riddlesSolved < riddles.Count) {
			Debug.Log ("Asking");
			foreach (Question q in riddles.ToArray ()) {
				question.text = q.whatToAsk;
				fourButtons [0].text = q.answer1;
				fourButtons [1].text = q.answer2;
				fourButtons [2].text = q.answer3;
				fourButtons [3].text = q.answer4;
				while (pressedButton != q.rightAnswer) {
					if (AskButton.isPressed && pressedButton != q.rightAnswer) {
						if (!roarer.isPlaying) {
							roarer.Play ();
						}
						AskButton.isPressed = false;
					}
					Debug.Log (pressedButton.ToString ());
					yield return null;

				}
				riddlesSolved++;
				pressedButton = 0;
			}

		}
		Debug.Log ("Finished");
		Container.SetActive (false);
		aftask.Play ();
		yield return new WaitForSeconds (delayAfterAsking);
		explode.gameObject.SetActive (true);
		explode.Play ();
		GameObject.FindObjectOfType <Spider> ().gameObject.GetComponentInChildren <Renderer> ().enabled = false;
		Debug.Log (explode.isPlaying.ToString());
		//GameObject.FindObjectOfType <Spider> ().gameObject.AddComponent <Rigidbody> ();
		GameObject.FindObjectOfType <Spider> ().gameObject.GetComponent <BoxCollider> ().isTrigger = true;
		yield return new WaitForSeconds (4f);
		GameObject cam = GameObject.Find ("Camera");
		GameObject climb = GameObject.Find ("Climb");
		cam.transform.parent = climb.transform;
		cam.GetComponent <Autowalk> ().speed = 0f;
		//cam.GetComponent <Rigidbody> ().isKinematic = true;

		while (Vector3.Distance (cam.transform.position, climb.transform.position) > 0.05f) {
			yield return new WaitForSeconds (Time.fixedDeltaTime);
			cam.transform.position = Vector3.Lerp (cam.transform.position, climb.transform.position, 0.1f);
		}
		while (cam.transform.localPosition.y <= 50f) {
			yield return null;
			if (GameObject.Find ("cam").transform.rotation.y > -1f && GameObject.Find ("cam").transform.rotation.y < -0.15f) {
				Physics.gravity = new Vector3 (-7.8f, 4f, 0f);
				yield return null;
				Debug.Log ("Does");
				Debug.Log (Physics.gravity.ToString ());
				cam.GetComponent <Rigidbody> ().isKinematic = false;
			} else {
				cam.GetComponent <Rigidbody> ().isKinematic = true;
				Debug.Log ("Doesn't");
				yield return null;
			}

			yield return null;
		}
		Debug.Log ("Finished already");

		Debug.Log (Physics.gravity.ToString ());
	}

}
