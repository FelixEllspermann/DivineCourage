using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Dota/Item", order = 2)]
public class Item : ScriptableObject
{
    public string name;
    public string URL;
    public int cost;
    public Sprite Icon;

}
