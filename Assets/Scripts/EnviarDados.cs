using System.Collections;
using UnityEngine;
using TMPro; // Importa o namespace do TextMeshPro
using UnityEngine.Networking;

// Define uma classe serializável para conter os dados do formulário
[System.Serializable]
public class FormData
{
    public string nome;
    public string data_nascimento;
    public string sexo;
    public string morada;
    public string carta_conducao;
    public string especialidade;
    public string email;
}

public class EnviarDados : MonoBehaviour
{
    // Referências para os campos de entrada do formulário
    public TMP_InputField nomeField;
    public TMP_InputField dataNascimentoField;
    public TMP_InputField sexoField;
    public TMP_InputField moradaField;
    public TMP_InputField cartaConducaoField;
    public TMP_InputField especialidadeField;
    public TMP_InputField emailField;

    // URL para onde os dados do formulário serão enviados
    private string url = "https://hook.eu2.make.com/epn3epbj67bg6g8usadurg33ix2m3p08";

    // Método chamado pelo botão de envio
    public void OnEnviarClick()
    {
        StartCoroutine(EnviarDadosCoroutine());
    }

    IEnumerator EnviarDadosCoroutine()
    {
        // Cria uma instância de FormData e preenche com os dados dos campos de entrada
        FormData formData = new FormData
        {
            nome = nomeField.text,
            data_nascimento = dataNascimentoField.text,
            sexo = sexoField.text,
            morada = moradaField.text,
            carta_conducao = cartaConducaoField.text,
            especialidade = especialidadeField.text,
            email = emailField.text
        };

        // Serializa o objeto formData para uma string JSON
        string jsonBody = JsonUtility.ToJson(formData);
        Debug.Log(jsonBody); // Imprime o JSON no console para depuração

        // Prepara o pedido HTTP POST
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, "POST"))
        {
            // Configura o corpo do pedido e o cabeçalho para conteúdo JSON
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonBody);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Envia o pedido e aguarda a resposta
            yield return webRequest.SendWebRequest();

            // Verifica se houve erro na requisição
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                Debug.Log("Formulário enviado com sucesso! Resposta: " + webRequest.downloadHandler.text);
            }
        }
    }
}