using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test7_1 : MonoBehaviour
{
    void Start()
    {
        //Ǫ�Ƽۺ���
        for (int i = 0; i < 10; i++)
        {
            int count = PoissonDistribution(3f);
            Debug.Log($"Minute {i + 1}: {count} events");
        }

        //���׺���
        int result = BinomialDistribution(10, 0.3f);
        Debug.Log($"Successes out of 10 trials: {result}");

        //�踣���̺���
        bool result2 = BernoulliDistribution(0.2f);
        Debug.Log($"Trial result: {(result2 ? "Sucess" : "Fail")}");

        //���Ϻ���
        int result3 = GeometricDistribution(0.1f);
        Debug.Log($"Tries until first sucess: {result3}");

        //�յ����
        int result4 = UniformDistribution(0, 4);
        Debug.Log($"Uniform Sample: {result4}");
    }

    //Ǫ�Ƽۺ���
    int PoissonDistribution(float lambda)
    {
        int k = 0;
        float p = 1f;
        float L = Mathf.Exp(-lambda);
        while (p > L)
        {
            k++;
            p *= Random.value;
        }
        return k - 1;
    }

    //���׺���
    int BinomialDistribution(int trials, float chance)
    {
        int successes = 0;
        for (int i = 0; i <= trials; i++)
        {
            if(Random.value < chance)
                successes++;
        }
        return successes;
    }

    //�踣���̺���
    bool BernoulliDistribution(float p)
    {
        return Random.value < p;
    }

    //���Ϻ���
    int GeometricDistribution(float p)
    {
        int tries = 1;
        while (Random.value >= p)
        {
            tries++;
        }
        return tries;
    }
    //�յ����
    int UniformDistribution(int minInclusive, int maxExclusive)
    {
        return Random.Range(minInclusive, maxExclusive);
    }
}
