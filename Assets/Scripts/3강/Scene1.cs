using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Scene1 : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private int angle = 0;

    private float currentTime = 0;
    private float timer = 0;
    void Start()
    {
        
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        //float round = (target.position - transform.position)
        //angle++;
        if (angle >= 360)
        {
            angle = 0;
        }
        float radians = angle * Mathf.Deg2Rad;
        Vector3 direction2 = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians));
        transform.position += direction2 * speed * Time.deltaTime;
    }
}
