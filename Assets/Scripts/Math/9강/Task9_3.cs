using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task9_3 : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(DestroyBullet());
        Debug.Log("»ý¼ºµÊ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
