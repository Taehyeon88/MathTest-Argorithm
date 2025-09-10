using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task_1 : MonoBehaviour
{
    public TextMeshProUGUI diceNumber;

    int diceNum = 6;

    public void DiceSimulator()
    {
       int result = Random.Range(1, diceNum + 1);
        diceNumber.text = result.ToString();
    }
}
