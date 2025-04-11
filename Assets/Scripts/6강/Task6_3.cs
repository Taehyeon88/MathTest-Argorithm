using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task6_3 : MonoBehaviour
{
    [SerializeField] Task6_1 player;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI criticalText;
    [SerializeField] TextMeshProUGUI attackRangeText;
    [SerializeField] TextMeshProUGUI dpaText;
    [SerializeField] TextMeshProUGUI level;

    //내부 변수
    private float attackValue = 0;
    private float criticalRate = 0;
    private float cumulatedDamage = 0;
    private int attackCount = 0;
    void Start()
    {
        player.attackDelegate += AddDamage;
    }

    void Update()
    {
        UpdateInformation();
    }

    void UpdateInformation()
    {
        attackValue = player.attackValue;

        if (player.currentWeapon != null)
        {
            criticalRate = player.currentWeapon.criticalRate;
            criticalText.text = $"Critical Hit Chance: {player.currentWeapon.p_critical:F2}% /n" +
                 $"Critical Damage: {criticalRate * attackValue}";

            attackRangeText.text = $"AttackRange: {attackValue - 3 * attackValue * criticalRate} ~ {attackValue - 3 * attackValue * criticalRate}";

        }

        level.text = $"Level: {player.level}";

        attackText.text = $"Player AttackVaule: {attackValue}";

        dpaText.text = $"Demage Per Attack: {cumulatedDamage / attackCount}";
        //Debug.Log($"{cumulatedDamage}, {attackCount}, {cumulatedDamage / attackCount * 100}");
    }

    void AddDamage(float attackDamage)
    {
        attackCount++;
        cumulatedDamage += attackDamage;
    }
}
