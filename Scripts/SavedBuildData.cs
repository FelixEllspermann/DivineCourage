using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SavedBuildData 
{
    public SavedBuildData(SavedBuildData data)
    {
        HeroID = data.HeroID;
        ItemIDs = new int[data.ItemIDs.Length];
        StartItemIDs = new int[data.StartItemIDs.Length];
        TalentIDs = new int[data.TalentIDs?.Length ?? 0]; // Sicherstellen, dass TalentIDs im Originalobjekt existiert

        for (int i = 0; i < data.ItemIDs.Length; i++)
        {
            ItemIDs[i] = data.ItemIDs[i];
        }
        for (int i = 0; i < data.StartItemIDs.Length; i++)
        {
            StartItemIDs[i] = data.StartItemIDs[i];
        }
        if (data.TalentIDs != null) // Überprüfe, ob TalentIDs initialisiert wurde
        {
            for (int i = 0; i < data.TalentIDs.Length; i++)
            {
                TalentIDs[i] = data.TalentIDs[i];
            }
        }
        skillPriority = data.skillPriority;
        Score = data.Score; // Außerhalb der Schleife setzen
    }

    public SavedBuildData()
    {
    }

    public int HeroID;
    public int[] ItemIDs = new int[6];
    public int[] StartItemIDs = new int[6];

    public int skillPriority;
    public int[] TalentIDs;
    public int Score;
}
