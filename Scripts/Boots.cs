using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Boots", menuName = "Dota/Boots", order = 4)]
public class Boots : ScriptableObject
{
    public string name;
    public Sprite Icon;
    public string URL;
    public int cost;
    public bool isItemForCore;
}
