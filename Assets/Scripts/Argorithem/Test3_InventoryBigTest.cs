using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Test3_InventoryBigTest : MonoBehaviour
{
    List<Item> items = new List<Item>();
    private System.Random rand = new System.Random();

    private void Start()
    {
        //10���� ������ ����
        for (int i = 0; i < 1000000; i++)
        {
            string name = $"Item_{i:D6}";
            int qty = rand.Next(1, 100);
            items.Add(new Item(name, qty));
        }

        //���� Ž�� �׽�Ʈ
        string target = "Item_456724";
        Stopwatch sw = Stopwatch.StartNew();
        Item foundLinear = FindItemByLinear(target);
        sw.Stop();
        UnityEngine.Debug.Log($"[���� Ž��] {target} ����: {foundLinear?.quantity}, �ð�: {sw.ElapsedMilliseconds}ms");

        //���� Ž�� �غ� (����)
        items.Sort((a, b) => a.itemName.CompareTo(b.itemName));
        //���� Ž�� �׽�Ʈ
        sw.Restart();
        Item foundBinary = FindItemByBinary(target);
        sw.Stop();
        UnityEngine.Debug.Log($"[���� Ž��] {target} ����: {foundBinary?.quantity}, �ð�: {sw.ElapsedMilliseconds}ms");
    }

    //���� Ž��
    public Item FindItemByLinear(string _itemName)
    {
        foreach (var item in items)
        {
            if (item.itemName == _itemName)
                return item;
        }
        return null;
    }

    //���� Ž��
    public Item FindItemByBinary(string targetName)
    {
        int left = 0;
        int right = items.Count - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            int compare = items[mid].itemName.CompareTo(targetName);

            if (compare == 0)
            {
                return items[mid];
            }
            else if (compare < 0)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return null; //�� ã��
    }
}
