using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task4_Search : MonoBehaviour
{
    [Header("UI Reference (TMP)")]
    public TMP_InputField inputItemCount;
    public TMP_InputField inputSearchCount;
    public TMP_Text resultText;

    private List<Item> items = new List<Item>();

    private long sortSteps;
    private long linearSteps;
    private long binarySteps;

    public void OnFindButton()
    {
        if (!int.TryParse(inputItemCount.text, out int itemCount)) itemCount = 10000;
        if (!int.TryParse(inputSearchCount.text, out int searchCount)) searchCount = 100;

        //아이템 초기화
        items.Clear();
        for (int i = 0; i < itemCount; i++)
        {
            items.Add(new Item($"Item_{Random.Range(0, itemCount):D5}", 1));
        }

        //탐색 대상 생성
        List<string> targets = new List<string>();
        for (int i = 0; i < searchCount; i++)
        {
            targets.Add($"Item_{Random.Range(0, itemCount):D5}");
        }

        //1. 선형 탐색
        linearSteps = 0;
        foreach (var t in targets)
        {
            linearSteps += FindItemByLinearSteps(t);
        }

        //퀵소트 + 이진 탐색
        sortSteps = 0;
        StartQuickSort(items, 0, items.Count - 1);

        binarySteps = 0;
        foreach (var t in targets)
        {
            binarySteps += FindItemByBinarySteps(t);
        }

        //결과 출력
        resultText.text =
            $"Item Count: {itemCount}\n" +
            $"Search Count: {searchCount}\n\n\n" +
            $"Linear Search Total Comparisions: {linearSteps}\n\n" +
            $"Quick Sort Comparisons: {sortSteps}\n" +
            $"Binary Search Total Comparisons: {binarySteps}\n" +
            $"Total (Sort + Binary): {sortSteps + binarySteps}";
    }

    public int FindItemByLinearSteps(string _itemName)
    {
        int step = 0;
        foreach (var item in items)
        {
            step++;
            if (item.itemName == _itemName)
                return step;
        }
        return step;
    }

    //이진 탐색
    public int FindItemByBinarySteps(string targetName)
    {
        int step = 0;
        int left = 0;
        int right = items.Count - 1;

        while (left <= right)
        {
            step++;
            int mid = (left + right) / 2;
            int compare = items[mid].itemName.CompareTo(targetName);

            if (compare == 0)
            {
                return step;
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
        return step; //못 찾음
    }

    public void StartQuickSort(List<Item> list, int low, int high)
    {
        if (low < high)
        {
            sortSteps++;
            int pivotIndex = Partition(list, low, high);

            StartQuickSort(list, low, pivotIndex - 1);
            StartQuickSort(list, pivotIndex + 1, high);
        }
    }

    private int Partition(List<Item> list, int low, int high)
    {
        Item pivot = list[high];
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            if (list[j].itemName.CompareTo(pivot.itemName) <= 0)
            {
                i++;
                //swap
                Item temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
        //pivot자리 교환
        Item temp2 = list[i + 1];
        list[i + 1] = list[high];
        list[high] = temp2;

        return i + 1;
    }
}
