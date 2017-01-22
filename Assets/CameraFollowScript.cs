using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    [SerializeField]
    private Vector3 distanceVector;
    [SerializeField]
    private Transform target;
    private Vector3 currentVector;

    public void Awake()
    {
        currentVector = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(distanceVector);
        transform.LookAt(target);
        transform.position = target.position + currentVector;
    }
}
