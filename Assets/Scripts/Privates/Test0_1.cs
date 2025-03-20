using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Test0_1 : MonoBehaviour
{
    public string[] babbling = new string[5]
    {
        "aya", "yee", "u", "maa", "wyeoo"
    };
    public int solution(string[] babbling)
    {
        int answer = 0;

        for (int i = 0; i < babbling.Length; i++)
        {
            string temp = babbling[i];
            int count = 0;
            string str = "";
            int test = 0;
            //Debug.Log(temp);
            while (count < 4)
            {
                switch (count % 4)
                {
                    case 0: str = "aya"; break;
                    case 1: str = "ye"; break;
                    case 2: str = "woo"; break;
                    case 3: str = "ma"; break;
                }
                if (temp.Contains(str))
                {
                    int index = temp.IndexOf(str);
                    temp = temp.Remove(index, str.Length);
                    temp = temp.Insert(index, ",");
                    Debug.Log($"{i}번째 {str}이다");
                    Debug.Log(temp);
                }
                count++;
                if (count >= 4)
                {
                    test++;
                }
                Debug.Log(test);
            }
            answer += test;
        }
        Debug.Log("정답은 :" + answer);
        return answer;
    }

    void Start()
    {
        solution(babbling);
    }
}
