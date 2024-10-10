using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public enum LoaiItem
    {
        doVat, huongDan
    }
    [SerializeField] bool keyManChoi = false;
    [SerializeField] ItemData itemData;
    [SerializeField] List<DialogData> datas = new List<DialogData>();
    [SerializeField] LoaiItem loaiItem;
    [SerializeField] float timeDelayAddHoiThoai = 2f;
    float countDelayAddHoiThoai = 0;
    //bool isStartCheckChuyenMan = false;

    private void Start()
    {
        gameObject.tag = "Item";
    }
    private void Update()
    {
        if (datas.Count > 0)
            countDelayAddHoiThoai += Time.deltaTime;
    }
    public void HanderAction()
    {
        if (keyManChoi)
        {
            AddHoiThoaiItem();
            Player.Instance.XuLyChuyenMan();
        }
        else
        {
            if (loaiItem == LoaiItem.doVat)
            {
                AddHoiThoaiItem();
                Player.Instance.AddItem(itemData);
                Destroy(gameObject);
            }

            else if (loaiItem == LoaiItem.huongDan)
            {
                AddHoiThoaiItem();
            }

        }
    }
    
    void AddHoiThoaiItem()
    {
        if (countDelayAddHoiThoai > timeDelayAddHoiThoai)
            foreach (DialogData data in datas)
            {
                countDelayAddHoiThoai = 0;
                Player.Instance.AddHoiThoai(data);
            }
    }
}
