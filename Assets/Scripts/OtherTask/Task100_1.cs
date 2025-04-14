using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task100_1 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    //내부변수
    Stack<Vector3> stack = new Stack<Vector3>();
    Rigidbody rb;
    float timer = 0f;

    bool isReverting = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isReverting = true;
            StartCoroutine(RevertPosition());
        }

        if (!isReverting) PlayerMove();
        
    }

    void PlayerMove()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 direction = (moveHorizontal * transform.right + moveVertical * transform.forward).normalized;

        if (direction.magnitude > 0.1f) timer += Time.deltaTime;

        if (timer > 0.05f)
        {
            SavePosition(transform);
            timer = 0f;
        }
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    void SavePosition(Transform pos)
    {
        stack.Push(pos.position);
        Debug.Log(pos.position);
    }

    IEnumerator RevertPosition()
    {
        //Debug.Log("작동한다");
        Vector3 pos;

        while (stack.TryPop(out pos))
        {
            Debug.Log(pos + "에서 반복한다");
            yield return new WaitForSeconds(0.05f);
            transform.position = pos;
            yield return null;

        }

        //foreach (var item in stack)
        //{
        //    Debug.Log("저장된 값: " + item);
        //}

        isReverting = false;
    }
}
