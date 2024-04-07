using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase.Firestore;
using Firebase.Extensions; // Necessário para ContinueWithOnMainThread
using TMPro;
using System.Collections.Generic;

public class RefeicoesQuentes : MonoBehaviour
{
    // Referências da UI no Unity
    public TextMeshProUGUI eventNameText;
    public TextMeshProUGUI eventDateText;
    public TextMeshProUGUI eventTimeText;
    public TextMeshProUGUI eventLocationText;
    public TextMeshProUGUI eventMentorText;
    public TextMeshProUGUI eventDescriptionText;
    public TextMeshProUGUI eventParticipantsText;
    public Image eventImage;

    FirebaseFirestore db;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        FetchEventDetails();
    }

    void FetchEventDetails()
    {
        DocumentReference docRef = db.Collection("eventos").Document("KxnG50dpcwFawTOA4S9n");
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Erro ao buscar detalhes do evento: " + task.Exception);
            }
            else
            {
                DocumentSnapshot document = task.Result;
                if (document.Exists)
                {
                    Dictionary<string, object> refeicoesQuentes = document.GetValue<Dictionary<string, object>>("refeicoes_quentes");
                    eventNameText.text = refeicoesQuentes["nome"].ToString();
                    eventDateText.text = refeicoesQuentes["data"].ToString();
                    eventTimeText.text = refeicoesQuentes["hora"].ToString();
                    eventLocationText.text = refeicoesQuentes["localizacao"].ToString();
                    eventMentorText.text = refeicoesQuentes["mentor"].ToString();
                    eventDescriptionText.text = refeicoesQuentes["descricao"].ToString();

                    // Verifique se o campo 'voluntarios_necessarios' existe antes de tentar acessá-lo
                    if (document.ContainsField("voluntarios_necessarios"))
                    {
                        string voluntariosNecessarios = document.GetValue<string>("voluntarios_necessarios");
                        eventParticipantsText.text = voluntariosNecessarios;
                        Debug.Log("Voluntários necessários: " + voluntariosNecessarios); // Para depuração
                    }
                    else
                    {
                        Debug.LogError("O campo 'voluntarios_necessarios' não existe no documento.");
                    }

                    // Carregar imagem de fundo
                    if (refeicoesQuentes.ContainsKey("bg_image") && refeicoesQuentes["bg_image"] != null)
                    {
                        StartCoroutine(LoadImage(refeicoesQuentes["bg_image"].ToString()));
                    }
                }
                else
                {
                    Debug.LogError("Documento não existe!");
                }
            }
        });
    }

    IEnumerator LoadImage(string imageUrl)
    {
        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return imageRequest.SendWebRequest();

        if (imageRequest.result == UnityWebRequest.Result.ConnectionError || imageRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Erro ao carregar imagem: " + imageRequest.error);
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(imageRequest);
            eventImage.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}
