using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAddHoiThoai : MonoBehaviour
{
    ActiveObject activeObject;
    [SerializeField] List<DialogData> dialogData = new List<DialogData>();
    bool isAdd = false;
    private void Start()
    {
        activeObject = GetComponent<ActiveObject>();
    }
    private void Update()
    {
        CheckAdd();
    }
    void CheckAdd()
    {
        if (isAdd || dialogData.Count <= 0) return;
        if (activeObject != null)
        {
            if (activeObject.IsActive)
            {
                foreach (DialogData dialog in dialogData)
                {
                    Player.Instance.AddHoiThoai(dialog);
                }
                isAdd = true;
            }
        }
    }
}
