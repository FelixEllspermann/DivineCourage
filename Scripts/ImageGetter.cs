using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageGetter : MonoBehaviour
{
    public Image TestImage;
    public SmallItem[] Items;

    public void getImage()
    {
        
        StartCoroutine(getImageFromURL(Items[Random.Range(0, Items.Length)].URL));
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
            TestImage.sprite = imageForSprite;

        }
    }
}
