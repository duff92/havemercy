using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


public class VRMovement : MonoBehaviour
{

    //float speed = 5.0f;
    //float rotationSpeed = 250.0f;

    //Rigidbody rb;
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}
    //// get the rotation angle, and let batman rotate
    //private void Update()
    //{
    //    float translation = CrossPlatformInputManager.GetAxis("Vertical") * speed;
    //    //float rotation = CrossPlatformInputManager.GetAxis ("Horizontal") * rotationSpeed;

    //    translation *= Time.deltaTime;
    //    //rotation *= Time.deltaTime;
    //    transform.Translate(0, 0, translation);
    //    //rb.MovePosition(new Vector3(transform.position.x, 1.0f, transform.position.z));
    //    transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
    //    //transform.rotation = GvrController.Orientation;
    //}
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}