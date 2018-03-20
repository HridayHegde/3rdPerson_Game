using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_CameraCollision : MonoBehaviour {
    public float minDist = 1f;
    public float maxDist = 4f;
    public float smooth = 10;
    Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;
    public float distance;    
	// Use this for initialization
	void Start () {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 desiredCamPos = transform.parent.TransformPoint(dollyDir * maxDist);

        RaycastHit hit;
        if(Physics.Linecast(transform.parent.position, desiredCamPos, out hit))
        {
            distance = Mathf.Clamp((hit.distance * 0.4f), minDist, maxDist);
        }
        else {
            distance = maxDist;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
		
	}
}
