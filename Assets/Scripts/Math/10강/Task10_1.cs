using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task10_1 : MonoBehaviour
{
    public Transform target;

    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] float maxSmoothSpeed = 100f;
    [SerializeField] LayerMask enemyLayer;

    private float rotateSpeed = 2f;
    private Vector3 velocity = Vector3.zero;
    private Coroutine curcoroutine;
    private Transform curTarget;

    private Vector3 startPoint;
    private Quaternion startRotate;

    void Start()
    {
        startPoint = transform.position;
        startRotate = transform.rotation;
    }

    void Update()
    {
        CheckSmoothCamera();
    }

    void CheckSmoothCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, enemyLayer))
            {
                target = hit.transform;

                if(target != curTarget)
                {
                    ResetCameraSmooth();
                }

                if (curcoroutine == null)
                {
                    curcoroutine = StartCoroutine(SmoothCamera());
                    curTarget = target;
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            ResetCameraSmooth();
            MoveToStartPoint();
        }
    }

    IEnumerator SmoothCamera()
    {
        if (!target) ResetCameraSmooth();

        Vector3 dir = (startPoint - target.position) / 4;

        target.GetComponent<Task10_2>().OnHit();

            while (true)
            {
                Vector3 desired = target.position + dir;

                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    desired,
                    ref velocity,
                    smoothTime,
                    maxSmoothSpeed,
                    Time.deltaTime
                    );

                CameraSlerp();
                yield return null;
            }
    }

    void CameraSlerp()
    {
        Quaternion lookRot = Quaternion.LookRotation(target.position - transform.position);
        float t = 1f - Mathf.Exp(-rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, t);
    }

    void ResetCameraSmooth()
    {
        if (curcoroutine != null)
        {
            curTarget.GetComponent<Task10_2>().OffHit();
            StopCoroutine(curcoroutine);
            curcoroutine = null;
        }
    }

    void MoveToStartPoint()
    {
        transform.position = startPoint;
        transform.rotation = startRotate;
    }
}
