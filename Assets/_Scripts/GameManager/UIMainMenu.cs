using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject objHuongDan;
    [SerializeField] private GameObject objAbout;

    [SerializeField] private GameObject objPlayer;
    [SerializeField] private float speedRotate = 3f;
    private Vector3 startPos;
    private Transform transformPlayer;

    private void Start()
    {
        transformPlayer = objPlayer.transform;
    }

    private void Update()
    {
        transformPlayer.Rotate(0,speedRotate,0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnObjectHuongDan()
    {
        objHuongDan.SetActive(true);
    }
    public void OffObjectHuongDan()
    {
        objHuongDan.SetActive(false);
    }
    public void OnObjectAbout()
    {
        objAbout.SetActive(true);
    }
    public void OffObjectAbout()
    {
        objAbout.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
