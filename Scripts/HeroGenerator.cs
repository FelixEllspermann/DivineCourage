using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HeroGenerator : MonoBehaviour
{
    public Image HeroImage;
    public Hero[] Heroes;
    public TextMeshProUGUI[] TalentTree;

    public int currentDif;

    public void GenerateNewHero()
    {
        int randomID = Random.Range(0, Heroes.Length);
        getImage(Heroes[randomID].URL);
        currentDif = Heroes[randomID].difficulty;
        GenerateTalentTree();
    }

    public void GenerateTalentTree()
    {
        for (int i = 0; i < 4; i++)
        {
            int randomTalent = Random.Range(0, 2);
            if (randomTalent == 0)
            {
                TalentTree[i].text = "Left";
            }
            else
            {
                TalentTree[i].text = "Right";
            }
        }
    }

    public void getImage(string URL)
    {
        StartCoroutine(getImageFromURL(URL));
    }



    IEnumerator getImageFromURL(string URL_)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(URL_);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            Texture2D rawImage = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite imageForSprite = Sprite.Create(rawImage, new Rect(0, 0, rawImage.width, rawImage.height), Vector2.zero);
            HeroImage.sprite = imageForSprite;

        }
    }

}
