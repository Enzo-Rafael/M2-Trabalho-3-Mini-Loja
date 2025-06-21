using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.Networking;
using System.Globalization;

public class GetMoedas : MonoBehaviour
{
    public static GetMoedas instance;
    public TMP_Text txtLabel;
    public TMP_Text mensagens;
    [SerializeField] private String url;
    public float moedasPlayer;

    //Metodos
    void Start()
    {
        instance = this;
        LoadCoins();
        
    }
    public void LoadCoins()
    {
         StartCoroutine("LoadText");
    }
    // Update is called once per frame

    public void UpdateWallet()
    {
       txtLabel.text =  moedasPlayer.ToString();
    }

    public void btnComprar(float iValue)
    {
        moedasPlayer = moedasPlayer - iValue;
        UpdateWallet();
    }

    IEnumerator LoadText() {
        
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            if (float.TryParse(request.downloadHandler.text, NumberStyles.Float, CultureInfo.InvariantCulture, out float m))
                moedasPlayer = m;
            UpdateWallet();
        }
    }

}