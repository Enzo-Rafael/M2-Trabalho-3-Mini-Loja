using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class PlayerWallet : MonoBehaviour
{
    //Variaveis
    public TMP_Text txtLabel;
    [SerializeField] private String url;
    

    //Metodos
    void Start()
    {
        UpdateWallet();
    }

    // Update is called once per frame
    
    public void UpdateWallet()
    {
        StartCoroutine("LoadText");
    }

    IEnumerator LoadText() {
        
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success){
             txtLabel.text = request.downloadHandler.text;
        }
    }

}
