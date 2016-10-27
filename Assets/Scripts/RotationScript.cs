using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

        transform.GetChild(0).RotateAround(transform.position, Vector3.left, 90 * Time.deltaTime);
        transform.GetChild(1).RotateAround(transform.position, Vector3.left, 90 * Time.deltaTime);
        transform.GetChild(2).RotateAround(transform.position, Vector3.left, 90 * Time.deltaTime);
        transform.GetChild(3).RotateAround(transform.position, Vector3.left, 90 * Time.deltaTime);
        transform.GetChild(4).RotateAround(transform.position, Vector3.left, 90 * Time.deltaTime);
        transform.GetChild(5).RotateAround(transform.position, Vector3.left, 90 * Time.deltaTime);
        transform.GetChild(6).RotateAround(transform.position, Vector3.left, 90 * Time.deltaTime);
        transform.GetChild(7).RotateAround(transform.position, Vector3.left, 90 * Time.deltaTime);
        transform.GetChild(8).RotateAround(transform.position, Vector3.left, 90 * Time.deltaTime);
    }
}
