using Firebase.Auth;
using TMPro;
using UnityEngine;

public class DisplayName : MonoBehaviour
{
    public TextMeshProUGUI displayNameText; // Referência ao seu TextMeshProUGUI no Unity

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
            // O DisplayName pode ser nulo se não for definido, então você pode querer verificar isso
            string name = user.DisplayName ?? "Usuário sem nome";
            displayNameText.text = name;
        }
        else
        {
            displayNameText.text = "Usuário não logado";
        }
    }
}