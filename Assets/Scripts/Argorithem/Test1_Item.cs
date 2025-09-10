using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public int quantity;

    public Item(string name, int qua = 1)
    {
        itemName = name;
        quantity = qua;
    }
}