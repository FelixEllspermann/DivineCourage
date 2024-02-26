using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NeutralItem", menuName = "Dota/Neutral Item", order = 1)]
public class NeutralItems : ScriptableObject
{
    public string name;
    public string URL;
    public int Tier;
    public Sprite Icon;
}
