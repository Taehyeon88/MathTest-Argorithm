using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera4 : MonoBehaviour
{
    //���ʹϾ� �ǽ� 4
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -4);
    public float smoothSpeed = 5f;

    // Update is called once per frame
    void LateUpdate()
    {
        Test4();
    }

    void Test4() //LateUpdete�� ī�޶� �÷��̾ ���� ������ ȸ��
    {
        Vector3 desiredPosition = target.position + Quaternion.Euler(0, target.eulerAngles.y, 0) * offset;
        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
