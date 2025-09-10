using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Scene1 : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rate = 10f;
    private float angle = 0;
    private Vector3 startPos;
    private float timer = 0;

    private void Start()
    {
        startPos = transform.localPosition;
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        timer += Time.deltaTime;
        angle = 360 * (timer / rate) -90;

        if (timer > rate)
        {
            timer = 0;
            transform.localPosition = startPos; //달이 괴도를 벗어난 것 방지용
        }
        float radians = angle * Mathf.Deg2Rad;
        Vector3 direction2 = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians));
        transform.position += direction2 * speed * Time.deltaTime;
    }
}
