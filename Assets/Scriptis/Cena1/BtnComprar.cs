using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class BtnComprar : MonoBehaviour
{
    //Variaveis
    [SerializeField] private string urlValorItem;
    [SerializeField] private float itemValor;
    public int cenaIndex = 0;
   

    //Metodos
    void Start()
    {
        ValLoad();
    }
    public void ValLoad()
    {
        StartCoroutine("LoadText");
    }

    public void BtnTryComprar()
    {
        if (cenaIndex == 0)
        {
            if (ComprasGerenciador.instance.moedasPlayer >= itemValor)
            {
                string newValue = (ComprasGerenciador.instance.moedasPlayer - itemValor).ToString();
                ComprasGerenciador.instance.SobrescreverArquivo(newValue);
                ComprasGerenciador.instance.mensagens.text = "Compra concluida!!!";
                StartCoroutine("Timer");
            }
            else
            {
                ComprasGerenciador.instance.mensagens.text = "Não foi possivel efetuar a compra, moedas insuficientes";
                StartCoroutine("Timer");
            }
        }
        else if (cenaIndex == 1)
        {
            if (GetMoedas.instance.moedasPlayer >= itemValor)
            {
                GetMoedas.instance.btnComprar(itemValor);
                GetMoedas.instance.mensagens.text = "Compra concluida!!!";
                StartCoroutine("Timer");
            }
            else
            {
                GetMoedas.instance.mensagens.text = "Não foi possivel efetuar a compra, moedas insuficientes";
                StartCoroutine("Timer");
            }
        }
        
    }

    IEnumerable Timer()
    {
        yield return new WaitForSeconds(3);
        if (cenaIndex == 0)
        {
            ComprasGerenciador.instance.mensagens.text = "";
        }
        else if (cenaIndex == 1)
        {
            GetMoedas.instance.mensagens.text = "";
        }
       
    }

    IEnumerator LoadText() {
        
        UnityWebRequest request = UnityWebRequest.Get(urlValorItem);
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success){
            if (float.TryParse(request.downloadHandler.text, NumberStyles.Float, CultureInfo.InvariantCulture, out float m))
            {
            itemValor = m;
            }
        }Debug.Log(itemValor);
    }
}
