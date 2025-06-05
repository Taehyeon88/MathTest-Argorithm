using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraManager12 : MonoBehaviour
{
    public Transform table;

    private float distance = 10f;
    private float speed = 100f;

    private float angle_h = 0f;
    private float angle_v = 260f;
    private const float MaxAngle = 350f;
    private const float MinAngle = 230f;

    private Vector3 offset = Vector3.forward;
    void Start()
    {
        offset = new Vector3(0, 1, 1).normalized;
    }

    void Update()
    {
        RotateCamera();
        ZoomInOut();
    }

    void RotateCamera()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        angle_h += horizontal * speed * Time.deltaTime;
        angle_v += vertical * speed * Time.deltaTime;
        angle_v = Mathf.Clamp(angle_v, MinAngle, MaxAngle);

        Quaternion quaternion = Quaternion.Euler(angle_v, - angle_h, 0);

        //offset = Quaternion.Euler(0, 0, angle_v) * offset;  //중간에 위아래 회전 추가

        Vector3 dir = quaternion * offset * distance;  //쿼터니언 * 백터(순서가 바뀌면 안됨)

        transform.position = table.position + dir;

        transform.LookAt(table.position);
    }

    void ZoomInOut()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        distance += scroll * speed * 10f * Time.deltaTime;
    }
}
