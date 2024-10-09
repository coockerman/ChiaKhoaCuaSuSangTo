using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    public static UIPlayer instance;
    [SerializeField] GameObject boxDialogText;
    public GameObject BoxDialogText { get { return boxDialogText; } }
    [SerializeField] TextMeshProUGUI dialogText;
    public TextMeshProUGUI DialogText { get { return dialogText; } }
    public GameObject huongDan;
    private void Awake()
    {
        instance = this;
    }
    
}
