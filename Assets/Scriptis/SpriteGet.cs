using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class SpriteGet : MonoBehaviour
{
    //Variaveis
    public Image img;
    [SerializeField] private string url;

    //Metodos
    public void Start()
    {
        ImgProduct();
    }

    public void ImgProduct()
    {
        Debug.Log("ImgProduct");
        StartCoroutine("LoadImage");
    }
     
     IEnumerator LoadImage(){
        
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Rect rect = new Rect(0, 0, tex.width, tex.height);
            Vector2 center = new Vector2(tex.width / 2.0f, tex.height / 2.0f);
            Sprite sprite = Sprite.Create(tex, rect, center);
            img.sprite = sprite;
             Debug.Log("Fim");
        }
    }
    
}
