using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task_2 : MonoBehaviour
{
    public TextMeshProUGUI description;
    private int attackCount = 0;
    private bool isCritical = false;
    private int successCount = 0;
    private float percent;

    public void Attack()
    {
        attackCount++;
        int result = Random.Range(1, 11);

        float preCarculate = (float)(successCount + 1) / (attackCount + 1) * 100f;
        float preCarculate2 = (float)successCount / (attackCount + 1) * 100f;

        if (percent < 10 && preCarculate < 10) result = 1;
        else if (percent > 10 && preCarculate2 > 10) result = 0;

        Debug.Log($"{result}");
        if (result == 1)
        {
            successCount++;
            isCritical = true;
        }
        else isCritical = false;

        percent = (float)successCount / attackCount * 100f;
        description.text = $"IsCritical: {isCritical} / AttackCount: {attackCount} / percent: {percent:F2}%";
    }
}
