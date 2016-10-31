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

    public float slowtime = 4.0f;

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
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
/*
﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections.Generic;

public class VRMovement : MonoBehaviour
{

    public float speed = 7.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    // add a lowpass filter to accelerometer so that it won't be too sensitive
    protected Queue<Vector3> filterDataQueue = new Queue<Vector3>();
    public int filterLength = 4; //you could change it in inspector

    private float yrot;
    private Quaternion var;
    private Vector3 forward;
    private Vector3 dir;
    private float movementData;
    private CharacterController controller;

    private bool walking = false;
    public float remaingwalkingtime;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        for (int i = 0; i < filterLength; i++)
            filterDataQueue.Enqueue(Input.acceleration); //filling the queue to requered length
    }

    public Vector3 LowPassAccelerometer()
    {
        if (filterLength <= 0)
            return Input.acceleration;
        filterDataQueue.Enqueue(Input.acceleration);
        filterDataQueue.Dequeue();

        Vector3 vFiltered = Vector3.zero;
        foreach (Vector3 v in filterDataQueue)
            vFiltered += v;
        vFiltered /= filterLength;
        return vFiltered;
    }

    // end of adding a lowpass filter, return a filtered accelerometer

    void Update()
    {
        yrot = transform.rotation.eulerAngles.y;

        //get angle rotation around y axis
        var = Quaternion.AngleAxis(yrot, Vector3.up);
        forward = var * Vector3.forward;

        dir = LowPassAccelerometer(); // get the movement from the mobile

        Debug.Log(dir);

        movementData = Mathf.Abs(dir.y);

        if (movementData >= 1.0)
        {
            walking = true;
            remaingwalkingtime = Time.time;

            if (controller.isGrounded)
            {
                moveDirection = forward;

                // move the player, and can only move forward
                //moveDirection *= Input.GetAxis("Horizontal") * (-1);
                moveDirection *= speed * movementData;
            }

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
        //walk a little bit after you have no accelration on phone, so that you don't stop when change direction of acceleration of your head
        else if (walking && remaingwalkingtime > 0)
        {
            if (Time.time - remaingwalkingtime > 0.25)
            {
                walking = false; 
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
*/