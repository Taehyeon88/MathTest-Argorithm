using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test10_1 : MonoBehaviour
{
    public Transform target;
    float speed = 2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraSlerpDetail();
    }

    void CameraSlerp()
    {
        Quaternion lookRot = Quaternion.LookRotation(target.position - transform.position);
        float t = 1f - Mathf.Exp(-speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, t);
    }

    void CameraSlerpDetail()
    {
        Quaternion lookRot = Quaternion.LookRotation(target.position - transform.position);
        float t = 1f - Mathf.Exp(-speed * Time.deltaTime);
        transform.rotation = ManualSlerp(transform.rotation, lookRot, t);
    }

    Quaternion ManualSlerp(Quaternion from, Quaternion to, float t)
    {
        float dot = Quaternion.Dot(from, to);

        if (dot < 0f)
        {
            to = new Quaternion(-to.x, -to.y, -to.z, -to.w);
            dot = -dot;
        }

        float theta = Mathf.Acos(dot);
        float sinTheta = Mathf.Sin(theta);

        float ratioA = Mathf.Sin((1f - t) * theta) / sinTheta;
        float ratioB = Mathf.Sin(t * theta) / sinTheta;

        Quaternion result = new Quaternion(
            ratioA * from.x + ratioB * to.x,
            ratioA * from.y + ratioB * to.y,
            ratioA * from.z + ratioB * to.z,
            ratioA * from.w + ratioB * to.w
        );
        if (1f - dot < 0.01f)
        {
            Quaternion lerp = new Quaternion(
                Mathf.Lerp(from.x, to.x, t),
                Mathf.Lerp(from.y, to.y, t),
                Mathf.Lerp(from.z, to.z, t),
                Mathf.Lerp(from.w, to.w, t)
            );
            return lerp.normalized;
        }
        return result.normalized;
    }
}
