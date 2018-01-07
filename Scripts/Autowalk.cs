using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Autowalk : MonoBehaviour 
{

	public static int contacts = 0;

	public GameObject lastContact = null;
	public GameObject lastTrigger = null;

	public static bool climbs = false;

	private const int RIGHT_ANGLE = 90; 

	public Image Bar;

	private bool isWalking = false;

	public float energy = 30f;

	public float speed;

	public bool walkWhenLookDown;
	
	public float dieSpeed;

	public double thresholdAngle;
	

	public bool freezeYPosition; 

	public GameObject tree;

	public float yOffset;

	public Transform cam;

	void Awake () {
		contacts = 0;
		dieSpeed = 0.5f;
	}

	void Start () {
		cam = GameObject.Find ("cam").transform;
		StartCoroutine ("Corout");
	}



	void FixedUpdate () 
	{
		Bar.fillAmount = energy / 30f;
		if (energy <= 0f) {
			energy = 0f;
			speed = 0f;
			GameObject.Find ("Boat").GetComponent <Rigidbody> ().Sleep ();
			Invoke ("Reload", 3f);

		}

		
		// Walk when player looks below the threshold angle 
		if (walkWhenLookDown && !isWalking &&  
		    cam.eulerAngles.x >= thresholdAngle && 
		    cam.eulerAngles.x <= RIGHT_ANGLE) 
		{
			isWalking = true;
		} 
		else if (walkWhenLookDown && isWalking && 
			(cam.eulerAngles.x <= thresholdAngle ||
		         cam.eulerAngles.x >= RIGHT_ANGLE)) 
		{
			isWalking = false;
		}
		

		
		if (isWalking) 
		{
			
			Vector3 direction = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized * speed * Time.deltaTime;
			Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
			transform.Translate(rotation * direction);
		}
		
		if(freezeYPosition)
		{
			transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
		}

	}

	public void Reload () {
		
			GameObject.Find ("Boat").GetComponent <MoveBoat> ().speed = 0f;
		GameObject.Find ("Boat").GetComponent <Rigidbody> ().Sleep ();
			SceneManager.LoadScene (0);
	
	}

	IEnumerator Corout () {
		yield return new WaitForSeconds (2f);
		yield return new WaitUntil(() => isWalking == true);

		while (true) {
			yield return new WaitForSeconds (1f);
			energy -= dieSpeed;

			Bar.fillAmount = energy / 30f;

		}
	}

	void OnCollisionEnter (Collision col) {
		lastContact = col.gameObject;

			contacts++;

	}

	void OnCollisionExit (Collision col) {
		
			contacts--;

	}

	void OnTriggerEnter (Collider col) {
		lastTrigger = col.gameObject;
	}
}

