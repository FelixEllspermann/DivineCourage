using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour
{
   
    public Item[] items;
    public Boots[] boots;
    public TextMeshProUGUI CostText;
    public int cost;

    public int StartGold = 600;
    private int StartGoldSave;
    public SmallItem[] SmallItems;
    public Image[] StartBuildImages;

    public Image[] ItemSprites = new Image[6];


    private void Start()
    {
        StartGoldSave = StartGold;
    }
    public void CreateNewItemBuild()
    {
        cost = 0;

        for (int i = 0; i < ItemSprites.Length; i++)
        {
            if (i < 5)
            {
                int randomID = Random.Range(0, items.Length);
                getImage(items[randomID].URL, ItemSprites[i]);
                cost += items[randomID].cost;

                CostText.text = "Total Costs: " + cost;
            }
            else
            {
                int randomBootsID = Random.Range(0, boots.Length);
                getImage(boots[randomBootsID].URL, ItemSprites[i]);
                cost += boots[randomBootsID].cost;
            }

           
        }

        

    }

    public void CreateNewStartBuild()
    {
        int iteration = 0;        
        // Alle StartBuildImages deaktivieren
        for (int i = 0; i < StartBuildImages.Length; i++)
        {            
            StartBuildImages[i].sprite = null;
            StartBuildImages[i].enabled = false;
        }

        // Schleife läuft, bis kein Gold mehr vorhanden ist oder alle Slots gefüllt sind
        while (StartGold > 0 && iteration < StartBuildImages.Length)
        {
            bool isPurchasable = false;
            int randomID = -1;

            // Versuche, ein kaufbares Item zu finden
            for (int tryCount = 0; tryCount < SmallItems.Length && !isPurchasable; tryCount++)
            {
                randomID = Random.Range(0, SmallItems.Length);
                if (SmallItems[randomID].cost <= StartGold)
                {
                    isPurchasable = true; // Kaufbares Item gefunden
                    break;
                }
            }

            // Prüfe, ob ein kaufbares Item gefunden wurde
            if (!isPurchasable)
            {
                break; // Kein kaufbares Item gefunden, Schleife beenden
            }

            // Kaufbares Item verarbeiten
            getImage(SmallItems[randomID].URL, StartBuildImages[iteration]);
            StartBuildImages[iteration].enabled = true;
            StartGold -= SmallItems[randomID].cost;

            iteration++; // Nächsten Slot für das nächste Item vorbereiten
        }

        StartGold = StartGoldSave;

        // Die Schleife endet automatisch, wenn kein Gold mehr vorhanden ist oder alle Slots gefüllt sind
    }




    public void getImage(string URL, Image image)
    {
        StartCoroutine(getImageFromURL(URL, image));        
    }



    IEnumerator getImageFromURL(string URL_, Image image)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(URL_);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            Texture2D rawImage = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite imageForSprite = Sprite.Create(rawImage, new Rect(0, 0, rawImage.width, rawImage.height), Vector2.zero);
            image.sprite = imageForSprite;

        }
    }



}
