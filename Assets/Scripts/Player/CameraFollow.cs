using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float damping = 1;
    [SerializeField]
    private float lookAheadFactor = 3;
    [SerializeField]
    private float lookAheadReturnSpeed = 0.5f;
    [SerializeField]
    private float lookAheadMoveThreshold = 0.1f;

    // private variables
    float offsetZ;
    Vector3 lastTargetPosition;
    Vector3 currentVelocity;
    Vector3 lookAheadPos;

    // Use this for initialization
    private void Start()
    {
        // if target not set, then set it to the player
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (target == null)
        {
            Debug.LogError("Target not set on Camera2DFollow.");
            return;
        }
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        transform.parent = null;


    }

    // Update is called once per frame
    private void Update()
    {
        if (target == null)
            return;

        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - lastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

        transform.position = newPos;

        lastTargetPosition = target.position;
    }
}

