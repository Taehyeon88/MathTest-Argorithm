using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task11_2 : MonoBehaviour  //카메라 시스템
{
    public Vector3 offset;
    public float direction = 1.5f;
    public float rotateSpeed = 1.0f;
    public GameObject player;
    public GameObject target;

    bool isResetting = false;
    float timer = 1f;
    void Start()
    {
        SetCamera();
    }

    private void Update()
    {
        if (isResetting) RealResetCamera();
    }

    private void SetCamera()
    {
        transform.position = player.transform.position + player.transform.TransformDirection(offset) * direction;
        transform.LookAt(player.transform.position);
    }

    public void RotateCamera(GameObject _target) //공격을 시작할 시, 회전하는 카메라함수
    {
        target = _target;

        float r = 0.8f;

        Vector3 lookPos = (1f - r) * target.transform.position + r * transform.position;

        Quaternion lookRot = Quaternion.LookRotation(lookPos - transform.position);
        float t = 1f - Mathf.Exp(-rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, t);
    }

    public void ResetCamera()
    {
        isResetting = true;
    }

    private void RealResetCamera()
    {

        Quaternion lookRot = Quaternion.LookRotation(player.transform.position - transform.position);
        float t = 1f - Mathf.Exp(-rotateSpeed * 3f * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, t);

        timer -= Time.deltaTime / 1.5f;
        if (timer < 0.001)
        {
            transform.LookAt(player.transform.position);
            isResetting = false;
            timer = 1f;
        }
    }
}
