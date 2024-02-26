using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public HeroGenerator HeroGenerator;
    public ItemGenerator ItemGenerator;
    public SkillGenerator SkillGenerator;
    public ScoreManager ScoreManager;

    public AudioSource GoldSound;


    public TextMeshProUGUI RollValueText;
    public TextMeshProUGUI ShardTimer;
    

    public void CreateNewBuild()
    {
        HeroGenerator.GenerateNewHero();
        ItemGenerator.CreateNewItemBuild();
        ItemGenerator.CreateNewStartBuild();
        ItemGenerator.CreateNewNeutralItemBuild();
        SkillGenerator.GenerateSkillBuild();
        SkillGenerator.GenerateNewChallenge();
        ScoreManager.generateScore();

        float randomShardTimer = Random.Range(450f, 1200f);

        int minutes= Mathf.FloorToInt(randomShardTimer / 60f);
        int seconds = Mathf.FloorToInt(randomShardTimer % 60);

        GoldSound.Play();
        ShardTimer.text = "Shard at: " + minutes + ":" + seconds;
        RollValueText.text = "Roll Value: " + Random.Range(0, 1000);
    }
}
