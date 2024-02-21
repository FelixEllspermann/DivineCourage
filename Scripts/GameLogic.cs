using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public HeroGenerator HeroGenerator;
    public ItemGenerator ItemGenerator;
    public SkillGenerator SkillGenerator;
    public ScoreManager ScoreManager;
   
    void Start()
    {
        CreateNewBuild();
    }

    public void CreateNewBuild()
    {
        HeroGenerator.GenerateNewHero();
        ItemGenerator.CreateNewItemBuild();
        ItemGenerator.CreateNewStartBuild();
        SkillGenerator.GenerateSkillBuild();
        ScoreManager.generateScore();
    }
}
