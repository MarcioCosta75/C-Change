using Firebase.Firestore;
using Firebase.Extensions; // Necessário para ContinueWithOnMainThread
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateButton : MonoBehaviour
{
    public Button yourButton;
    public TextMeshProUGUI buttonText;
    public Color defaultColor;
    public Color clickedColor;
    private bool clicked = false;

    private FirebaseFirestore db;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        yourButton.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
        clicked = !clicked;
        buttonText.text = clicked ? "Inscrito" : "Participar";
        yourButton.GetComponent<Image>().color = clicked ? clickedColor : defaultColor;

        // Obtem a referência do documento
        DocumentReference docRef = db.Collection("eventos").Document("KxnG50dpcwFawTOA4S9n");

        // Cria um update para o campo específico dentro do mapa 'refeicoes_quentes'
        Dictionary<string, object> updates = new Dictionary<string, object>
        {
            { "refeicoes_quentes.voluntarios_inscritos", FieldValue.Increment(clicked ? 1 : -1) }
        };

        docRef.UpdateAsync(updates).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Erro ao atualizar o campo voluntarios_inscritos: " + task.Exception);
            }
            else
            {
                Debug.Log("Campo voluntarios_inscritos atualizado com sucesso.");
            }
        });
    }
}