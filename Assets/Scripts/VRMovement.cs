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
    private GameObject sceneCamera;

    private Vector3 moveDirection = Vector3.zero;

    public float slowtime = 4.0f;
    void Start()
    {
        sceneCamera = GameObject.Find("Scene Camera");
    }
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        float yrot = transform.rotation.eulerAngles.y;

        //get angle rotation around y axis
        Quaternion var = Quaternion.AngleAxis(yrot, Vector3.up);
        Vector3 forward = var * Vector3.forward;
        if (controller.isGrounded)
        {
            moveDirection = forward;
            //move amount according to input
            moveDirection *= Input.GetAxis("Horizontal") * (-1);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        if (sceneCamera != null)
        {
            sceneCamera.transform.position = this.transform.position;
            sceneCamera.transform.rotation = this.transform.rotation;
        }
        
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}