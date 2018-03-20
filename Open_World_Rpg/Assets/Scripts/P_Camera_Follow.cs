using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Camera_Follow : MonoBehaviour {
    #region Variables
    public float CameraMoveSpeed = 120f;
    public GameObject CameraFollowObj;
    Vector3 Followpos;
    public float Clampangle = 80f;
    public float InputSensitivity = 150f;
    public GameObject Camera;
    public GameObject Player;
    public float CamDistXtoPlayer;
    public float CamDistytoPlayer;
    public float CamDistZtoPlayer;
    public float MouseX;
    public float MouseY;
    public float FinalInputX;
    public float FinalInputZ;
    public float SmoothX;
    public float SmoothY;
    private float RotY = 0f;
    private float RotX = 0f;
    #endregion

    public bool LockCursor;//Shift To InputManager
    public bool InvertY;//Shift To InputManager

    // Use this for initialization
    void Start () {
        Vector3 Rot = transform.localRotation.eulerAngles;
        RotY = Rot.y;
        RotX = Rot.x;
        if (LockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        float InputX = Input.GetAxis("RightStickHorizontal");//Change to RightStickHorizontal if req
        float InputZ = Input.GetAxis("RightStickVertical");//Change to RightStickVertical if req
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y") * (InvertY ? -1 : 1);
        FinalInputX = InputX + MouseX;
        FinalInputZ = InputZ + MouseY;

        RotY += FinalInputX * InputSensitivity * Time.deltaTime;
        RotX += FinalInputZ * InputSensitivity * Time.deltaTime;

        RotX = Mathf.Clamp(RotX, -Clampangle, Clampangle);

        Quaternion LocalRot = Quaternion.Euler(RotX, RotY, 0);
        transform.rotation = LocalRot;
    }

    private void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        //set target Object to follow
        Transform target = CameraFollowObj.transform;

        //move towards the gameobject
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
