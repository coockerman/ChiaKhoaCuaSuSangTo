using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActiveObject : MonoBehaviour
{
    public enum loaiDoXoay
    {
        cuaRaVao, cuaTu, nganKeoDai, nganKeoNgan, cuaSo, cauThang
    }
    [SerializeField] bool isMoveOrRotateIn = true;
    [SerializeField] bool isLoop = false;
    [SerializeField] bool isActive = false;
    [SerializeField] loaiDoXoay loaiDo;
    float gocXoay = 90;
    float doDaiKeo = 1f;
    bool huongCuaSoX = false;
    private void Start()
    {
        gameObject.tag = "Selectable";
        if (isActive)
        {
            HandelAction();
        }
        if (loaiDo == loaiDoXoay.cuaRaVao) gocXoay = 90;
        else if (loaiDo == loaiDoXoay.cuaTu) gocXoay = 135;
        else if (loaiDo == loaiDoXoay.nganKeoNgan) doDaiKeo = 0.3f;
        else if (loaiDo == loaiDoXoay.nganKeoDai) doDaiKeo = 0.5f;
        else if (loaiDo == loaiDoXoay.cauThang) gocXoay = 60;
        else if (loaiDo == loaiDoXoay.cuaSo) { XuLyHuongCuaSo(); doDaiKeo = 0.7f; }
    }
    public void HandelAction()
    {
        if (loaiDo == loaiDoXoay.cuaRaVao || loaiDo == loaiDoXoay.cuaTu)
        {
            if (!isLoop)
            {
                if (!isActive) RotateY();
            }
            else
            {
                RotateY();
            }
        }
        else if(loaiDo == loaiDoXoay.nganKeoDai || loaiDo == loaiDoXoay.nganKeoNgan)
        {
            if (!isLoop)
            {
                if (!isActive) MoveX();
            }
            else
            {
                MoveX();
            }
        }
        else if(loaiDo == loaiDoXoay.cauThang)
        {
            if (!isLoop)
            {
                if (!isActive) RotateX();
            }
            else
            {
                RotateX();
            }
        }else if(loaiDo == loaiDoXoay.cuaSo)
        {
            if (!isLoop)
            {
                if (!isActive) DiChuyenCuaSo();
            }
            else
            {
                DiChuyenCuaSo();
            }
        }
    }
    void RotateY()
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
    void RotateX()
    {
        isActive = true;

        if (isMoveOrRotateIn)
        {
            transform.Rotate(new Vector3(gocXoay, 0, 0));
            isMoveOrRotateIn = false;
        }
        else
        {
            transform.Rotate(new Vector3(-gocXoay, 0, 0));
            isMoveOrRotateIn = true;
        }
    }
    void MoveX()
    {
        isActive = true;
        if(isMoveOrRotateIn)
        {
            transform.Translate(new Vector3(doDaiKeo, 0, 0));
            isMoveOrRotateIn = false;
        }
        else
        {
            transform.Translate(new Vector3(-doDaiKeo, 0, 0));
            isMoveOrRotateIn = true;
        }
    }
    void MoveZ()
    {
        isActive = true;
        if (isMoveOrRotateIn)
        {
            transform.Translate(new Vector3(doDaiKeo, 0, 0));
            isMoveOrRotateIn = false;
        }
        else
        {
            transform.Translate(new Vector3(-doDaiKeo, 0, 0));
            isMoveOrRotateIn = true;
        }
    }
    void XuLyHuongCuaSo()
    {
        if(Mathf.Abs(transform.position.x) - 0.7 > Mathf.Abs(transform.position.z) - 0.7)
        {
            huongCuaSoX = false;
        }
        else
        {
            huongCuaSoX = true;
        }
    }
    void DiChuyenCuaSo()
    {
        if(huongCuaSoX) MoveX();
        else MoveZ();
    }
}
