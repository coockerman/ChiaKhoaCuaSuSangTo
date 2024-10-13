using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TTMatMa : AbtractThuThach
{
    [SerializeField] GameObject objMatMa;
    [SerializeField] TextMeshProUGUI txtMatMa;
    protected override bool IsCheckDieuKien()
    {
        if(txtMatMa.text == "9412")
        {
            OffObject();
            return true;
        }
        return false;
    }
    public void OffObject()
    {
        Cursor.lockState = CursorLockMode.Locked;
        objMatMa.gameObject.SetActive(false);
        ExamplePlayer.Instance.IsSelectMouse = true;
    }
    public void NhapMa(int so)
    {
        if(so > 9 || so < 1)
        {
            txtMatMa.text = "";
        }
        else
        {
            txtMatMa.text += so.ToString();
        }
    }
}
