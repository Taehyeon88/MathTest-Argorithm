using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test5_StackSample : MonoBehaviour
{
    private void Start()
    {
        StackSample();
    }

    private void Update()
    {
        UpdateStackMove();
    }

    private void StackSample()
    {
        Stack<int> stack = new Stack<int>();

        //Push: 데이터 넣기
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);

        Debug.Log("=============== Stack 1 =================");
        foreach (int num in stack)
            Debug.Log(num);
        Debug.Log("=========================================");

        //Peek: 맨 위 데이터 확인
        Debug.Log("Peek: " + stack.Peek());

        //Pop: 맨 위 데이터 꺼내기
        Debug.Log("Pop: " + stack.Pop());
        Debug.Log("Pop: " + stack.Pop());

        Debug.Log("남은 데이터 수: " + stack.Count);

        Debug.Log("=============== Stack 2 =================");
        foreach (int num in stack)
            Debug.Log(num);
        Debug.Log("=========================================");
    }

    public float speed = 5f;
    private Stack<Vector3> moveHistory = new Stack<Vector3>();

    private void UpdateStackMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //이동 입력이 있을 때
        if (x != 0 || y != 0)
        {
            moveHistory.Push(transform.position);

            Vector3 move = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
            transform.position += move;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (moveHistory.Count > 0)
            {
                transform.position = moveHistory.Pop();
            }
        }
    }
}
