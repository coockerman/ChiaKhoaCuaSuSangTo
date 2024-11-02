using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    public static UIPlayer instance;
    [SerializeField] GameObject boxDialogText;

    public GameObject BoxDialogText
    {
        get { return boxDialogText; }
    }

    [SerializeField] TextMeshProUGUI dialogText;

    public TextMeshProUGUI btnVolume;
    public GameObject UiSetting;

    public TextMeshProUGUI DialogText
    {
        get { return dialogText; }
    }

    public GameObject huongDan;
    public Image imgChuyenMan;
    bool isChuyenMan = false;
    delegate void ChuyenManDelegate();
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
        if (!isChuyenMan)
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
        imgChuyenMan.gameObject.SetActive(false);
    }

    IEnumerator ChuyenMan()
    {
        imgChuyenMan.gameObject.SetActive(true);
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
        GamePlayManager.Instance.ChuyenMan();
    }
    IEnumerator TatManHinh(ChuyenManDelegate chuyenManDelegate)
    {
        imgChuyenMan.gameObject.SetActive(true);
        Color color = imgChuyenMan.color;
        imgChuyenMan.color = color;

        for (float t = 0; t <= 2f; t += 0.1f)
        {
            color.a = Mathf.Lerp(0f, 1f, t / 2f);
            imgChuyenMan.color = color;
            yield return new WaitForSeconds(0.1f);
        }

        color.a = 1f;
        imgChuyenMan.color = color;
        yield return new WaitForSeconds(0.5f);
        chuyenManDelegate?.Invoke();
    }
    public void OnSettingGame()
    {
        if (UiSetting.activeSelf == false)
        {
            UiSetting.SetActive(true);
        }
    }
    public void ChangeVolume()
    {
        btnVolume.text = AudioController.instance.ChangeVolumeAudio();
    }

    public void RestartGame()
    {
        ChuyenManDelegate restart = GamePlayManager.Instance.RestartGame;
        StartCoroutine(TatManHinh(restart));
    }


    public void MainMenu()
    {
        ChuyenManDelegate mainMenu = GamePlayManager.Instance.MainMenu;
        StartCoroutine(TatManHinh(mainMenu));
    }

    public void ContinueGame()
    {
        UiSetting.SetActive(false);
        GamePlayManager.Instance.LockCursor();
    }
}