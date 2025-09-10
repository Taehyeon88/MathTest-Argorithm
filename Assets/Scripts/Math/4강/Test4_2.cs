using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test4_2 : MonoBehaviour
{
    //���ʹϾ� �ǽ� 1
    public float rotationSpeed = 90f;
    //���ʹϾ� �ǽ� 4
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

    void Test() //Y���� �������� 1�ʸ��� 45�� ȸ��
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

    void Test4() //LateUpdete�� ī�޶� �÷��̾ ���� ������ ȸ��
    {
        Vector3 desiredPosition = target.position + Quaternion.Euler(0, target.eulerAngles.y, 0) * offset;
        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
