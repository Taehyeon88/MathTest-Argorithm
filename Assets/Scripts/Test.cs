using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed;
    Vector3 targetPos = Vector3.zero;

    void Start()
    {
        //Translate(new Vector3(0, 10, 10));
        //transform.position = new Vector3(0, 4, 5);
    }

    // Update is called once per frame
    void Update()
    {
        //Test1();
        //Test2();
        //Test3();
        Test4();
    }


    void Test1()  //백터의 크기가 정규화되어 있지 않아서 대각선의 속도가 빠름
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;
        transform.position += move;
    }

    void Test2()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveX, moveY, 0).normalized;
        Vector3 move = direction * speed * Time.deltaTime;
        transform.position += move;
    }

    void Test3()
    {
        //int A = 1;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float z = 0;

        float magnutude = Mathf.Sqrt(x * x +  y * y + 0)== 0 ? 1 : Mathf.Sqrt(x * x + y * y + 0);
        Vector3 normalized = new Vector3(x / magnutude, y / magnutude, z / magnutude);
        Vector3 move = normalized * speed * Time.deltaTime;
        transform.position += move;
    }

    void Test4()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetPos = hit.point;
                targetPos.y = 0.5f;
            }
        }

        Vector3 currentPos = transform.position;
        Vector3 direction = (targetPos - currentPos).normalized;
        float distance = Mathf.Sqrt((targetPos.x - currentPos.x) * (targetPos.x - currentPos.x) + (targetPos.z - currentPos.z) * (targetPos.z - currentPos.z));
        if (distance > 0.1f)                                              //백터에는 방향백터와 위치백터가 있다. 이 두 백터는 엄연히 다르고 두점 사이의 거리는 위치백터에서 구한다.
        {                                                                 //방향백터는 방향(x,y,z)과 크기(= 방향백터의 거리)를 가진다.
            Vector3 move = direction * speed * Time.deltaTime;            //이 두 백터는 엄연히 다르고 두점 사이의 거리는 위치백터에서 구한다.
            transform.position += move;
        }
    }
}
