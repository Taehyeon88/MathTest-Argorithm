using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task7_1 : MonoBehaviour
{
    [Header("TestSetting")]
    [SerializeField] float enemyHP = 100f;             //플레이어체력
    [SerializeField] float poissonLambda = 2f;         //푸아송분포용_람다
    [SerializeField] int maxHitsPerTurn = 5;           //최대공격가능횟수
    [SerializeField] float hitRate = 0.6f;             //공격성공확률
    [SerializeField] float meanDamage = 20f;           //평균데미지
    [SerializeField] float stdDevDamage = 5f;          //데미지표준편차
    [SerializeField] float critChance = 0.2f;          //치명타확률
    [SerializeField] float critDamageRate = 2f;        //치명타데미지확률

    [Header("UI Setting")]
    [SerializeField] TextMeshProUGUI totalTurn_Text;
    [SerializeField] TextMeshProUGUI totalEnemy_Text;
    [SerializeField] TextMeshProUGUI enemyKill_Text;
    [SerializeField] TextMeshProUGUI totalHitRate_Text;
    [SerializeField] TextMeshProUGUI totalCriRate_Text;
    [SerializeField] TextMeshProUGUI maxDamage_Text;
    [SerializeField] TextMeshProUGUI minDamage_Text;
    //획득한 아이템
    [SerializeField] TextMeshProUGUI totalItems_Text;

    //내부변수
    private int totalEnemyCount = 0;
    private int enemykillCount = 0;
    private int totalHitCount = 0;
    private int totalTryHitCount = 0;
    private int totalCriCount = 0;
    private float maxDamage = 0;
    private float minDamage = 15;

    private Dictionary<string, int> totalItems = new Dictionary<string, int>();

    int turn = 0;
    bool rareItemObtained = false;
    private float rareItemRate = 0.2f; 

    string[] rewards = { "Gold", "Weapon", "Armor", "Potion" };

    private void Start()
    {
        StartSimulation();
    }

    void SetUIText()
    {
        totalTurn_Text.text = $"totalTurn: {turn}";
        totalEnemy_Text.text = $"totalEnemy: {totalEnemyCount}";
        enemyKill_Text.text = $"enemyKill: {enemykillCount}";
        totalHitRate_Text.text = $"totalHitRate: {(float)totalHitCount / totalTryHitCount * 100}%";
        totalCriRate_Text.text = $"totalCriRate: {(float)totalCriCount / totalHitCount * 100}%";
        maxDamage_Text.text = $"maxDamage: {maxDamage}";
        minDamage_Text.text = $"minDamage: {minDamage}";

        totalItems_Text.text = string.Join(", ", totalItems);

        Debug.Log($"totalTurn: {turn}");
        Debug.Log($"totalEnemy: {totalEnemyCount}");
        Debug.Log($"enemyKill: {enemykillCount}");
        Debug.Log($"totalHitRate: {(float)totalHitCount / totalTryHitCount * 100}%");
        Debug.Log($"totalCriRate: {(float)totalCriCount / totalHitCount * 100}%");
        Debug.Log($"maxDamage: {maxDamage}");
        Debug.Log($"minDamage: {minDamage}");
        Debug.Log(string.Join(", ", totalItems));
    }


    public void StartSimulation()
    {
        ResetValues();
        while (!rareItemObtained)
        {
            SimulateTurn();
            turn++;
            rareItemRate += 0.1f;
        }
        SetUIText();
    }

    void SimulateTurn()
    {
        int enemyCount = PoissonDistribution(poissonLambda);  //등장하는 적의 수(랜덤)

        totalEnemyCount += enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            float damageSum = 0;
            int hitCount = BinomialDistribution(maxHitsPerTurn, hitRate);   //공격횟수(랜덤)

            totalTryHitCount += maxHitsPerTurn;
            totalHitCount += hitCount;
            for (int j = 0; j < hitCount; j++)
            {
                float hitDamage = NormalDistribution(meanDamage, stdDevDamage);  //공격력(랜덤)
                hitDamage = Mathf.Floor(hitDamage * 10f) / 10f;
                maxDamage = Mathf.Max(maxDamage, hitDamage);
                minDamage = Mathf.Min(minDamage, hitDamage);

                if (BernoulliDistribution(critChance))  //치.확
                {
                    totalCriCount++;
                    hitDamage *= critDamageRate;
                }
                damageSum += hitDamage;
            }

            if (enemyHP - damageSum <= 0)
            {
                enemykillCount++;
                DropItem();
            }
        }
    }

    void DropItem()
    {
        string dropedItem = rewards[Random.Range(0, rewards.Length)];

        if (dropedItem != null)
        {
            if (dropedItem == "Weapon" || dropedItem == "Armor")
            {
                if (Random.value <= rareItemRate)
                {
                    Debug.Log("된다.");
                    rareItemObtained = true;
                }
            }

            if (totalItems.ContainsKey(dropedItem))
            {
                totalItems[dropedItem]++;
            }
            else
            {
                totalItems.Add(dropedItem, 1);
            }
        }
    }

    void ResetValues()
    {
        totalEnemyCount = 0;
        enemykillCount = 0;
        totalHitCount = 0;
        totalTryHitCount = 0;
        totalCriCount = 0;
        maxDamage = 0;
        minDamage = 15;
        rareItemObtained = false;
        turn = 0;
        totalItems.Clear();
        rareItemRate = 0.2f;
    }

    // --- 분포 샘플 함수들 ---
    int PoissonDistribution(float lambda)  //푸아송분포
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

    int BinomialDistribution(int n, float p)  //이항분포
    {
        int success = 0;
        for (int i = 0; i < n; i++)
            if (Random.value < p) success++;
        return success;
    }

    float NormalDistribution(float mean, float stdDev)  //정규분포
    {
        float u1 = Random.value;
        float u2 = Random.value;
        float z = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Cos(2.0f * Mathf.PI * u2);
        return mean + stdDev * z;
    }

    bool BernoulliDistribution(float p)
    {
        return Random.value < p;
    }
}
