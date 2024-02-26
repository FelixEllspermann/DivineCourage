using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// Diese Klasse verwaltet das Laden von gespeicherten Builds, inklusive Helden, Fähigkeiten und Gegenständen.
public class LoadSavedBuild : MonoBehaviour
{
    // Referenzen zu Generator-Scripts und dem ScoreManager
    public ItemGenerator ItemGenerator;
    public HeroGenerator HeroGenerator;
    public SkillGenerator SkillGenerator;
    public ScoreManager ScoreManager;

    // UI-Elemente
    public Image SavedBuildIcon;

    // Interne Variable zur Speicherung der geladenen Daten
    private SavedBuildData Picture;

    // Beim Start des Skriptes wird versucht, einen gespeicherten Build zu laden.
    private void Start()
    {
        string path = Application.persistentDataPath + "/SavedBuild.Beephi";
        if (File.Exists(path))
        {
            Picture = LoadPictureFromSave();
            StartCoroutine(getImageFromURL(HeroGenerator.Heroes[Picture.HeroID].URL));
        }
    }

    // Coroutine zum Laden eines Bildes aus einer URL.
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
            SavedBuildIcon.sprite = imageForSprite;
        }
    }

    // Lädt einen Build aus der gespeicherten Datei.
    public void LoadBuildFromSave()
    {
        string path = Application.persistentDataPath + "/SavedBuild.Beephi";
        if (File.Exists(path))
        {
            SavedBuildData LoadedBuild = SaveSystem.LoadBuild();
            Debug.Log($"ItemGenerator exists: {ItemGenerator != null}");
            Debug.Log($"HeroGenerator exists: {HeroGenerator != null}");

            // Lädt die Gegenstandsbilder.
            for (int i = 0; i < LoadedBuild.ItemIDs.Length; i++)
            {
                if (i == 5) // Spezialfall für Boots
                {
                    ItemGenerator.ItemSprites[i].sprite = ItemGenerator.boots[LoadedBuild.ItemIDs[i]].Icon;
                }
                else
                {
                    ItemGenerator.ItemSprites[i].sprite = ItemGenerator.items[LoadedBuild.ItemIDs[i]].Icon;
                }
            }

            // Setzt die Bilder der Startgegenstände zurück.
            for (int i = 0; i < ItemGenerator.StartBuildImages.Length; i++)
            {
                ItemGenerator.StartBuildImages[i].sprite = null;
                ItemGenerator.StartBuildImages[i].enabled = false;
            }

            // Aktualisiert die Bilder der Startgegenstände.
            for (int i = 0; i < LoadedBuild.StartItemIDs.Length; i++)
            {
                if (LoadedBuild.StartItemIDs[i] == 900) // Spezialfall
                {
                    ItemGenerator.StartBuildImages[i].enabled = false;
                }
                else
                {
                    ItemGenerator.StartBuildImages[i].sprite = ItemGenerator.SmallItems[LoadedBuild.StartItemIDs[i]].Icon;
                    ItemGenerator.StartBuildImages[i].enabled = true;
                }
            }

            // Lädt das Heldenbild und generiert die Fähigkeiten.
            HeroGenerator.HeroImage.sprite = HeroGenerator.Heroes[LoadedBuild.HeroID].Icon;
            SkillGenerator.GeneratePredeterminedSkillBuild(LoadedBuild.skillPriority);

            // Aktualisiert den Score.
            ScoreManager.RawScore = LoadedBuild.Score;
            ScoreManager.ScoreText.text = "Score: " + LoadedBuild.Score.ToString();
        }
    }

    // Lädt ein Bild aus dem gespeicherten Build.
    public SavedBuildData LoadPictureFromSave()
    {
        string path = Application.persistentDataPath + "/SavedBuild.Beephi";
        if (File.Exists(path))
        {
            SavedBuildData LoadedBuild = SaveSystem.LoadBuild();
            HeroGenerator.HeroImage.sprite = HeroGenerator.Heroes[LoadedBuild.HeroID].Icon;
            return LoadedBuild;
        }
        return null; // Gibt null zurück, falls keine Datei existiert.
    }
    }

    // Speichert den aktuellen Build.