using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Tesk14 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject skillPrefab;
    public LayerMask groundLayer;

    private Coroutine currentCoroutine;

    void Update()
    {
        CheckMove();
        UseSkill();
    }

    private void CheckMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 50f, groundLayer))
            {

                if (currentCoroutine == null)
                {
                    currentCoroutine = StartCoroutine(Move(hit.point));
                }
                else
                {
                    StopCoroutine(currentCoroutine);
                    currentCoroutine = null;

                    currentCoroutine = StartCoroutine(Move(hit.point));
                }
            }
        }
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        Vector3 currentPos = transform.position;
        Vector3 dir = (targetPos - currentPos).normalized;

        Rigidbody rb = GetComponent<Rigidbody>();

        while (true)
        {
            rb.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);

            float dis = Vector3.Distance(transform.position,  targetPos);
            if (dis < 0.5f)
            {
                break;
            }
            yield return null;
        }
    }

    private void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Instantiate(skillPrefab, transform.position, Quaternion.identity);
        }
    }
}
