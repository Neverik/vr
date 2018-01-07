using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskButton : MonoBehaviour {

	public int represents;
	public static bool isPressed = false;


	void Ouch () {
		Ask.pressedButton = represents;
		isPressed = true;
	}

}
