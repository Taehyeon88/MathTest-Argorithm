using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test4 : MonoBehaviour
{
    //첫번째 실습
    public Transform player;
    public float viewAngle = 60f;
    //두번째 실습
    public float viewAngle2 = 60f;
    public float viewDistance = 5f;
    //감옥게임용 추가변수들
    public float chaseSpeed = 5f;
    public float rotateSpeed = 30f;
    private bool findedPlayer = false;
    void Start()
    {
        
    }
    
    void Update()
    {
        VectorDot_1();
        LoodAround();
        if(findedPlayer) ChasePlayer();
    }

    void VectorDot_1()
    {
        Vector3 a = (player.position - transform.position).normalized;
        Vector3 b = transform.forward;
        float distance = Vector3.Distance(player.position, transform.position);

        float dot = a.x * b.x + a.y* b.y + a.z * b.z;
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (angle < viewAngle / 2 && distance <= viewDistance)
        {
            ChasePlayer();
            findedPlayer = true;
            Debug.Log("플레이어가 시야 안에 있음!");
        }
    }

    void ChasePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * chaseSpeed * Time.deltaTime;
    }

    void LoodAround()
    {
        transform.rotation *= Quaternion.Euler(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 forward = transform.forward * viewDistance;

        //왼쪽 시야 결계
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * forward;
        //오른쪽 시야 결계
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * forward;

        Gizmos.DrawRay(transform.position, leftBoundary);
        Gizmos.DrawRay(transform.position, rightBoundary);

        //캐릭터 앞쪽 방향
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            findedPlayer = false;
            transform.position = transform.position;
            Transform spawnPoint = GameObject.Find("spawnPoint").transform;
            player.position = new Vector3(spawnPoint.position.x, player.transform.position.y, spawnPoint.position.z);
        }
    }
}
