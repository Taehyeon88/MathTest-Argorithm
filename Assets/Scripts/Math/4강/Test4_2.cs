using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test4_2 : MonoBehaviour
{
    //쿼터니언 실습 1
    public float rotationSpeed = 90f;
    //쿼터니언 실습 4
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -4);
    public float smoothSpeed = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Test();
        //Test2();
        //Test3();
        Test4();
    }

    void Test() //Y축을 기준으로 1초마다 45도 회전
    {
        transform.Rotate(0, 45 * Time.deltaTime, 0);
    }

    void Test2()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Rotate(0, input * rotationSpeed * Time.deltaTime, 0);
    }

    void Test3()
    {
        float input = Input.GetAxis("Horizontal");
        if (Mathf.Abs(input) > 0.01f)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, input * rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = deltaRotation * transform.rotation;
        }
    }

    void Test4() //LateUpdete로 카메라가 플레이어를 따라서 느리게 회전
    {
        Vector3 desiredPosition = target.position + Quaternion.Euler(0, target.eulerAngles.y, 0) * offset;
        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
