using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetTrans;
    public Vector3 offset;
    private Vector3 vel;
    public float smoothTime;

    public void Start()
    {
        targetTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void FixedUpdate()
    {
        Vector3 desiredPos = targetTrans.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref vel, smoothTime);
    }
}
