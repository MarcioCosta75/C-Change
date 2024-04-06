using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using TMPro;
using System;

public class DatabaseManager : MonoBehaviour
{
    public TMP_InputField nomeField;
    public TMP_InputField dataNascimentoField;
    public TMP_InputField sexoField;
    public TMP_InputField moradaField;
    public TMP_InputField cartaConducaoField;
    public TMP_InputField especialidadeField;
    public TMP_InputField emailField;

    public TMP_Text nomeText;
    public TMP_Text data_nascimentoText;
    public TMP_Text sexoText;
    public TMP_Text moradaText;
    public TMP_Text cartaConducaoText;
    public TMP_Text especialidadeText;
    public TMP_Text emailText;

    //private string userID;
    public static string userID;


    private DatabaseReference dbReference;
    // Start is called before the first frame update
    void Start()
    {
        //userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    public void CreateUser()
    {
        User newUser = new User(nomeField.text, dataNascimentoField.text, sexoField.text, moradaField.text, cartaConducaoField.text, especialidadeField.text, emailField.text);
        string json = JsonUtility.ToJson(newUser);

        // Usando Push() para criar um novo usuário com um ID único
        dbReference.Child("Voluntarios").Child(DatabaseManager.userID).SetRawJsonValueAsync(json);
    }

    public IEnumerator Getnome(Action<string> OnCallback)
    {
        var userNameData = dbReference.Child("Voluntarios").Child(userID).Child("nome").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            OnCallback.Invoke(snapshot.Value.ToString());
        }
    }

    public IEnumerator Getdata_nascimento(Action<string> OnCallback)
    {
        var userNameData = dbReference.Child("Voluntarios").Child(userID).Child("data_nascimento").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            OnCallback.Invoke(snapshot.Value.ToString());
        }
    }

    public IEnumerator Getsexo(Action<string> OnCallback)
    {
        var userNameData = dbReference.Child("Voluntarios").Child(userID).Child("sexo").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            OnCallback.Invoke(snapshot.Value.ToString());
        }
    }


    public IEnumerator Getmorada(Action<string> OnCallback)
    {
        var userNameData = dbReference.Child("Voluntarios").Child(userID).Child("morada").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            OnCallback.Invoke(snapshot.Value.ToString());
        }
    }

    public IEnumerator Getcarta_conducao(Action<string> OnCallback)
    {
        var userNameData = dbReference.Child("Voluntarios").Child(userID).Child("carta_conducao").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            OnCallback.Invoke(snapshot.Value.ToString());
        }
    }

    public IEnumerator Getespecialidade(Action<string> OnCallback)
    {
        var userNameData = dbReference.Child("Voluntarios").Child(userID).Child("especialidade").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            OnCallback.Invoke(snapshot.Value.ToString());
        }
    }

    public IEnumerator Getemail(Action<string> OnCallback)
    {
        var userNameData = dbReference.Child("Voluntarios").Child(userID).Child("email").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            OnCallback.Invoke(snapshot.Value.ToString());
        }
    }

    public void GetUserInfo()
    {
        StartCoroutine(Getnome((string nome) =>
        {
            nomeText.text = "Nome:" + nome;
        }));

        StartCoroutine(Getdata_nascimento((string data_nascimento) =>
        {
            data_nascimentoText.text = "Data de Nacimento:" + data_nascimento;
        }));

        StartCoroutine(Getsexo((string sexo) =>
        {
            sexoText.text = "Sexo:" + sexo;
        }));

        StartCoroutine(Getmorada((string morada) =>
        {
            moradaText.text = "Morada:" + morada;
        }));

        StartCoroutine(Getcarta_conducao((string carta_conducao) =>
        {
            cartaConducaoText.text = "Carta de Condução:" + carta_conducao;
        }));

        StartCoroutine(Getespecialidade((string especialidade) =>
        {
            especialidadeText.text = "Especialidade:" + especialidade;
        }));

        StartCoroutine(Getemail((string email) =>
        {
            emailText.text = "Email:" + email;
        }));
    }

    public void Updatenome()
    {
        dbReference.Child("Voluntarios").Child(DatabaseManager.userID).Child("Nome").SetValueAsync(nomeField.text);
    }

    public void Updatedata_nascimento()
    {
        dbReference.Child("Voluntarios").Child(DatabaseManager.userID).Child("Data de Nascimento").SetValueAsync(nomeField.text);
    }

    public void Updatesexo()
    {
        dbReference.Child("Voluntarios").Child(DatabaseManager.userID).Child("Sexo").SetValueAsync(nomeField.text);
    }

    public void Updatemorada()
    {
        dbReference.Child("Voluntarios").Child(DatabaseManager.userID).Child("Morada").SetValueAsync(nomeField.text);
    }

    public void UpdatecartaConducao()
    {
        dbReference.Child("Voluntarios").Child(DatabaseManager.userID).Child("Carta de Condução").SetValueAsync(nomeField.text);
    }

    public void Updateespecialidade()
    {
        dbReference.Child("Voluntarios").Child(DatabaseManager.userID).Child("Especialidade").SetValueAsync(nomeField.text);
    }

    public void Updateemail()
    {
        dbReference.Child("Voluntarios").Child(DatabaseManager.userID).Child("Email").SetValueAsync(nomeField.text);
    }
}