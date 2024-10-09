using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    List<DialogData> dialogDatas = new List<DialogData>();
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] float letterDelay = 0.1f;
    bool isRunDialog = false;
    bool isCanAddHoiThoai = true;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        CheckHoiThoai();
    }

    public void AddHoiThoai(DialogData newDialog)
    {
        if(isCanAddHoiThoai)
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

        // Tách đoạn hội thoại thành từng từ
        string[] words = dialogue.Split(' ');

        foreach (string word in words)
        {
            UIPlayer.instance.DialogText.text += word + " ";  // Thêm từng từ vào UI Text
            yield return new WaitForSeconds(letterDelay);  // Chờ một khoảng thời gian giữa các từ
        }

        yield return new WaitForSeconds(3f);
        dialogDatas.RemoveAt(0);  // Xoá đoạn hội thoại đầu tiên
        UIPlayer.instance.BoxDialogText.SetActive(false);  // Ẩn Text sau khi hội thoại kết thúc
        isRunDialog = false;
    }

    public bool CheckHaveHoiThoai()
    {
        isCanAddHoiThoai = false;
        if (!isRunDialog && dialogDatas.Count <= 0) return false;
        else return true;
    }
    public void AddItem()
    {

    }
}
