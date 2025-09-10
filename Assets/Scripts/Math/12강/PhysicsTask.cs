using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTask : MonoBehaviour //�÷��� �ý���
{
    public static float force = 5;
    public LayerMask playerLayer;

    private GameObject target;
    private Vector3 attackVector;
    private bool isAttack = false;
    private Rigidbody targetRb;

    void Update()
    {
        PlayGame();
    }

    private void FixedUpdate()
    {
        if (isAttack)
        {
            targetRb.AddForce(attackVector * force, ForceMode.Impulse);
            isAttack = false;
        }
    }

    void PlayGame()  //�Է��� �޾Ƽ� ��ġ��
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, playerLayer))
            {
                string playerName = GameManager12.Instance.playerName;

                if (GameManager12.Instance.isWaiting) return;
                if (!hit.collider.gameObject.CompareTag(playerName)) return;

                target = hit.collider.gameObject;
                attackVector =  - hit.normal;
                targetRb = target.GetComponent<Rigidbody>();

                isAttack = true;
                GameManager12.Instance.StartWaiting();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (target != null && attackVector.magnitude > 0)
        {
            Debug.DrawRay(target.transform.position, - attackVector * 2f, Color.red);
        }
    }
}
