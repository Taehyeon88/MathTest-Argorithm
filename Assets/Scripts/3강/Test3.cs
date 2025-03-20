using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    public Transform player;
    public float viewAngle = 30f;

    public Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        Test5();
        Test6();
    }

    private void Test5() //내적을 사용한 메서드
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;

        float dot = Vector3.Dot(forward, toPlayer);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (angle < viewAngle / 2)
        {
            Debug.Log("플레이어가 시야 안에 있음!");
        }
    }

    private void Test6()
    {
        Vector3 playerForward = transform.forward;
        Vector3 toTarget = (target.position - transform.position).normalized;

        if (IsLeft(playerForward, toTarget, Vector3.up))
        {
            Debug.Log("타겟이 플레이어 기준 왼쪽에 있음");
        }
        else
        {
            Debug.Log("타겟이 플레이어 기준 오른쪽에 있음");
        }
    }

    bool IsLeft(Vector3 forward, Vector3 targetDirection, Vector3 up)
    {
        Vector3 cross = Vector3.Cross(forward, targetDirection);
        return Vector3.Dot(cross, up) > 0; //양수면 왼쪽, 음수면 오른쪽
    }
}
