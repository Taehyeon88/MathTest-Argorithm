using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Task11_4 : MonoBehaviour  //UIΩ√Ω∫≈€
{
    [SerializeField] private TextMeshProUGUI coolTime;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShowTimer()
    {
        coolTime.text = $"coolTime: {Mathf.Ceil(Task11_3.currentSkillCoolTime)}";
    }

    public void ResetTimer()
    {
        coolTime.text = string.Empty;
    }
}
