using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    private HeroGenerator hg;
    private ItemGenerator ig;

    public int MaxRandomScore;


    private void Start()
    {
        hg = GetComponent<HeroGenerator>();
        ig = GetComponent<ItemGenerator>();
    }

    public void generateScore()
    {
        int difMultiplier = hg.currentDif;
        int goldCost = ig.cost;
        int randomScore = Random.Range(0, MaxRandomScore);

        int RawScore = goldCost + (difMultiplier * randomScore);

        ScoreText.text = "Score: " + RawScore.ToString();
    }
}
