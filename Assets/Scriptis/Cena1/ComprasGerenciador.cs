using UnityEngine;
using TMPro;
using System.IO;
using System;
using System.Globalization;
using Unity.VisualScripting;

public class ComprasGerenciador : MonoBehaviour
{
    //Instancia
    public static ComprasGerenciador instance;
    //Variaveis
    private float matchTime = 0f;
    private float startTime = 1f;
    public TMP_Text mensagens;
    public TMP_Text moedasQantiti;
    public float moedasPlayer;

    [SerializeField] private string nomeDoArquivo = "PlayerWallet.txt";


    //Metodos
    void Start()
    {
        instance = this;
        matchTime = startTime * 60;
        if (ArquivoExiste()) moedasQantiti.text = LerDoArquivo();
        else SeNaoExistir();
        moedasPlayer = Conversor();
    }
    void Update()
    {
        //Debug.Log(GetCaminhoDoArquivo());
        matchTime -= Time.deltaTime;
        if (matchTime <= 0)
        {
            moedasQantiti.text = LerDoArquivo();
            moedasPlayer = Conversor();
            matchTime = startTime * 60;
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            SobrescreverArquivo("0");
        }
    }
    public void OnBtnAddMoeda()//Em teste
    {
        if (ArquivoExiste())
        {
            string text = LerDoArquivo();
            float moedas;
            if (float.TryParse(text, NumberStyles.Float, new CultureInfo("pt-BR"), out moedas))
            {
                string newValue = (moedas + 100).ToString();
                SobrescreverArquivo(newValue);
            }


        }
        else
        {
            SeNaoExistir();
            float moedas = Convert.ToSingle(LerDoArquivo());
            string newValue = (moedas + 100).ToString();
            EscreverNoArquivo(newValue);
        }
        moedasQantiti.text = LerDoArquivo();
        moedasPlayer = Conversor();
    }

    public float Conversor()
    {
        string text = LerDoArquivo();
        float moedas;
        if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out moedas))return moedas;
        
        return moedas;
    }

    public void SeNaoExistir()
    {
        string caminho = GetCaminhoDoArquivo();
        if (!File.Exists(caminho))
        {
            try
            {
                // File.Create retorna um FileStream, que precisa ser fechado.
                // O 'using' garante que o stream seja fechado automaticamente.
                using (FileStream fs = File.Create(caminho))
                {
                    // Não é necessário escrever nada aqui se for apenas para criar o arquivo vazio.
                    // Se você quiser um conteúdo inicial, pode fazer:
                    byte[] info = new System.Text.UTF8Encoding(true).GetBytes("100");
                    // fs.Write(info, 0, info.Length);
                }
                Debug.Log($"Arquivo criado com sucesso: {caminho}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Erro ao criar o arquivo: {e.Message}");
            }
        }
        else
        {
            Debug.Log($"Arquivo já existe: {caminho}");
        }
    }


    public void EscreverNoArquivo(string texto)
    {
        string caminho = GetCaminhoDoArquivo();
        try
        {
            // Usando 'true' no construtor do StreamWriter para 'append' (adicionar ao final)
            using (StreamWriter writer = new StreamWriter(caminho, true))
            {
                writer.WriteLine(texto);
            }
            Debug.Log($"Texto '{texto}' adicionado ao arquivo: {caminho}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao escrever no arquivo: {e.Message}");
        }
    }

    private string GetCaminhoDoArquivo()
    {
        string path = Application.persistentDataPath + "/" + nomeDoArquivo;
        return path;
    }

    public void SobrescreverArquivo(string texto)
    {
        string caminho = GetCaminhoDoArquivo();
        try
        {
            // Usando 'false' no construtor do StreamWriter para 'overwrite' (sobrescrever)
            using (StreamWriter writer = new StreamWriter(caminho, false))
            {
                writer.Write(texto); // Use Write em vez de WriteLine se quiser apenas o texto sem nova linha no final
            }
            Debug.Log($"Arquivo sobrescrito com: '{texto}' em: {caminho}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao sobrescrever o arquivo: {e.Message}");
        }
         moedasQantiti.text = LerDoArquivo();
    }

    public bool ArquivoExiste()
    {
        return File.Exists(GetCaminhoDoArquivo());
    }

    public string LerDoArquivo()
    {
        string caminho = GetCaminhoDoArquivo();
        if (File.Exists(caminho))
        {
            try
            {
                using (StreamReader reader = new StreamReader(caminho))
                {
                    string conteudo = reader.ReadToEnd(); // Lê todo o conteúdo do arquivo
                    return conteudo;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Erro ao ler o arquivo: {e.Message}");
                return "";
            }
        }
        else
        {
            Debug.LogWarning($"Arquivo não encontrado no caminho: {caminho}");
            return "";
        }
    }
}
