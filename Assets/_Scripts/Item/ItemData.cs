using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData", fileName = "item", order = 1)]
public class ItemData : ScriptableObject
{
    public string tenItem;
    public int maItem;
    public Sprite spriteItem;
}
