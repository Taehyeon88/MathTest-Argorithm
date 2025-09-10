using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PhysicsTest : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float forcePower = 10f;
    [SerializeField] private float speed;
    [SerializeField] private bool onImpulse;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //if (onImpulse)
        //{
        //    rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
        //    //�ӵ� = V(��, ����) / M(����)
        //}
        //else
        //{
        //    rb.AddForce(Vector3.forward * 10f);
        //    //rb.AddForce(Vector3.forward * 10f, ForceMode.Force);
        //    //�ӵ� = V(��, ����) / M(����) / Time.deltaTime(1s / 1�� �� �� ������ ��)
        //}
    }

    private void FixedUpdate()  //1�ʴ� 50������ (����)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.forward * forcePower, ForceMode.Force);
            Debug.Log("�ԷµǾ���");
        }
    }
    void Update()
    {
        speed = rb.velocity.magnitude;
        Debug.Log($"{speed}, {forcePower}"); //speed = 0.4�� ����, m = 1, �� �����Ӽ� = 100;
        
    }
}
