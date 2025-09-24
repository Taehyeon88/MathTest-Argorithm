using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Task4_Search2 : MonoBehaviour
{
    [Header("UI Reference")]
    public TMP_InputField searchItemInputField;
    public Button linearSearchButton;
    public Button bindarySearchButton;
    public TMP_Text descriptionText;
    public GameObject parentObj;     //이미지를 가지는 부모 오브젝트
    public GameObject imagePrf;      //이미지 프리팹

    public List<GameObject> itemImageObjs = new List<GameObject>();
    public List<Item> items = new List<Item>();

    private void Start()
    {
        if (linearSearchButton == null || bindarySearchButton == null) return;
        linearSearchButton.onClick.AddListener(SetItemByLinear);
        bindarySearchButton.onClick.AddListener(SetItemByBinary);

        //아이템 생성
        for (int i = 0; i < 100; i ++)
        {
            items.Add(new Item($"Item_{i}", 1));

            GameObject temp = Instantiate(imagePrf);
            temp.transform.SetParent(parentObj.transform);  //자식으로 넣어줌
            TextMeshProUGUI text = temp.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = $"Item_{i}";  //텍스트 할당
            }
            itemImageObjs.Add(temp);  //저장
        }
    }

    public void SetItemByLinear()
    {
        if (string.IsNullOrEmpty(searchItemInputField.text))
        {
            descriptionText.text = $"You have to set text in InputField!!";
            return;
        }
        descriptionText.text = "";

        GameObject temp = FindItemByLinear(searchItemInputField.text);
        foreach (var itemObj in itemImageObjs)
        {
            if (itemObj != temp)
            {
                Destroy(itemObj);  //해당 이미지 삭제
            }
        }
    }

    public void SetItemByBinary()
    {
        if (string.IsNullOrEmpty(searchItemInputField.text))
        {
            descriptionText.text = $"You have to set text in InputField!!";
            return;
        }
        descriptionText.text = "";

        GameObject temp = FindItemByBinary(searchItemInputField.text);
        foreach (var itemObj in itemImageObjs)
        {
            if (itemObj != temp)
            {
                Destroy(itemObj);  //해당 이미지 삭제
            }
        }
    }

    //선형 탐색
    public GameObject FindItemByLinear(string _itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == _itemName)
                return itemImageObjs[i];
        }
        return null;
    }

    //이진 탐색
    public GameObject FindItemByBinary(string targetName)
    {
        int left = 0;
        int right = items.Count - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            int compare = items[mid].itemName.CompareTo(targetName);

            if (compare == 0)
            {
                return itemImageObjs[mid];
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
