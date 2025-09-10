using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2 : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private float angle = 0;

    private float timer = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        timer += Time.deltaTime;
        angle = 360 * (timer / 10) - 90;
        Debug.Log(angle);

        if (timer > 10)
        {
            timer = 0;
        }
        float radians = angle * Mathf.Deg2Rad;
        Vector3 direction2 = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
        transform.position += direction2 * speed * Time.deltaTime;
    }
}
