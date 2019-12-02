using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0, 1)]
    public float airControlPercent;
    
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    // jump variables
    [Header("JumpStuff")]
    private float jumpTimeCounter ;
    public float jumpTime = 0.35f;
    private bool isJumpting;
    private int extraJumps ;
    public int extraJumpValue = 1;
    public float JumpFallWaitTime = 0.5f;
 
    Animator animator;
    Transform cameraT;

    public bool canMove;

    public CharacterController controller;
    // AnimationController AnimController;

    void Start()
    {
        canMove = true;
        extraJumps = extraJumpValue;
        //  AnimController.GetComponent<AnimationController>();
        //  animator = GetComponentInChildren<Animator>();
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        //  animator.SetBool("Aim", false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    void Update()
    {

        if (canMove == false)
            return;

        if (controller.isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }
        // 2= x 0 = a 3 = y 1 = b
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            animator.SetTrigger("Punch");
        }
        // input For Movement
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button2);

        Move(inputDir, running);


        // Jump Input Functionality
      //  if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0) && extraJumps > 0)
     //   {
       //     Jump();
        //    extraJumps -= 1;
        //    Debug.Log
      //  }
         if (Input.GetKeyDown(KeyCode.Space)  || Input.GetKeyDown(KeyCode.Joystick1Button0) ) 
        {
            if(controller.isGrounded == true)
            Jump();
        }
        // Jump Input Functionality Increase Jump Hight On Hold
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0))
        {
            JumpIncreaseHight();
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            isJumpting = false;
        }
        // animator
        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
        // set name of forward animation here

        animator.SetFloat("Forward", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

    }

    void Move(Vector2 inputDir, bool running)
    {
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg  + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
            // NOTE check if .. lag remove and set a wait in animation to time stop jumping
            // AnimController.StopJumpingAnimation();
            animator.SetBool("Jumping", false);
           // animator.SetBool("JumpFall", false);
        }

    }

    void Jump()
    {
        // removed to add double jump
       if (controller.isGrounded)
        {
            isJumpting = true;
            jumpTimeCounter = jumpTime;
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
            Debug.Log("HitJumpbutton");
            animator.SetBool("Jumping", true);
           // Invoke("JumpFall", JumpFallWaitTime);
       }
    }

    public void JumpFall()
    {
        animator.SetBool("JumpFall", true);
    }
    void JumpIncreaseHight()
    {
        if (isJumpting == true)
        {
            if (jumpTimeCounter > 0)
            {
                jumpTimeCounter -= Time.deltaTime;
                float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
                velocityY = jumpVelocity;
              //  Debug.Log("HitJumpbutton");
              //  animator.SetBool("Jumping", true);
            }
            else
            {
                isJumpting = false;
            }
        }
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }

        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }

 

    
}
