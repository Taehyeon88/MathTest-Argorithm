using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test10_2 : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 1f, -2f);

    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] float maxSpeed = 100f;

    Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        SmoothCamera();
    }

    void SmoothCamera()
    {
        if(!target) return;

        Vector3 desired = target.position + target.TransformDirection(offset);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desired,
            ref velocity,
            smoothTime,
            maxSpeed,
            Time.deltaTime
            );
    }
}
