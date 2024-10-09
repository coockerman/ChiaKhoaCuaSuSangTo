using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbtractThuThach : MonoBehaviour
{
    public List<GameObject> listActiveCheck = new List<GameObject>();
    [SerializeField] float timeDelayCheck = 0.5f;
    [SerializeField] DialogData thongBaoHoanThanh;
    float countTimeCheck = 0;
    bool isCheckFinish = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TimeCheck();
    }
    void TimeCheck()
    {
        if (isCheckFinish) return;
        countTimeCheck += Time.deltaTime;
        if (countTimeCheck > timeDelayCheck)
        {
            CheckCanActive();
            countTimeCheck = 0;
        }
    }
    protected abstract bool IsCheckDieuKien();
    
    void CheckCanActive()
    {
        if (IsCheckDieuKien()) 
            CanActive();
    }
    void CanActive()
    {
        GetComponent<ActiveObject>().SetCanActive(true);
        if (thongBaoHoanThanh != null)
            Player.Instance.AddHoiThoai(thongBaoHoanThanh);
        isCheckFinish = true;
    }
}
