using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    public Transform player;
    public float viewAngle = 30f;
    public float viewDistance = 5f;
    private Vector3 currentScale = new Vector3(1,1,1);
    private Vector3 changedScale = new Vector3(2,2,2);

    public Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        Test5();
        //Test6();
    }

    private void Test5() //������ ����� �޼���
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = player.position;
        float distance = Mathf.Sqrt(Mathf.Pow(targetPos.x - currentPos.x, 2f) + Mathf.Pow(targetPos.z - currentPos.z, 2f));

        if (distance >= viewDistance)
        {
            if (player.transform.localScale.x != 1) player.transform.localScale = currentScale;
            return;
        }
        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;

        float dot = Vector3.Dot(forward, toPlayer);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (angle < viewAngle / 2)
        {
            Debug.Log("�÷��̾ �þ� �ȿ� ����!");
            player.transform.localScale = changedScale;
        }
        else
        {
            if(player.transform.localScale.x != 1) player.transform.localScale = currentScale;
        }
    }
    
    private void Test6()  //������ ����� �޼���
    {
        Vector3 playerForward = transform.forward;
        Vector3 toTarget = (target.position - transform.position).normalized;

        if (IsLeft(playerForward, toTarget, Vector3.up))
        {
            Debug.Log("Ÿ���� �÷��̾� ���� ���ʿ� ����");
        }
        else
        {
            Debug.Log("Ÿ���� �÷��̾� ���� �����ʿ� ����");
        }
    }

    bool IsLeft(Vector3 forward, Vector3 targetDirection, Vector3 up)
    {
        Vector3 cross = Vector3.Cross(forward, targetDirection);
        return Vector3.Dot(cross, up) > 0; //����� ����, ������ ������
    }
}
