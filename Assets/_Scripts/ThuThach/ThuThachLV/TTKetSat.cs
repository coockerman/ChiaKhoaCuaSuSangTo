using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTKetSat : AbtractThuThach
{
    protected override bool IsCheckDieuKien()
    {
        foreach(GameObject obj in listActiveCheck)
        {
            if (obj.transform.eulerAngles.y != 180) return false;
        }
        foreach(GameObject obj in listActiveCheck)
        {
            obj.GetComponent<ActiveObject>().SetCanActive(false);
        }
        return true;
    }
}
