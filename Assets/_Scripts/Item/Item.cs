using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] bool keyManChoi = false;
    [SerializeField] List<DialogData> datas = new List<DialogData>();
    bool isStartCheckChuyenMan = false;
    private void Start()
    {
        gameObject.tag = "Item";
    }
    private void Update()
    {
        if(isStartCheckChuyenMan)
        {
            if(Player.Instance.CheckHaveHoiThoai())
            {
                GamePlayManager.Instance.ChuyenMan();
            }
        }
    }
    public void HanderAction()
    {
        if(keyManChoi)
        {
            XuLyChuyenMan();
        }
        else
        {
            Player.Instance.AddItem();
        }
    }
    public void XuLyChuyenMan()
    {
        foreach(DialogData data in datas)
        {
            Player.Instance.AddHoiThoai(data);
        }
        isStartCheckChuyenMan = true;
    }
}
