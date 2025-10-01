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

    private Vector3 recordPlayerPos;  //��ϵǴ� �÷��̾� ��ġ
    private Vector3 recordShadowPos;  //��ϵǴ� �׸��� ��ġ

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
            if (x != 0 || y != 0)  //�̵� �� �̵� ���
            {
                recordMoveHistory.Enqueue(transform.position);
                returnMoveHistory.Push(transform.position);

                Vector3 move = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
                transform.position += move;

                if (currentCor == null)
                {
                    //Debug.Log("�׸��� ����");
                    currentCor = StartCoroutine(C_StartShadowMove());
                }
            }

            if (Input.GetKey(KeyCode.R)) //�ǵ��ư��� ����
            {
                if (recordMoveHistory.Count > 0 && returnMoveHistory.Count > 0)
                {
                    isReturnning = true;
                    gameObject.GetComponent<Renderer>().material.color = Color.blue;
                }
            }
        }

        if (!isReturnning) return;  //�ǵ��ư��� ����----------------------

        //���� �׸����� ��ġ���� �޴´�. (����)
        //�ش� �׸����� ��ġ�������� �ǵ��ư��� ó��

        StopShadowMove();  //�׸��� �̵� ����

        Vector3 targetPos = Vector3.zero;
        if(recordMoveHistory.Count > 0)
            targetPos = recordMoveHistory.Peek();

        if (returnMoveHistory.Count > 0)
        {
            returnSpeedRate = returnSpeedRate == 0 ? 1 : Mathf.Abs(returnSpeedRate);  //����ó��
            for (int i = 0; i < returnSpeedRate; i++)
            {
                transform.position = returnMoveHistory.Pop();

                if (transform.position == targetPos) //�ǵ��ư��� ����
                {
                    isReturnning = false;
                    Destroy(shadowObj);
                    recordMoveHistory.Clear(); //�׸��ڰ� ���� ��� �ʱ�ȭ
                    gameObject.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
        else
        {
            isReturnning = false;
            Destroy(shadowObj);
            recordMoveHistory.Clear(); //�׸��ڰ� ���� ��� �ʱ�ȭ
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }

    }

    private IEnumerator C_StartShadowMove()
    {
        //n���Ŀ� �ѹ� ������
        yield return new WaitForSeconds(2f);

        isShadowMoving = true;
        shadowObj = Instantiate(shadowPrf, recordMoveHistory.Peek(), Quaternion.identity);  //�׸��� ����
    }

    private void ShadowMove()
    {
        if (!isShadowMoving) return;

        if (recordMoveHistory.Count > 0)
        {
            shadowObj.transform.position = recordMoveHistory.Dequeue();
        }
        else  //�׸��� ������� ����
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
