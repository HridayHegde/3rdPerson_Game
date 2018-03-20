using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MoveInput : MonoBehaviour {
    public float InputX,InputZ;
    public Vector3 desiredMoveDir;
    public bool blockRotPlayer;
    public float desiredRotSpeed;
    public Animator Anim;
    public float Speed;
    public float allowPlayerRot;
    public Camera Cam;
    public CharacterController controller;
    public bool isGrounded;
    private float verticalVel;
    private Vector3 moveVector;

    public bool KeepCharGrounded;





	// Use this for initialization
	void Awake () {
        SetRefs();

    }

    // Update is called once per frame
    void Update () {
        InputMagnitude();

        //Ground the player
        if (KeepCharGrounded)
        {
            isGrounded = controller.isGrounded;
            if (isGrounded)
            {
                verticalVel -= 0;

            }
            else
            {
                verticalVel -= 2;
            }
            moveVector = new Vector3(0, verticalVel, 0);
            controller.Move(moveVector);
        }
    }

    void PlayerMoveAndRot()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        // var camera = Camera.main;
        var forward = Cam.transform.forward;
        var right = Cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDir = forward * InputZ + right * InputX;

        if(blockRotPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDir), desiredRotSpeed);
        }
        else if(blockRotPlayer == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward), desiredRotSpeed);
        }
    }

    void InputMagnitude()
    {
        //Calc Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Anim.SetFloat("InputZ", InputZ, 0f, Time.deltaTime * 2f);
        Anim.SetFloat("InputX", InputX, 0f, Time.deltaTime * 2f);

        //Calc the MoveMagnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;
        
        //Physically Move The Player
        if(Speed > allowPlayerRot)
        {
            Anim.SetFloat("MovementMagnitude", Speed, 0f, Time.deltaTime);
            PlayerMoveAndRot();
        }
        else if(Speed < allowPlayerRot)
        {
            Anim.SetFloat("MovementMagnitude", Speed, 0f, Time.deltaTime);
        }

    }

    void SetRefs()
    {
        Anim = GetComponent<Animator>();
        Cam = Camera.main;
        controller = GetComponent<CharacterController>();
    }
}
