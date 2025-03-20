using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public float degrees = 45f;
    //public int valueToRadians = 3;
    public int radianValue;

    public float angle = 30f;
    public float speed = 5f;
    void Start()
    {
        //Test();
        //Test3();
    }

    // Update is called once per frame
    void Update()
    {
        //Test3();
        Test4();
    }

    private void Test()
    {
        float radians = degrees * Mathf.Deg2Rad;
        Debug.Log("45도 -> 라디안 : " + radians);

        //float radianValue = Mathf.PI / valueToRadians; //60도
        float degreeValue = radianValue * Mathf.Rad2Deg;
        Debug.Log("파이/3 라디안 -> 도 변환 : " + degreeValue);
    }

    private void Test3()   //라디안의 0 ~ 2라디안의 범위 내에서 오브젝트가 상.하.좌.우 모든 방향으로 움직일 수 있다.
    {
        float speed = 5f;
        //float angle = 30f;
        float radians = angle * Mathf.Deg2Rad;

        Vector3 direction = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians));
        transform.position += direction * speed * Time.deltaTime;
    }

    private void Test4()
    {
        float radians = angle * Mathf.Deg2Rad;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            angle += 15f;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            angle -= 15f;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            speed += 1f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            speed -= 1f;
        }
        Vector3 direction = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians));
        transform.position += direction * speed * Time.deltaTime;
    }
}
