using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class Test2_ArraySort : MonoBehaviour
{
    public void Start()
    {
        int[] data = GenerateRandomArray(100);
        //StartSelectionSort(data);
        //StartQuickSort(data, 0, data.Length - 1);
        foreach (var item in data)
        {
            Debug.Log(item);
        }
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

    public static void StartSelectionSort(int[] arr) //�������� �Լ�
    {                                                //����: ù �ε������� ���ʴ�� ���� ���� ���� ã�Ƽ� ������ ���ġ
        int n = arr.Length;                          //�ð� ���⵵: n(n - 1)X 1/2 --> (n-1) + (n-2) .... + 2 + 1
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }
            // swap
            int temp = arr[minIndex];
            arr[minIndex] = arr[i];
            arr[i] = temp;
        }
    }

    //���� ���� �Լ�
    //����(����):
    //�迭: [29, 10, 14, 37, 13]
    //1ȸ��:
    // (29,10) �� ��ȯ �� [10, 29, 14, 37, 13] / (29,14) �� ��ȯ �� [10, 14, 29, 37, 13]  / (29,37) �� �״��  /  (37,13) �� ��ȯ �� [10, 14, 29, 13, 37]

    //2ȸ��:
    // (10,14) �� �״�� / (14,29) �� �״�� /  (29,13) �� ��ȯ �� [10, 14, 13, 29, 37]

    //3ȸ��:
    // (10,14) �� �״�� /  (14,13) �� ��ȯ �� [10, 13, 14, 29, 37]

    //4ȸ��:
    // (10,13) �� �״��
    //���� ���: [10, 13, 14, 29, 37]
    //�ð� ���⵵: n(n - 1)X 1/2 --> (n-1) + (n-2) .... + 2 + 1

    public static void StartBubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            bool swapped = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    //swap
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                    swapped = true;
                }
            }
            //�̺� ���ĵ� ��� ���� ����
            if (!swapped) break;
        }
    }

    //�� ����
    //����:
    //���� ����(Divide & Conquer) ���
    //1. �ǹ�(Pivot) ����
    //2. �ǹ����� ���� ���� ����, ū ���� ���������� ����
    //3. �� �κ� �迭�� ���� ��������� ���� ����

    //����:
    //�迭: [29, 10, 14, 37, 13]
    //�ǹ�: 29  �� ����: [10, 14, 13], �ǹ�: [29], ������: [37]
    //����[10, 14, 13] �� �ǹ�: 10  �� [ ], [10], [14, 13]
    //[14, 13] �� �ǹ�: 14  �� [13], [14], [ ]

    //�� ���� �� ���� ��ü: [10, 13, 14]
    //������[37] �� �̹� ���ĵ�

    // ���� ���: [10, 13, 14, 29, 37]

    //�ð� ���⵵:n(�Ѱ���_Ž�� ����) X log(n)(���� Ƚ��)


    public static void StartQuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);

            StartQuickSort(arr, low, pivotIndex - 1);
            StartQuickSort(arr, pivotIndex + 1, high);
        }
    }

    private static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                //swap
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
        //pivot�ڸ� ��ȯ
        int temp2 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp2;

        return i + 1;
    }
}
