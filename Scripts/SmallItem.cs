using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Small Item", menuName = "Dota/Small Item", order = 3)]
public class SmallItem : ScriptableObject
{
    public string name;
    public string URL;
    public int cost;
    public bool isItemForCore;
}
