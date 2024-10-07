using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActiveObject : MonoBehaviour
{
    public enum loaiDoXoay
    {
        cuaRaVao, cuaTu, nganKeoDai, nganKeoNgan, cuaSo
    }
    [SerializeField] bool isMoveOrRotateIn = true;
    [SerializeField] bool isLoop = false;
    [SerializeField] bool isActive = false;
    [SerializeField] loaiDoXoay loaiDo;
    float gocXoay = 90;
    float doDaiNganKeo = 1f;
    float doDaiCuaSo = 0.5f;
    private void Start()
    {
        gameObject.tag = "Selectable";
        if (isActive)
        {
            HandelAction();
        }
        if (loaiDo == loaiDoXoay.cuaRaVao) gocXoay = 90;
        else if (loaiDo == loaiDoXoay.cuaTu) gocXoay = 135;
        else if (loaiDo == loaiDoXoay.nganKeoNgan) doDaiNganKeo = 0.3f;
        else if (loaiDo == loaiDoXoay.nganKeoDai) doDaiNganKeo = 0.5f;

    }
    public void HandelAction()
    {
        if (loaiDo == loaiDoXoay.cuaRaVao || loaiDo == loaiDoXoay.cuaTu)
        {
            if (!isLoop)
            {
                if (!isActive) RotateOn();
            }
            else
            {
                RotateOn();
            }
        }
        else
        {
            if (!isLoop)
            {
                if (!isActive) MoveOn();
            }
            else
            {
                MoveOn();
            }
        }

    }
    void RotateOn()
    {
        isActive = true;

        if (isMoveOrRotateIn)
        {
            transform.Rotate(new Vector3(0, gocXoay, 0));
            isMoveOrRotateIn = false;
        }
        else
        {
            transform.Rotate(new Vector3(0, -gocXoay, 0));
            isMoveOrRotateIn = true;
        }
    }
    void MoveOn()
    {
        isActive = true;
        if(isMoveOrRotateIn)
        {
            transform.Translate(new Vector3(doDaiNganKeo, 0, 0));
            isMoveOrRotateIn = false;
        }
        else
        {
            transform.Translate(new Vector3(-doDaiNganKeo, 0, 0));
            isMoveOrRotateIn = true;
        }
    }
}
