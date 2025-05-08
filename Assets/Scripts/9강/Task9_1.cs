using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task9_1 : MonoBehaviour
{
    //위치 보간(수동)
    public Transform startPos;
    public Transform endPos;

    [SerializeField] private float duration = 2.0f;
    [SerializeField] private float t = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //PassiveLerp();
        //Vector3Lerp();
        PingPongLerp();
        //LerpUnClamped();
    }

    void PassiveLerp()
    {
        if (t < 1f)
        {
            t += Time.deltaTime / duration;

            Vector3 a = startPos.position;
            Vector3 b = endPos.position;
            Vector3 p = (1f - t) * a + t * b;

            transform.position = p;
        }

        //t += Time.deltaTime / duration;

        //Vector3 a = startPos.position;
        //Vector3 b = endPos.position;
        //Vector3 p = (1f - t) * a + t * b;

        //transform.position = p;
    }

    void Vector3Lerp()
    {
        t += Time.deltaTime / duration;

        transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
    }

    void PingPongLerp()
    {
        t = Mathf.PingPong(Time.time / duration, 1f);

        transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
    }

    void LerpUnClamped()
    {
        t += Time.deltaTime / duration;

        transform.position = Vector3.LerpUnclamped(startPos.position, endPos.position, t);
    }
}
