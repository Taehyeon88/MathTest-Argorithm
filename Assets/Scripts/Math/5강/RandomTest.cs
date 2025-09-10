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
        //Unity Random(균등 분포)
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
        Random.InitState(1234);  //Unity 난수 시드 고정
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(Random.Range(1, 7));
        }
    }
    //System.Random은 선형 합동 생성기(LCG, Linear Congruential Generator)
    //UnityEngine.Random(Random.InitState)은 PCG (Permuted Congruential Generator)

    //PCG는 더 긴 주기를 가지고 있어서 LCG보다 더 예측하기 어려운 난수를 생성할 수 있어.
    //PCG는 LCG보다 더 균일한 분포의 난수를 제공해.
    //LCG는 쉽게 패턴이 드러날 수 있는 반면, PCG는 더욱 랜덤하게 보이는 결과를 제공해.

    //System.Random와 Random.InitState는 같은 시드값을 공유하지 않는다.
    void SaveSeed()
    {
        List<int> generatedNumbers = new List<int>();
        int seed = Random.Range(1, 1000000);             // 적절한 범위의 랜덤 시드 생성
        Debug.Log("Generated Seed: " + seed);

        // UnityEngine.Random에 시드 적용
        Random.InitState(seed);

        for (int i = 0; i < 5; i++)
        {
            int num = Random.Range(1, 8);               // 1~7 난수 생성
            generatedNumbers.Add(num);
            Debug.Log("Generated Number: " + num);
        }

        // 동일한 시드를 사용하여 System.Random에서도 같은 난수 생성 확인
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
            Debug.Log($"{i + 1}: {counts[i]}회 ({percent:F2}%)");
        }
    }
}
