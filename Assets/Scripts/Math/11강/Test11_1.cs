using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test11_1 : MonoBehaviour
{
    [Header("Bezier_class2")]
    public Transform point0;
    public Transform point1;
    public Transform point2;

    [Header("Bezier_class3")]
    public Transform point_0;
    public Transform point_1;
    public Transform point_2;
    public Transform point_3;

    [Header("Bezier_class4")]
    public List<Transform> points = new List<Transform>();
    private List<Vector3> pointPos = new List<Vector3>();

    float timeValue = 0f;

    void Awake()
    {
        foreach (var pt in points)
        {
            if (pt != null)
                pointPos.Add(pt.position);
        }
    }

    void Update()
    {
        Bezier();
    }

    void Bezier()
    {
        timeValue += Time.deltaTime / 2f;
        //transform.position = GetPointOnBezierCurve_2(point0.position, point1.position, point2.position, timeValue);

        //transform.position = GetPointOnBezierCurve_3(point_0.position, point_1.position, point_2.position, point_3.position, timeValue);
        transform.position = FourPointBezier(point_0.position, point_1.position, point_2.position, point_3.position, timeValue);

        //transform.position = DeCasteljau(pointPos, timeValue);
    }

    Vector3 GetPointOnBezierCurve_2(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 ab = Vector3.Lerp(a, b, t);

        return ab;
    }
    Vector3 GetPointOnBezierCurve_3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 abc = Vector3.Lerp(ab, bc, t);

        return abc;
    }

    Vector3 DeCasteljau(List<Vector3> p, float t)
    {
        while (p.Count > 1)
        {
            int last = p.Count - 1;  // ������ ���� �ε���

            var next = new List<Vector3>(last);
            for(int i = 0; i < last; i++)
                next.Add(Vector3.Lerp(p[i], p[i + 1], t));
            p = next;          // �� �ܰ� ���̱�   
        }

        //count�� 1�� �Ǹ�, p[0]�� ���� ���� ��� ��ġ

        return p[0];            //���� �� ���� � ��ġ
    }

    private Vector3 FourPointBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        return Mathf.Pow(1 - t, 3) * a
            + Mathf.Pow(1 - t, 2) * 3 * t * b
            + Mathf.Pow(t, 2) * 3 * (1 - t) * c
            + Mathf.Pow(t, 3) * d;
    }
}
