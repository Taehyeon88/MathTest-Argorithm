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
        //10만개 아이템 생성
        for (int i = 0; i < 1000000; i++)
        {
            string name = $"Item_{i:D6}";
            int qty = rand.Next(1, 100);
            items.Add(new Item(name, qty));
        }

        //선형 탐색 테스트
        string target = "Item_456724";
        Stopwatch sw = Stopwatch.StartNew();
        Item foundLinear = FindItemByLinear(target);
        sw.Stop();
        UnityEngine.Debug.Log($"[선형 탐색] {target} 개수: {foundLinear?.quantity}, 시간: {sw.ElapsedMilliseconds}ms");

        //이진 탐색 준비 (정렬)
        items.Sort((a, b) => a.itemName.CompareTo(b.itemName));
        //이진 탐색 테스트
        sw.Restart();
        Item foundBinary = FindItemByBinary(target);
        sw.Stop();
        UnityEngine.Debug.Log($"[이진 탐색] {target} 개수: {foundBinary?.quantity}, 시간: {sw.ElapsedMilliseconds}ms");
    }

    //선형 탐색
    public Item FindItemByLinear(string _itemName)
    {
        foreach (var item in items)
        {
            if (item.itemName == _itemName)
                return item;
        }
        return null;
    }

    //이진 탐색
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
        return null; //못 찾음
    }
}
