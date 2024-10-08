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
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "";  // Xóa nội dung cũ

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;  // Thêm từng chữ cái vào UI Text
            yield return new WaitForSeconds(letterDelay);  // Chờ một khoảng thời gian giữa các chữ cái
        }

        yield return new WaitForSeconds(3f);
        dialogDatas.RemoveAt(0);  // Xoá đoạn hội thoại đầu tiên
        dialogueText.gameObject.SetActive(false);  // Ẩn Text sau khi hội thoại kết thúc
        isRunDialog = false;
    }
}
