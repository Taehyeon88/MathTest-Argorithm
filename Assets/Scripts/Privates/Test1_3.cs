using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1_3 : MonoBehaviour
{
    public int solution(int number, int limit, int power)
    {
        int answer = 0;
        int[] odds = new int[number + 1];
        for (int i = 1; i <= number; i++)
        {
            int temp = i;                     //현재수를 임시로 저장해서 나누어지는 수
            int num = 1;                      //약수의 개수를 받을 수
            int num2 = 1;
            int count = 0;
            while (temp > 1)
            {
                if (temp % 2 == 0)
                {
                    num++;
                    temp = temp / 2;
                }
                else
                {
                    if (odds[count] != 0)
                    {
                        if (temp % odds[count] == 0)
                        {
                            num2++;
                            temp = temp / odds[count];
                        }
                        else
                        {
                            num *= num2;
                            num2 = 1;
                            count++;
                        }
                    }
                    else
                    {
                        num *= 2;
                        odds[count] = temp;
                        //Debug.Log($"temp : {i}, 새로운 소수 : {temp}");
                        break;
                    }
                }
            }
            //Debug.Log($"temp : {i}, 약수의 개수 : {num}");
            if (num > limit) num = power;
            answer += num;
        }

        return answer;
    }
    void Start()
    {
        Debug.Log(solution(10, 3, 2));
    }
}
