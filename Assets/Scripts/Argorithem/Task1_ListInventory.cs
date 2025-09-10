using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1_ListInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void AddItem(string itemName)
    {
        items.Add(new Item(itemName, 1));
        Debug.Log(itemName + "추가됨 (현재 개수: " + items.Count + ")");
    }

    public void RemoveItem(string itemName)
    {
        Item target = items.Find(x => x.itemName == itemName);
        if (target != null)
        {
            items.Remove(target);
            Debug.Log(itemName + " 삭제됨 (현재 개수: " + items.Count + ")");
        }
        else
        {
            Debug.Log(itemName + " 아이템이 없습니다.");
        }
    }

    public void PrintInventory()
    {
        Debug.Log("=== 리스트 인벤토리 상태 ===");
        if (items.Count == 0)
        {
            Debug.Log("인벤토리가 비어 있습니다");
            return;
        }

        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log(i + "번 슬롯: " + items[i].itemName + " x" + items[i].quantity);
        }
    }

    public void RemoveAllItem(string itemName)
    {

        if (items.Find(item => item.itemName == itemName) == null)
        {
            Debug.Log($"이미 {itemName}이 존재하지 않습니다.");
            return;
        }

        List<int> indexs = new List<int>();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == itemName)
            {
                indexs.Add(i);   //해당 이름을 가지고 있는 모든 아이템인덱스 저장
            }
        }

        indexs.Sort((a,b) => b.CompareTo(a));  //인덱스들을 내림차순으로 정렬
        for (int i = 0; i < indexs.Count; i++)
        {
            items.RemoveAt(indexs[i]);   //제일 마지막 인덱스 순으로 지워짐
        }
        Debug.Log($"{itemName}을 모두 버렸습니다.");
    }
}
