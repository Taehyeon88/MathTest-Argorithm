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
        //    //속도 = V(힘, 방향) / M(질량)
        //}
        //else
        //{
        //    rb.AddForce(Vector3.forward * 10f);
        //    //rb.AddForce(Vector3.forward * 10f, ForceMode.Force);
        //    //속도 = V(힘, 방향) / M(질량) / Time.deltaTime(1s / 1초 당 총 프레임 수)
        //}
    }

    private void FixedUpdate()  //1초당 50프레임 (고정)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.forward * forcePower, ForceMode.Force);
            Debug.Log("입력되었다");
        }
    }
    void Update()
    {
        speed = rb.velocity.magnitude;
        Debug.Log($"{speed}, {forcePower}"); //speed = 0.4씩 증가, m = 1, 총 프레임수 = 100;
        
    }
}
