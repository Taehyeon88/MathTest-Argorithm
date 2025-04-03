using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4 : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotateSpeed = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float input_x = Input.GetAxis("Horizontal");
        float input_y = Input.GetAxis("Vertical");

        float currentRot_y = transform.rotation.y;
        Vector3 currentForward = transform.forward;
        transform.rotation *= Quaternion.Euler(0,input_x * rotateSpeed * Time.deltaTime, 0);
        transform.position += currentForward * input_y * moveSpeed * Time.deltaTime;
    }
}
