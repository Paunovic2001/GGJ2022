using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]float maxMovementSpeed = 1;
    [SerializeField] float smoothTime = 0.2f;
    public Vector3 currentSpeed;
    float velocityX = 0f;
    float velocityY = 0f;
    float velocityZ = 0f;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        //movement inputi
        if (Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            MoveBack();
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            StrafeLeft();
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            StrafeRight();
        }
    }

    //movement metode
    void StrafeLeft()
    {
        rb.velocity = Vector3.left * maxMovementSpeed;
    }
    void StrafeRight()
    {
        rb.velocity = Vector3.right * maxMovementSpeed;
    }

    void MoveForward()
    {
        rb.velocity = Vector3.forward * maxMovementSpeed;
    }

    void MoveBack()
    {
        rb.velocity = Mathf.SmoothDamp(rb.velocity.z, maxMovementSpeed,ref velocityZ, smoothTime) * Vector3.back /* maxMovementSpeed*/;
        currentSpeed.z = velocityZ;
    }
}
