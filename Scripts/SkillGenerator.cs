using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class SkillGenerator : MonoBehaviour
{

    public TextMeshProUGUI SkillTree;
    public string[] SkillTreeOptions;
    public TextMeshProUGUI ChallengeText;
    public Challenge[] Challenges;
    public int currentChallengePoints;

    public int currentSkillID;

    public void GenerateNewChallenge()
    {
        int randNum = Random.Range(0, Challenges.Length);
        currentChallengePoints = Challenges[randNum].points;
        ChallengeText.text = Challenges[randNum].challenge;
    }

    public void GenerateSkillBuild()
    {
        int randomID = Random.Range(0, SkillTreeOptions.Length);
        SkillTree.text = SkillTreeOptions[randomID];
        currentSkillID = randomID;
    }

    public void GeneratePredeterminedSkillBuild(int id)
    {
        SkillTree.text = SkillTreeOptions[id];
    }
}

