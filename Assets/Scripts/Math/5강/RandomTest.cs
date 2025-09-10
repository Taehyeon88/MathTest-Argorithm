using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTest : MonoBehaviour
{
    int[] counts = new int[6];
    public int trials = 100;
    void Start()
    {
        //Test();
        //SeedRandomTest1();
        //SeedRandomTest2();
        SaveSeed();
        //DiceSimulator();
    }

    void Test()
    {
        //Unity Random(�յ� ����)
        float chance = Random.value; //0 ~ 1 float
        int dice = Random.Range(1, 7); // 1~6 int

        //System.Random
        System.Random sysRand = new System.Random();
        int number = sysRand.Next(1, 7); // 1~6(int)

        Debug.Log("Unity Random (Random.value): " + chance);
        Debug.Log("Unity Random (Random.Range): " + dice);
        Debug.Log("System Random (Next): " + number); // 1~6 (int)
    }

    void SeedRandomTest1()
    {
        System.Random rnd = new System.Random(4321);

        for (int i = 0; i < 4; i++)
        {
            Debug.Log(rnd.Next(1, 5));
        }
    }

    void SeedRandomTest2()
    {
        Random.InitState(1234);  //Unity ���� �õ� ����
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(Random.Range(1, 7));
        }
    }
    //System.Random�� ���� �յ� ������(LCG, Linear Congruential Generator)
    //UnityEngine.Random(Random.InitState)�� PCG (Permuted Congruential Generator)

    //PCG�� �� �� �ֱ⸦ ������ �־ LCG���� �� �����ϱ� ����� ������ ������ �� �־�.
    //PCG�� LCG���� �� ������ ������ ������ ������.
    //LCG�� ���� ������ �巯�� �� �ִ� �ݸ�, PCG�� ���� �����ϰ� ���̴� ����� ������.

    //System.Random�� Random.InitState�� ���� �õ尪�� �������� �ʴ´�.
    void SaveSeed()
    {
        List<int> generatedNumbers = new List<int>();
        int seed = Random.Range(1, 1000000);             // ������ ������ ���� �õ� ����
        Debug.Log("Generated Seed: " + seed);

        // UnityEngine.Random�� �õ� ����
        Random.InitState(seed);

        for (int i = 0; i < 5; i++)
        {
            int num = Random.Range(1, 8);               // 1~7 ���� ����
            generatedNumbers.Add(num);
            Debug.Log("Generated Number: " + num);
        }

        // ������ �õ带 ����Ͽ� System.Random������ ���� ���� ���� Ȯ��
        Debug.Log("Reproduced Numbers:");
        Random.InitState(seed);

        for (int i = 0; i < 5; i++)
        {
            Debug.Log("Reproduced Number: " + Random.Range(1, 8));
        }
    }

    void DiceSimulator()
    {
        for (int i = 0; i < trials; i++)
        {
            int result = Random.Range(1, 7);
            counts[result - 1]++;
        }

        for (int i = 0; i < counts.Length; i++)
        {
            float percent = (float)counts[i] / trials * 100f;
            Debug.Log($"{i + 1}: {counts[i]}ȸ ({percent:F2}%)");
        }
    }
}
