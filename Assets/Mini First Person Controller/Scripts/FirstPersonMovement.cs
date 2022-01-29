using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    public Animator anim;
    GroundCheck gc;
    int animSpeedHash;
    int animInteractHash;
    int animJumpHash;
    int animCrouchHash;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        gc = FindObjectOfType<GroundCheck>();
        animSpeedHash = Animator.StringToHash("Speed");
        animInteractHash = Animator.StringToHash("Interact");
        animJumpHash = Animator.StringToHash("Jump");
        animCrouchHash = Animator.StringToHash("Crouch");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool(animInteractHash, true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetBool(animInteractHash, false);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool(animCrouchHash, true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetBool(animCrouchHash, false);
        }
        anim.SetBool(animJumpHash, !gc.isGrounded);
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
        //Debug.Log(rigidbody.velocity.magnitude);
        anim.SetFloat(animSpeedHash, rigidbody.velocity.magnitude);
    }
}