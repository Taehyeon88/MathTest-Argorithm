using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Task2_ArraySort : MonoBehaviour
{
    public int dataCount = 100;
    //선택정렬
    public void PlaySelectionSort()
    {
        int[] data = GenerateRandomArray(dataCount);

        Stopwatch sw = new Stopwatch();
        sw.Reset();
        sw.Start();
        Test2_ArraySort.StartSelectionSort(data);
        sw.Stop();
        long selectionTime = sw.ElapsedMilliseconds;

        UnityEngine.Debug.Log("Selection Sort: " + selectionTime);
    }
    
    public void PlayBubbleSort()
    {
        int[] data = GenerateRandomArray(dataCount);

        Stopwatch sw = new Stopwatch();
        sw.Reset();
        sw.Start();
        Test2_ArraySort.StartBubbleSort(data);
        sw.Stop();
        long selectionTime = sw.ElapsedMilliseconds;

        UnityEngine.Debug.Log("Bubble Sort: " + selectionTime);
    }

    public void PlayQuickSort()
    {
        int[] data = GenerateRandomArray(dataCount);

        Stopwatch sw = new Stopwatch();
        sw.Reset();
        sw.Start();
        Test2_ArraySort.StartQuickSort(data, 0, data.Length - 1);
        sw.Stop();
        long selectionTime = sw.ElapsedMilliseconds;

        UnityEngine.Debug.Log("Quick Sort: " + selectionTime);
    }

    int[] GenerateRandomArray(int size)
    {
        int[] arr = new int[size];
        System.Random rand = new System.Random();
        for (int i = 0; i < size; i++)
        {
            arr[i] = rand.Next(0, 10000);
        }
        return arr;
    }
}
