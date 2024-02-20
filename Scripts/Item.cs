using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Dota/Item", order = 1)]
public class Item : ScriptableObject
{
    public string name;
    public int ID;
    public string URL;
    public int cost;
    public bool isItemForCore;
    public bool isSellable;

}
