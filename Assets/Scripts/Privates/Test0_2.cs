using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test0_2 : MonoBehaviour
{
    public string[] strings = new string[1];
    public int n = 0;
    public string[] solution(string[] strings, int n)
    {
        int count = strings.Length;
        string[] answer = new string[count];
        return answer;
    }
    void Start()
    {
        Debug.Log(string.Join(", ", solution(strings, n)));
        
    }

}
