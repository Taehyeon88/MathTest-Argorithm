using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test5_QueueSample : MonoBehaviour
{
    void Start()
    {
        QueueSample();
    }

    void Update()
    {
        UpdateQueueMove();
    }

    private void QueueSample()
    {
        Queue<string> queue = new Queue<string>();

        //Enqueue: 데이터 넣기
        queue.Enqueue("첫 번째");
        queue.Enqueue("두 번째");
        queue.Enqueue("세 번째");

        Debug.Log("=============== Queue 1 =================");
        foreach (string item in queue)
            Debug.Log(item);
        Debug.Log("=========================================");

        //Peek: 맨 위 데이터 확인
        Debug.Log("Peek: " + queue.Peek());

        //Dequeue: 맨 위 데이터 꺼내기
        Debug.Log("Pop: " + queue.Dequeue());
        Debug.Log("Pop: " + queue.Dequeue());

        Debug.Log("남은 데이터 수: " + queue.Count);

        Debug.Log("=============== Queue 2 =================");
        foreach (string item in queue)
            Debug.Log(item);
        Debug.Log("=========================================");
    }

    public float speed = 5f;
    private Queue<Vector3> moveHistory = new Queue<Vector3>();
    private bool isMoving;

    private void UpdateQueueMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (!isMoving)
        {
            //이동 입력이 있을 때
            if (x != 0 || y != 0)
            {
                moveHistory.Enqueue(transform.position);

                Vector3 move = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
                transform.position += move;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (moveHistory.Count > 0)
                {
                    isMoving = true;
                }
            }
        }
        else
        {
            if (moveHistory.Count > 0)
            {
                Vector3 targetPos = moveHistory.Dequeue();
                transform.position = targetPos;
            }
            else
            {
                isMoving = false;
            }
        }
    }
}
