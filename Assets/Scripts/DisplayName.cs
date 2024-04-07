using Firebase.Auth;
using TMPro;
using UnityEngine;

public class DisplayName : MonoBehaviour
{
    public TextMeshProUGUI displayNameText; // Refer�ncia ao seu TextMeshProUGUI no Unity

    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        UpdateDisplayName();
    }

    void UpdateDisplayName()
    {
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            // O DisplayName pode ser nulo se n�o for definido, ent�o voc� pode querer verificar isso
            string name = user.DisplayName ?? "Usu�rio sem nome";
            displayNameText.text = name;
        }
        else
        {
            displayNameText.text = "Usu�rio n�o logado";
        }
    }
}