using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MoveInput : MonoBehaviour {
    [Header("Movement")]
    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDir;
    public bool blockRotPlayer;
    public float desiredRotSpeed;
    public float Speed;
    public float allowPlayerRot;
    public bool KeepCharGrounded;
    public bool isGrounded;
    private float verticalVel;
    private Vector3 moveVector;

    [Header("References")]
    public Animator Anim;
    public Camera Cam;
    public CharacterController controller;


    [Header("Sprinting")]
    public KeyCode Sprint_Key;
    public bool Sprint;

    private Vector3 rightFootPosition, leftFootPosition, leftFootIKPosition, rightFootIKPosition;
    private Quaternion leftFootIKRot, rightFootIKRot;
    private float lastpelvisPosY, lastRightFootPosY, lastLeftFootPosY;
    [Header("Feet IK")]
    public bool enableFeetIK = true;
    [Range(0, 2)][SerializeField] private float heightFromGroundRaycast = 1.14f;
    [Range(0, 2)] [SerializeField] private float raycastDownDist = 1.5f;
    [SerializeField] private LayerMask environmentLayer;
    [SerializeField] private float pelvisOffset = 0;
    [Range(0, 1)] [SerializeField] private float pelvisUpAndDownSpeed = 0.28f;
    [Range(0, 1)] [SerializeField] private float feetToIKPosSpeed = 0.5f;

    public string leftFootAnimVariableName = "LeftFootCurve";
    public string rightFootAnimVariableName = "RightFootCurve";

    public bool useProIK = false;
    public bool showSolverDebug = true;


    #region ScriptRefs
    public P_AttackInput PlayerAttackInput;
    #endregion





    // Use this for initialization
    void Awake () {
        SetRefs();

    }

    // Update is called once per frame
    void Update () {
        #region Sprinting
        if (Input.GetKey(Sprint_Key))
        {
            Sprint = true;
            SprintFunc();
        }
        else
        {
            Sprint = false;
            Anim.SetBool("Sprint", false);
        }
        #endregion



        InputMagnitude();

        #region Player Grounding
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
        #endregion

        #region FeetIK

        #endregion
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
        else if(blockRotPlayer == true && !PlayerAttackInput.Attacking)
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
        PlayerAttackInput = GetComponent<P_AttackInput>();
    }

    void SprintFunc()
    {
        Anim.SetBool("Sprint", true);
    }
}
