using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test4_1 : MonoBehaviour
{
    public Transform target; //목표 위치
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        VectorCross();
    }

    Vector3 CrossProduct(Vector3 a, Vector3 b)
    {
        return new Vector3(
            a.y * b.z - a.z * b.y,
            a.z * b.x - a.x * b.z,
            a.x * b.y - a.y * b.x
            );
    }

    void VectorCross()
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
    //외적의 특징 : 
    bool IsLeft(Vector3 forward, Vector3 targetDirection, Vector3 up)
    {
        Vector3 cross = CrossProduct(forward, targetDirection);
        return Vector3.Dot(cross, up) < 0;
    }
    
}
