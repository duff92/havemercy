using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


public class VRMovement : MonoBehaviour {

	float speed = 5.0f;
	float rotationSpeed = 250.0f;

	// get the rotation angle, and let batman rotate
	private void Update()
	{
		float translation = CrossPlatformInputManager.GetAxis ("Vertical") * speed;
		//float rotation = CrossPlatformInputManager.GetAxis ("Horizontal") * rotationSpeed;
	

		translation *= Time.deltaTime;
		//rotation *= Time.deltaTime;
		transform.Translate (0, 0, translation);
		//transform.rotation = GvrController.Orientation;
	}


}
