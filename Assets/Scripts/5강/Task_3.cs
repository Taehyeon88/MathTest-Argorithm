using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task_3 : MonoBehaviour
{
    public TextMeshProUGUI expressText;
    List<string> cards = new List<string>();

    public void DrawCard()
    {
        int num = Random.Range(1, 11);

        if (num == 1) cards.Add("S");
        else if (num <= 3) cards.Add("A");
        else if (num <= 6) cards.Add("B");
        else if(num <= 10) cards.Add("C");
        SetCard();
    }

    public void DrawTenCard()
    {
        for (int i = 0; i < 10; i++)
        {
            int num = Random.Range(1, 11);
            
            if (i == 9)
            {
                int temp = Random.Range(1, 4);
                if (temp == 1) cards.Add("S");
                else if (temp <= 3) cards.Add("A");
                SetCard();
            }
            else
            {
                if (num == 1) cards.Add("S");
                else if (num <= 3) cards.Add("A");
                else if (num <= 6) cards.Add("B");
                else if (num <= 10) cards.Add("C");
            }
        }
    }

    void SetCard()
    {
        expressText.text = $"Drawed Card: {string.Join(",", cards)}";
        cards.Clear();
    }
}
