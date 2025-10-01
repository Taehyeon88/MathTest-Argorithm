using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Task5_EcoMove : MonoBehaviour
{
    public float speed = 5f;
    public int returnSpeedRate = 2;
    public GameObject shadowPrf;
    public Stack<Vector3> returnMoveHistory { get; private set; }
    public Queue<Vector3> recordMoveHistory { get; private set; }
    public bool isReturnning { get; private set; }
    private bool isShadowMoving;
    private GameObject shadowObj;

    private Coroutine currentCor;

    private Vector3 recordPlayerPos;  //기록되는 플레이어 위치
    private Vector3 recordShadowPos;  //기록되는 그림자 위치

    private void Start()
    {
        returnMoveHistory = new Stack<Vector3>();
        recordMoveHistory = new Queue<Vector3>();
    }
    private void Update()
    {
        EcoMove();
        ShadowMove();
    }

    private void EcoMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (!isReturnning)
        {
            if (x != 0 || y != 0)  //이동 및 이동 기록
            {
                recordMoveHistory.Enqueue(transform.position);
                returnMoveHistory.Push(transform.position);

                Vector3 move = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
                transform.position += move;

                if (currentCor == null)
                {
                    //Debug.Log("그림자 생성");
                    currentCor = StartCoroutine(C_StartShadowMove());
                }
            }

            if (Input.GetKey(KeyCode.R)) //되돌아가기 시작
            {
                if (recordMoveHistory.Count > 0 && returnMoveHistory.Count > 0)
                {
                    isReturnning = true;
                    gameObject.GetComponent<Renderer>().material.color = Color.blue;
                }
            }
        }

        if (!isReturnning) return;  //되돌아가기 시작----------------------

        //현재 그림자의 위치값을 받는다. (저장)
        //해당 그림자의 위치값까지만 되돌아가기 처리

        StopShadowMove();  //그림자 이동 정지

        Vector3 targetPos = Vector3.zero;
        if(recordMoveHistory.Count > 0)
            targetPos = recordMoveHistory.Peek();

        if (returnMoveHistory.Count > 0)
        {
            returnSpeedRate = returnSpeedRate == 0 ? 1 : Mathf.Abs(returnSpeedRate);  //예외처리
            for (int i = 0; i < returnSpeedRate; i++)
            {
                transform.position = returnMoveHistory.Pop();

                if (transform.position == targetPos) //되돌아가기 종료
                {
                    isReturnning = false;
                    Destroy(shadowObj);
                    recordMoveHistory.Clear(); //그림자가 추적 기록 초기화
                    gameObject.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
        else
        {
            isReturnning = false;
            Destroy(shadowObj);
            recordMoveHistory.Clear(); //그림자가 추적 기록 초기화
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }

    }

    private IEnumerator C_StartShadowMove()
    {
        //n초후에 한번 실행함
        yield return new WaitForSeconds(2f);

        isShadowMoving = true;
        shadowObj = Instantiate(shadowPrf, recordMoveHistory.Peek(), Quaternion.identity);  //그림자 생성
    }

    private void ShadowMove()
    {
        if (!isShadowMoving) return;

        if (recordMoveHistory.Count > 0)
        {
            shadowObj.transform.position = recordMoveHistory.Dequeue();
        }
        else  //그림자 따라오기 종료
        {
            Destroy(shadowObj);
            isShadowMoving = false;
            StopShadowMove();
        }
    }

    private void StopShadowMove()
    {
        if (currentCor != null)
        {
            StopCoroutine(currentCor);
            currentCor = null;
        }
        isShadowMoving = false;
    }
}
