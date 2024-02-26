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
    public NeutralItems[] NeutralItems;
    public TextMeshProUGUI CostText;
    public int cost;

    public int StartGold = 600;
    private int StartGoldSave;
    public SmallItem[] SmallItems;
    public Image[] StartBuildImages;
    public Image[] ItemSprites = new Image[6];
    public Image[] NeutralItemSprites;

    public int[] CurrentItemIDs = new int[6];
    public int[] CurrentStartItemIDs = new int[6];


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
                ItemSprites[i].sprite = items[randomID].Icon;
                cost += items[randomID].cost;
                CurrentItemIDs[i] = randomID;
                CostText.text = "Total Costs: " + cost;
            }
            else
            {
                int randomBootsID = Random.Range(0, boots.Length);
                ItemSprites[i].sprite = boots[randomBootsID].Icon;
                cost += boots[randomBootsID].cost;
                CurrentItemIDs[i] = randomBootsID;
            }

           
        }

        

    }

    public void CreateNewStartBuild()
    {
        int iteration = 0;


        for (int i = 0; i < StartBuildImages.Length; i++)
        {
            CurrentStartItemIDs[i] = 900;
        }
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
            StartBuildImages[iteration].sprite = SmallItems[randomID].Icon;
            StartBuildImages[iteration].enabled = true;
            StartGold -= SmallItems[randomID].cost;

            CurrentStartItemIDs[iteration] = randomID;

            iteration++; // Nächsten Slot für das nächste Item vorbereiten
        }

        StartGold = StartGoldSave;

        // Die Schleife endet automatisch, wenn kein Gold mehr vorhanden ist oder alle Slots gefüllt sind
    }

    public void CreateNewNeutralItemBuild()
    {
        List<NeutralItems> NeutralItemPool = new List<NeutralItems>();
        int currentImage = 0;

        for (int i = 1; i < 6; i++)
        {
            NeutralItemPool = FindNeutralItemByTier(i); 

            for (int j = 0; j < 3; j++)
            {
                int randomNeutralItemID = Random.Range(0, NeutralItemPool.Count);
                NeutralItems currentRandomNeutral = NeutralItemPool[randomNeutralItemID];
                NeutralItemSprites[currentImage].sprite = currentRandomNeutral.Icon;

                NeutralItemPool.Remove(currentRandomNeutral);

                currentImage++;
            }
        }
        NeutralItemPool.Clear();
    }

    private List<NeutralItems> FindNeutralItemByTier(int Tier)
    {
        List<NeutralItems> FoundNeutralItems = new List<NeutralItems>();

       

        for (int i = 0; i < NeutralItems.Length; i++)
        {
            if (NeutralItems[i].Tier == Tier)
            {
                FoundNeutralItems.Add(NeutralItems[i]);              

            }
        }

        return FoundNeutralItems;
    }
    




    //public void getImage(string URL, Image image)
    //{
    //    StartCoroutine(getImageFromURL(URL, image));        
    //}



    //IEnumerator getImageFromURL(string URL_, Image image)
    //{
    //    UnityWebRequest request = UnityWebRequestTexture.GetTexture(URL_);
    //    yield return request.SendWebRequest();
    //    if (request.isNetworkError || request.isHttpError)
    //        Debug.Log(request.error);
    //    else
    //    {
    //        Texture2D rawImage = ((DownloadHandlerTexture)request.downloadHandler).texture;
    //        Sprite imageForSprite = Sprite.Create(rawImage, new Rect(0, 0, rawImage.width, rawImage.height), Vector2.zero);
    //        image.sprite = imageForSprite;
    //
    //    }
    //}



}
