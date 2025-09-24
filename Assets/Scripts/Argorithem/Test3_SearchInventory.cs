using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3_SearchInventory : MonoBehaviour
{
    //�κ��丮 ������ ����Ʈ
    public List<Item> items = new List<Item>();

    private void Start()
    {
        //������ ã�� �׽�Ʈ(���� Ž��)-------------------------

        ////���� ������ �߰�
        //items.Add(new Item("Sword"));
        //items.Add(new Item("Shield"));
        //items.Add(new Item("Potion"));

        //Item found = FindItemByLinear("Potion");
        //if (found != null) Debug.Log($"ã�� ������: {found.itemName}");
        //else Debug.Log("�������� ã�� �� ����");

        //������ ã�� �׽�Ʈ(���� Ž��)-----------------------

        items.Add(new Item("Potion", 5));
        items.Add(new Item("High-Potion", 2));
        items.Add(new Item("Elixir", 1));
        items.Add(new Item("Sword", 1));

        //Ž�� �� ���� (�̸� ����)
        items.Sort((a, b) => a.itemName.CompareTo(b.itemName));

        //Ž�� �׽�Ʈ
        Item found2 = FindItemByBinary("Elixir");
        if (found2 != null)
            Debug.Log($"[���� Ž��] ã�� ������ : {found2.itemName}, ����: {found2.quantity}");
        else Debug.Log("���� Ž������ �������� ã�� �� �����ϴ�.");
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
