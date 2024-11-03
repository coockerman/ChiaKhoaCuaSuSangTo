using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    List<ItemData> listItem = new List<ItemData>();
    List<DialogData> dialogDatas = new List<DialogData>();
    [SerializeField] List<DialogData> dialogStart = new List<DialogData>();
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] float letterDelay = 0.1f;
    bool isRunDialog = false;
    bool isStartCheckChuyenMan = false;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UIPlayer.instance.StartManChoi();
        StartCoroutine(addFirstDialog(2.5f));
    }
    IEnumerator addFirstDialog(float timeWait)
    {
        yield return new WaitForSeconds(timeWait);
        AddDialogFirst();
    }
    void AddDialogFirst()
    {
        if (dialogStart.Count > 0)
        {
            foreach (DialogData data in dialogStart)
            {
                AddHoiThoai(data);
            }
        }
    }
    void Update()
    {
        CheckHoiThoai();
        if (isStartCheckChuyenMan)
        {
            if (CheckHaveHoiThoai())
            {
                UIPlayer.instance.StartChuyenMan();
            }
        }
    }

    public void AddHoiThoai(DialogData newDialog)
    {
        if(!isStartCheckChuyenMan)
        dialogDatas.Add(newDialog);
    }

    void CheckHoiThoai()
    {
        if (dialogDatas.Count > 0 && !isRunDialog)
        {
            StartCoroutine(DisplayLetters(dialogDatas[0].noiDungHoiThoai));
        }
    }

    IEnumerator DisplayLetters(string dialogue)
    {
        isRunDialog = true;
        UIPlayer.instance.BoxDialogText.SetActive(true);
        UIPlayer.instance.DialogText.text = "";  // Xóa nội dung cũ
        float timeWait = 1f;
        // Tách đoạn hội thoại thành từng từ
        string[] words = dialogue.Split(' ');

        foreach (string word in words)
        {
            timeWait += 0.1f;
            UIPlayer.instance.DialogText.text += word + " ";  // Thêm từng từ vào UI Text
            yield return new WaitForSeconds(letterDelay);  // Chờ một khoảng thời gian giữa các từ
        }

        yield return new WaitForSeconds(timeWait);
        dialogDatas.RemoveAt(0);  // Xoá đoạn hội thoại đầu tiên
        UIPlayer.instance.BoxDialogText.SetActive(false);  // Ẩn Text sau khi hội thoại kết thúc
        isRunDialog = false;
    }

    public bool CheckHaveHoiThoai()
    {
        if (!isRunDialog && dialogDatas.Count <= 0) return true;
        else return false;
    }
    public void XuLyChuyenMan()
    {
        isStartCheckChuyenMan = true;
    }
    public void AddItem(ItemData itemData)
    {
        if(itemData!=null)
        {
            listItem.Add(itemData);
        }
    }
    public bool CheckItemOnBag(List<ItemData> listItemData)
    {
        foreach(ItemData item in listItemData)
        {
            if(!listItem.Contains(item))
            {
                return false;
            }
        }
        return true;
    }
    public void GetItemOnBag(List<ItemData> listItemData)
    {
        foreach (ItemData item in listItemData)
        {
            ItemData itemFind = listItem.Find(i => i == item);
            if (itemFind != null)
            {
                listItem.Remove(itemFind);
            }
        }
    }
}
