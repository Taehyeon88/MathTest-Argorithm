using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Task9_2 : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletDPS = 2f;
    [SerializeField] private float bulletForce = 10f;
    [SerializeField] private Material lineRedererMaterial;

    private float timer = 0;
    private LineRenderer lr;
    private Coroutine currentCoroutine;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargettingMode();
    }
    void SetLineRenderer()
    {
        if (lr == null)
        {
            lr = GetComponent<LineRenderer>();
            lr.widthMultiplier = 0.05f;
            lr.material = lineRedererMaterial;
        }
        lr.positionCount = 2;
    }

    void ResetLineRenderer()
    {
        lr.positionCount = 0;
    }

    void PlayLineRenderer(GameObject target)
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, target.transform.position);
    }

    IEnumerator OnTargettingRay(GameObject target)
    {
        Debug.Log("����ȴ�");
        SetLineRenderer();
        while (true)
        {
            PlayLineRenderer(target);
            timer += Time.deltaTime;
            Debug.Log(timer);
            Debug.Log(target.transform.position);
            if (timer >= bulletDPS)
            {
                Vector3 dir = (target.transform.position - transform.position).normalized;
                GameObject temp = Instantiate(bulletPrefab, transform.position + transform.forward * 0.2f, Quaternion.identity);
                Rigidbody rb = temp.GetComponent<Rigidbody>();
                rb.AddForce(dir * bulletForce, ForceMode.Impulse);

                timer = 0;
            }
            yield return null;
        }
    }

    void CheckTargettingMode()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, 50f, enemyLayer))
            {
                GameObject target = hit.collider.gameObject;
                if (currentCoroutine == null)
                {
                    currentCoroutine = StartCoroutine(OnTargettingRay(target));
                }
                else
                {
                    Debug.Log("�ڷ�ƾ�� �̹� �����մϴ�");
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;
                ResetLineRenderer();
            }
        }
    }
}
