using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task100_2 : MonoBehaviour
{
    bool isMove = false;
    Camera mCamera;

    Queue<Vector3> movePos = new Queue<Vector3>();
    bool isMoving = false;
    bool isDone = false;
    void Start()
    {
        mCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (movePos.Count > 0) isMoving = true;

        if (isMoving && !isDone)
        {
            isDone = true;
            StartCoroutine(MoveToPosition());
        }

        CheckMove();
    }
    void CheckMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(mCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (!isMoving)
                {
                    movePos.Enqueue(hit.point + new Vector3(0, 0.5f, 0));
                }
            }
        }
    }

    IEnumerator MoveToPosition()
    {
        Vector3 pos;
        while (movePos.TryDequeue(out pos))
        {
            float distance = 1;
            while (distance > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 2);
                Debug.Log("이동한다");
                distance = Vector3.Distance(pos, transform.position);
                yield return null;
            }
        }
        isDone = false;
        isMoving = false;
    }
}
