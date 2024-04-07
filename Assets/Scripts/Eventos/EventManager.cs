using Firebase.Firestore;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Firebase.Extensions;

public class EventManager : MonoBehaviour
{
    FirebaseFirestore db;

    public GameObject eventDetailPrefab; // Seu prefab de detalhes de evento
    public Transform eventDetailParent; // Onde o prefab ser� instanciado na UI

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        FetchEvents();
    }

    void FetchEvents()
    {
        db.Collection("events").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
                // Tratar erro
            }
            else
            {
                foreach (DocumentSnapshot document in task.Result.Documents)
                {
                    // Cria uma nova inst�ncia do prefab de detalhes de evento
                    GameObject eventDetailObj = Instantiate(eventDetailPrefab, eventDetailParent);

                    // Aqui voc� ajusta os detalhes do evento dinamicamente
                    eventDetailObj.GetComponentInChildren<Text>().text = document.GetValue<string>("name");
                    // Configure outros componentes, como data, hora e bot�o de participa��o
                }
            }
        });
    }

    // Chame este m�todo quando o usu�rio clicar em 'Participar'
    public void RegisterForEvent(string eventId, string userId)
    {
        DocumentReference eventDocRef = db.Collection("events").Document(eventId);
        // Adiciona o ID do usu�rio � lista de participantes
        eventDocRef.UpdateAsync("participants", FieldValue.ArrayUnion(userId)).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log(task.Exception.ToString());
                // Tratar erro
            }
            else
            {
                Debug.Log("Usu�rio registrado com sucesso no evento!");
                // Atualize a UI conforme necess�rio
            }
        });
    }
}