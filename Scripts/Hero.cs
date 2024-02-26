using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Hero", menuName = "Dota/Hero", order = 1)]
public class Hero : ScriptableObject
{
    public string name;
    public Sprite Icon;
    public string URL;
    public int difficulty;
}

