using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


public class VRMovement : MonoBehaviour
{

    float speed = 5.0f;
    float rotationSpeed = 250.0f;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // get the rotation angle, and let batman rotate
    private void Update()
    {
        float translation = CrossPlatformInputManager.GetAxis("Vertical") * speed;
        //float rotation = CrossPlatformInputManager.GetAxis ("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        //rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        //rb.MovePosition(new Vector3(transform.position.x, 1.0f, transform.position.z));
        transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
        //transform.rotation = GvrController.Orientation;
    }
}