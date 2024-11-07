using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject objHuongDan;
    [SerializeField] private GameObject objAbout;

    [SerializeField] private GameObject objPlayer;
    [SerializeField] private float speedRotate = 3f;

    [SerializeField] private GameObject UISelect;
    [SerializeField] private GameObject btnUISelect;
    [SerializeField] private GameObject groupSelct;
    [SerializeField] private GameObject prefabSelect;
    [SerializeField] private ManagerData managerData;
    
    private Vector3 startPos;
    private Transform transformPlayer;
    private bool loadUISelect = false;
    private void Start()
    {
        transformPlayer = objPlayer.transform;
        if (managerData.ManChoiFinish <= 1)
        {
            btnUISelect.SetActive(false);
        }
        else
        {
            btnUISelect.SetActive(true);
        }
    }
    
    private void Update()
    {
        transformPlayer.Rotate(0,speedRotate,0);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnUiSelect()
    {
        UISelect.SetActive(true);
        SetupUiSelect();
    }

    public void OffUiSelect()
    {
        UISelect.SetActive(false);
    }

    void SetupUiSelect()
    {
        if (managerData.ManChoiFinish <= 1 || loadUISelect)
        {
            Debug.Log("Have save");
            return;
        }
        
        for (int lv = 1; lv <= managerData.ManChoiFinish; lv++)
        {
            int lvTemp = lv;
            GameObject newPrefab = Instantiate(prefabSelect, groupSelct.transform);
            newPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Màn " + lvTemp;
            newPrefab.transform.GetComponent<Button>().onClick.AddListener(() => {LoadSceneIndex(lvTemp);});
        }

        loadUISelect = true;
    }

    void LoadSceneIndex(int index)
    {
        SceneManager.LoadScene(index);
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
