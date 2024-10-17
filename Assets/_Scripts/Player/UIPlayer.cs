using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    public static UIPlayer instance;
    [SerializeField] GameObject boxDialogText;
    public GameObject BoxDialogText { get { return boxDialogText; } }
    [SerializeField] TextMeshProUGUI dialogText;
    public TextMeshProUGUI DialogText { get { return dialogText; } }
    public GameObject huongDan;
    public Image imgChuyenMan;
    bool isChuyenMan = false;
    private void Awake()
    {
        instance = this;
    }
    public void StartManChoi()
    {
        StartCoroutine(BatDauManChoi());
    }

    public void StartChuyenMan()
    {
        if(!isChuyenMan)
        {
            StartCoroutine(ChuyenMan());
            isChuyenMan = true;
        }
    }

    IEnumerator BatDauManChoi()
    {
        // Đặt alpha ban đầu là 1 (màn hình tối)
        Color color = imgChuyenMan.color;
        color.a = 1f; // Màn hình tối
        imgChuyenMan.color = color;
        yield return new WaitForSeconds(0.5f);
        // Giảm alpha từ 1 xuống 0 trong 2 giây
        for (float t = 0; t <= 2f; t += 0.1f)
        {
            color.a = Mathf.Lerp(1f, 0f, t / 2f);
            imgChuyenMan.color = color;
            yield return new WaitForSeconds(0.1f);
        }
        color.a = 0f;
        imgChuyenMan.color = color;
    }

    IEnumerator ChuyenMan()
    {
        // Đặt alpha ban đầu là 0 (màn hình sáng)
        Color color = imgChuyenMan.color;
        color.a = 0f; // Màn hình sáng
        imgChuyenMan.color = color;

        // Tăng alpha từ 0 lên 1 trong 2 giây
        for (float t = 0; t <= 2f; t += 0.1f)
        {
            color.a = Mathf.Lerp(0f, 1f, t / 2f);
            imgChuyenMan.color = color;
            yield return new WaitForSeconds(0.1f);
        }

        color.a = 1f;
        imgChuyenMan.color = color;
        yield return new WaitForSeconds(0.5f);
        // Gọi chuyển màn sau khi hiệu ứng hoàn tất
        GamePlayManager.Instance.ChuyenMan();
    }
}
