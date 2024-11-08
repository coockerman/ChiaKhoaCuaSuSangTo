using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public enum LoaiItem
    {
        doVat, huongDan, moKhoa, pin
    }
    
    [SerializeField] bool keyManChoi = false;
    [SerializeField] ItemData itemData;
    [SerializeField] List<DialogData> datas = new List<DialogData>();
    [SerializeField] LoaiItem loaiItem;
    [SerializeField] float timeDelayAddHoiThoai = 2f;
    [SerializeField] GameObject objectNhapKhoa;
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
            AudioController.instance.OnAudioDoVat();
            Player.Instance.XuLyChuyenMan();
        }
        else
        {
            if (loaiItem == LoaiItem.doVat)
            {
                AddHoiThoaiItem();
                AudioController.instance.OnAudioDoVat();
                Player.Instance.AddItem(itemData);
                Destroy(gameObject);
            }
            else if (loaiItem == LoaiItem.huongDan)
            {
                AudioController.instance.OnAudioHuongDan();
                AddHoiThoaiItem();
            }
            else if (loaiItem == LoaiItem.moKhoa)
            {
                AudioController.instance.OnAudioHuongDan();

                if (objectNhapKhoa != null)
                {
                    GamePlayManager.Instance.ReCusor();
                    objectNhapKhoa.SetActive(true);
                }
            }
            else if (loaiItem == LoaiItem.pin)
            {
                bool checkAddEnergy = GamePlayManager.Instance.AddEnergyFlashLight(50);
                if (checkAddEnergy) 
                {
                    // thêm thành công
                    AudioController.instance.OnAudioDoVat();
                    Destroy(gameObject);
                }
                else
                {
                    //Thêm không thành công và in ra hội thoại lỗi
                    AddHoiThoaiItem();
                }
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
