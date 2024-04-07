using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase.Firestore;
using Firebase.Extensions; // Para usar ContinueWithOnMainThread
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
    public TextMeshProUGUI eventParticipantsNeededText;
    public TextMeshProUGUI eventParticipantsSignedText; // Certifique-se de que este campo está vinculado no Inspector
    public Image eventImage; // Certifique-se de que este campo está vinculado no Inspector

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
                    Debug.Log("Documento encontrado, atualizando a UI.");

                    Dictionary<string, object> eventData = document.ToDictionary();
                    Dictionary<string, object> refeicoesQuentes = eventData["refeicoes_quentes"] as Dictionary<string, object>;

                    eventNameText.text = refeicoesQuentes["nome"].ToString();
                    eventDateText.text = refeicoesQuentes["data"].ToString();
                    eventTimeText.text = refeicoesQuentes["hora"].ToString();
                    eventLocationText.text = refeicoesQuentes["localizacao"].ToString();
                    eventMentorText.text = refeicoesQuentes["mentor"].ToString();
                    eventDescriptionText.text = refeicoesQuentes["descricao"].ToString();
                    eventParticipantsNeededText.text = refeicoesQuentes["voluntarios_necessarios"].ToString();
                    eventParticipantsSignedText.text = refeicoesQuentes["voluntarios_inscritos"].ToString();

                    StartCoroutine(LoadImage(refeicoesQuentes["bg_image"].ToString()));
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