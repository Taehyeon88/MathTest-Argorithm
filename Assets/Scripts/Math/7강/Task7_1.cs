using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task7_1 : MonoBehaviour
{
    [Header("TestSetting")]
    [SerializeField] float enemyHP = 100f;             //�÷��̾�ü��
    [SerializeField] float poissonLambda = 2f;         //Ǫ�Ƽۺ�����_����
    [SerializeField] int maxHitsPerTurn = 5;           //�ִ���ݰ���Ƚ��
    [SerializeField] float hitRate = 0.6f;             //���ݼ���Ȯ��
    [SerializeField] float meanDamage = 20f;           //��յ�����
    [SerializeField] float stdDevDamage = 5f;          //������ǥ������
    [SerializeField] float critChance = 0.2f;          //ġ��ŸȮ��
    [SerializeField] float critDamageRate = 2f;        //ġ��Ÿ������Ȯ��

    [Header("UI Setting")]
    [SerializeField] TextMeshProUGUI totalTurn_Text;
    [SerializeField] TextMeshProUGUI totalEnemy_Text;
    [SerializeField] TextMeshProUGUI enemyKill_Text;
    [SerializeField] TextMeshProUGUI totalHitRate_Text;
    [SerializeField] TextMeshProUGUI totalCriRate_Text;
    [SerializeField] TextMeshProUGUI maxDamage_Text;
    [SerializeField] TextMeshProUGUI minDamage_Text;
    //ȹ���� ������
    [SerializeField] TextMeshProUGUI totalItems_Text;

    //���κ���
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
        int enemyCount = PoissonDistribution(poissonLambda);  //�����ϴ� ���� ��(����)

        totalEnemyCount += enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            float damageSum = 0;
            int hitCount = BinomialDistribution(maxHitsPerTurn, hitRate);   //����Ƚ��(����)

            totalTryHitCount += maxHitsPerTurn;
            totalHitCount += hitCount;
            for (int j = 0; j < hitCount; j++)
            {
                float hitDamage = NormalDistribution(meanDamage, stdDevDamage);  //���ݷ�(����)
                hitDamage = Mathf.Floor(hitDamage * 10f) / 10f;
                maxDamage = Mathf.Max(maxDamage, hitDamage);
                minDamage = Mathf.Min(minDamage, hitDamage);

                if (BernoulliDistribution(critChance))  //ġ.Ȯ
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
                    Debug.Log("�ȴ�.");
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

    // --- ���� ���� �Լ��� ---
    int PoissonDistribution(float lambda)  //Ǫ�Ƽۺ���
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

    int BinomialDistribution(int n, float p)  //���׺���
    {
        int success = 0;
        for (int i = 0; i < n; i++)
            if (Random.value < p) success++;
        return success;
    }

    float NormalDistribution(float mean, float stdDev)  //���Ժ���
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
