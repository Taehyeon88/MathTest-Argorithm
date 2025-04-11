using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task6_2 : MonoBehaviour
{
    [SerializeField] GameObject textPrefab;
    void Start()
    {
        FindObjectOfType<Task6_1>().attackDelegate += UpdateDamages;
        FindObjectOfType<Task6_1>().attackDelegate += OnDamageMotion;
    }

    public void UpdateDamages(float attackValue)
    {

        GameObject textObj = Instantiate(textPrefab, transform);
        textObj.transform.position = transform.position + Vector3.up * 1.1f;

        TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();
        if (temp != null)
        {
            temp.text = attackValue.ToString();
            StartCoroutine(Action(textObj));
        }
    }

    private IEnumerator Action(GameObject textObj)
    {
        float duration = 1.5f;
        float timer = 0;

        Vector3 currentPos = textObj.transform.position;
        TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float percent = timer / duration;

            textObj.transform.position = currentPos + Vector3.up * percent * 50f;

            if (temp != null)
            {
                temp.alpha = 1 - percent;
            }

            yield return null;
        }
        Destroy(textObj);
    }

    public void OnDamageMotion(float attackValue)
    {
        Debug.Log("¾ÆÇÁ´Ù!!");
    }
}
