using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager12 : MonoBehaviour
{
    [SerializeField] private Slider powerSlider;
    [SerializeField] private TextMeshProUGUI turnText;

    [SerializeField] private TextMeshProUGUI player1Text;
    [SerializeField] private TextMeshProUGUI player2Text;
    void Start()
    {
        powerSlider.value = PhysicsTask.force  / 10f;

        powerSlider.onValueChanged.AddListener(SetPowerValue);
    }


    public void SetPowerValue(float power)
    {
        PhysicsTask.force = power * 10;
    }

    public void SetPlayerName(int id)
    {
        turnText.text = $"Player{id}";
    }

    public void SetWaitting()
    {
        turnText.text = "IsWatting...";
    }

    public void SetPlayerScoll(int id, int scoll)
    {
        switch (id)
        {
            case 1:
                player1Text.text = scoll.ToString();
            break;

            case 2:
                player2Text.text = scoll.ToString();
            break;
        }
    }
}
