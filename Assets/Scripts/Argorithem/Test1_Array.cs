using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test1_Array : MonoBehaviour
{
    void Start()
    {
        ArrayTest();
        //ListTest();
    }

    void ArrayTest()
    {
        string[] inventory = new string[5];
        inventory[0] = "Potion";
        inventory[1] = "Sword";
        string target = Array.Find(inventory, item => item == "Potion");
        if (target != null) Debug.Log($"{target}을 찾았습니다.");
        else Debug.Log("Potion을 찾을 수 없습니다.");

        Debug.Log(inventory[0]);
        Debug.Log(inventory[1]);
        Debug.Log(inventory[2]);
    }

    void ListTest()
    {
        List<string> listInventory = new List<string>();
        listInventory.Add("Potion");
        listInventory.Add("Sword");

        Debug.Log(listInventory[0]);
        Debug.Log(listInventory[1]);
        Debug.Log(listInventory[2]);
    }
}
