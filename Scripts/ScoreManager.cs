using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    private HeroGenerator hg;
    private ItemGenerator ig;
    public SkillGenerator sg;

    public int MaxRandomScore;
    public int RawScore = 0;

    public int FullScore;
    public int ScoreWithoutChallenge;

    public bool ChallengeIsActive = true;


    private void Start()
    {
        hg = GetComponent<HeroGenerator>();
        ig = GetComponent<ItemGenerator>();
        sg = GetComponent<SkillGenerator>();
    }
    private void Update()
    {
        if (ChallengeIsActive)
        {
            RawScore = FullScore;
        }
        else
        {
            RawScore = ScoreWithoutChallenge;
        }
        ScoreText.text = "Score: " + RawScore.ToString();
    }

    public void generateScore()
    {
        int difMultiplier = hg.currentDif;
        int goldCost = ig.cost;
        int randomScore = Random.Range(0, MaxRandomScore);
        int ChallengeCost = sg.currentChallengePoints;
        int randomGoldScore = Random.Range(0, 20000);
        FullScore = (goldCost + randomGoldScore) + (difMultiplier * (randomScore + ChallengeCost));  
        ScoreWithoutChallenge = (goldCost + randomGoldScore) + (difMultiplier * randomScore);
        
    }
}
