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
        Color color = imgChuyenMan.color;
        color.a = 1f;
        imgChuyenMan.color = color;
        yield return new WaitForSeconds(0.5f);
        
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
        Color color = imgChuyenMan.color;
        color.a = 0f;
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
        
        GamePlayManager.Instance.ChuyenMan();
    }
}
