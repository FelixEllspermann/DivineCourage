using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class SkillGenerator : MonoBehaviour
{

    public TextMeshProUGUI SkillTree;
    public string[] SkillTreeOptions;
    public void GenerateSkillBuild()
    {
        int randomID = Random.Range(0, SkillTreeOptions.Length);
        SkillTree.text = SkillTreeOptions[randomID];
    }
}

