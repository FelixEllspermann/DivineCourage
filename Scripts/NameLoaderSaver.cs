using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NameLoaderSaver : MonoBehaviour
{
    private void Start()
    {
        CheckForUsername();
    }

    public TMP_InputField InputField;
    public void SavePlayer()
    {
        SaveSystem.SavePlayerName(InputField.text);
        SceneManager.LoadScene("SampleScene");
    }

    public string LoadPlayer()
    {
        string playerName = SaveSystem.LoadName();

        return playerName;
    }

    public void CheckForUsername()
    {
        if (LoadPlayer() == null)
        {
            return;
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
