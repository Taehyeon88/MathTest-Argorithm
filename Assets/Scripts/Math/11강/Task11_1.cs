using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Task11_1 : MonoBehaviour  //총알 스크립트
{
    public Transform p3;
    public Vector3 p0;
    public GameObject target;

    [Header("Random Ranges")]
    public float p1Radius = 2f;
    public float p2Radius = 2f;
    public float p1Height = 3f;
    public float p2Height = 3f;

    [HideInInspector] public Vector3 p1;
    [HideInInspector] public Vector3 p2;

    List<Vector3> points;
    float time = 0f;

    private bool isOneTime = false;

    void Update()
    {
        if (p0 != Vector3.zero && p3 != null && target != null)
        {
            if (!isOneTime)
            {
                GenerateRandomControlPoints();
                isOneTime = true;
            }

            time += Time.deltaTime / 0.8f;
            points = new List<Vector3> { p0, p1, p2, target.transform.position };
            transform.position = DeCasteljau(points, time);

            if (time > 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    void GenerateRandomControlPoints()
    {
        p1Height = UnityEngine.Random.Range(0f, 5f);
        Vector2 rand1 = Random.insideUnitCircle * p1Radius;
        p1 = p0 + new Vector3(rand1.x, 0f, rand1.y);
        p1.y += p1Height;


        p2Height = UnityEngine.Random.Range(0f, 5f);
        Vector2 rand2 = Random.insideUnitCircle * p2Radius;
        p2 = p3.position + new Vector3(rand2.x, 0f, rand2.y);
        p2.y += p2Height;
    }

    Vector3 DeCasteljau(List<Vector3> pt, float t)
    {
        while (pt.Count > 1)
        {
            int last = pt.Count - 1;
            var next = new List<Vector3>(last);
            for (int i = 0; i < last; i++)
            {
                next.Add(Vector3.Lerp(pt[i], pt[i + 1], t));
            }
            pt = next;
        }

        return pt[0];
    }
}
