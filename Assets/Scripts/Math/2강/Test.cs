using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed;
    Vector3 targetPos;
    

    void Start()
    {
        targetPos = transform.position;
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


    void Test1()  //������ ũ�Ⱑ ����ȭ�Ǿ� ���� �ʾƼ� �밢���� �ӵ��� ����
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
        if (distance > 0.1f)                                              //���Ϳ��� ������Ϳ� ��ġ���Ͱ� �ִ�. �� �� ���ʹ� ������ �ٸ��� ���� ������ �Ÿ��� ��ġ���Ϳ��� ���Ѵ�.
        {                                                                 //������ʹ� ����(x,y,z)�� ũ��(= ��������� �Ÿ�)�� ������.
            Vector3 move = direction * speed * Time.deltaTime;            //�� �� ���ʹ� ������ �ٸ��� ���� ������ �Ÿ��� ��ġ���Ϳ��� ���Ѵ�.
            transform.position += move;
        }
    }
}
