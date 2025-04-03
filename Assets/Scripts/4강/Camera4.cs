using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera4 : MonoBehaviour
{
    //쿼터니언 실습 4
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -4);
    public float smoothSpeed = 5f;

    // Update is called once per frame
    void LateUpdate()
    {
        Test4();
    }

    void Test4() //LateUpdete로 카메라가 플레이어를 따라서 느리게 회전
    {
        Vector3 desiredPosition = target.position + Quaternion.Euler(0, target.eulerAngles.y, 0) * offset;
        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
