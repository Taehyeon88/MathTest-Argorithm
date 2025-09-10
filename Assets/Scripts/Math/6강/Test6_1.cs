using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test6_1 : MonoBehaviour
{
    int n = 1000;
    float minValue = 0;
    float maxValue = 1;

    void Start()
    {
        Debug.Log(GenerateGaussian(5, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StandardDeviation()
    {
        float[] samples = new float[n];
        for (int i = 0; i < n; i++)
        {
            samples[i] = Random.Range(minValue, maxValue);
        }
        float mean = samples.Average();
        float sumOfSquares = samples.Sum(x => Mathf.Pow(x - mean, 2));
        float stdDev = Mathf.Sqrt(sumOfSquares / n);

        Debug.Log($"���: {mean}, ǥ������: {stdDev}");
    }

    float GenerateGaussian(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value; //0���� ū ����
        float u2 = 1.0f - Random.value; //0���� ū ����

        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                            Mathf.Sin(2.0f * Mathf.PI * u2);

        return mean + stdDev * randStdNormal; //���ϴ� ��հ� ǥ�������� ��ȯ
    }
}
