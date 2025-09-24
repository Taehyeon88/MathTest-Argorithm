using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3_SearchInventory : MonoBehaviour
{
    //인벤토리 아이템 리스트
    public List<Item> items = new List<Item>();

    private void Start()
    {
        //아이템 찾기 테스트(선형 탐색)-------------------------

        ////예시 아이템 추가
        //items.Add(new Item("Sword"));
        //items.Add(new Item("Shield"));
        //items.Add(new Item("Potion"));

        //Item found = FindItemByLinear("Potion");
        //if (found != null) Debug.Log($"찾은 아이템: {found.itemName}");
        //else Debug.Log("아이템을 찾을 수 없음");

        //아이템 찾기 테스트(이진 탐색)-----------------------

        items.Add(new Item("Potion", 5));
        items.Add(new Item("High-Potion", 2));
        items.Add(new Item("Elixir", 1));
        items.Add(new Item("Sword", 1));

        //탐색 전 정렬 (이름 기준)
        items.Sort((a, b) => a.itemName.CompareTo(b.itemName));

        //탐색 테스트
        Item found2 = FindItemByBinary("Elixir");
        if (found2 != null)
            Debug.Log($"[이진 탐색] 찾은 아이템 : {found2.itemName}, 개수: {found2.quantity}");
        else Debug.Log("이진 탐색으로 아이템을 찾을 수 없습니다.");
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
