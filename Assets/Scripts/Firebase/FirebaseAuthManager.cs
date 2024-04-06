using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using TMPro;

public class FirebaseAuthManager : MonoBehaviour
{
    // Firebase variable
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;

    // Login Variables
    [Space]
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    // Registration Variables
    [Space]
    [Header("Registration")]
    public TMP_InputField nameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField confirmPasswordRegisterField;
    // Registration Additional Fields
    [Header("Registration Additional Fields")]
    public TMP_InputField birthDateRegisterField;
    public TMP_InputField genderRegisterField;
    public TMP_InputField addressRegisterField;
    public TMP_InputField drivingLicenseRegisterField;
    public TMP_InputField specialtyRegisterField;

    private void Awake()
    {
        // Check that all of the necessary dependencies for firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all firebase dependencies: " + dependencyStatus);
            }
        });
    }

    void InitializeFirebase()
    {
        // Set the default instance object
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    public void Login()
    {
        StartCoroutine(LoginAsync(emailLoginField.text, passwordLoginField.text));
    }

    private IEnumerator LoginAsync(string email, string password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogError(loginTask.Exception);

            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;

            string failedMessage = "Login Failed! Because ";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    failedMessage += "Email is invalid";
                    break;
                case AuthError.WrongPassword:
                    failedMessage += "Wrong Password";
                    break;
                case AuthError.MissingEmail:
                    failedMessage += "Email is missing";
                    break;
                case AuthError.MissingPassword:
                    failedMessage += "Password is missing";
                    break;
                default:
                    failedMessage = "Login Failed";
                    break;
            }

            Debug.Log(failedMessage);
        }
        else
        {
            user = loginTask.Result.User;

            Debug.LogFormat("{0} You Are Successfully Logged In", user.DisplayName);

            References.userName = user.DisplayName;
            UnityEngine.SceneManagement.SceneManager.LoadScene("HomePage");
        }
    }

    public void Register()
    {
        StartCoroutine(RegisterAsync(
            nameRegisterField.text,
            emailRegisterField.text,
            passwordRegisterField.text,
            confirmPasswordRegisterField.text,
            birthDateRegisterField.text,
            genderRegisterField.text,
            addressRegisterField.text,
            drivingLicenseRegisterField.text,
            specialtyRegisterField.text
        ));
    }

    private IEnumerator RegisterAsync(string name, string email, string password, string confirmPassword, string birthDate, string gender, string address, string drivingLicense, string specialty)
    {
        if (passwordRegisterField.text != confirmPasswordRegisterField.text)
        {
            Debug.LogError("Password does not match");
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                Debug.LogError(registerTask.Exception);
            }
            else
            {
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = name };
                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                if (updateProfileTask.Exception == null)
                {
                    // Adicionar aqui a lógica de armazenamento dos dados adicionais no Firestore ou Realtime Database
                    Debug.Log("Registration and Profile Update Successful");

                    // Exemplo de como enviar dados para Firestore (implementar separadamente)
                    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
                    DocumentReference docRef = db.Collection("voluntarios").Document(user.UserId);
                    Dictionary<string, object> userAdditionalInfo = new Dictionary<string, object>
                    {
                        { "nome", name },
                        { "data_nascimento", birthDate },
                        { "sexo", gender },
                        { "morada", address },
                        { "carta_conducao", drivingLicense },
                        { "especialidade", specialty },
                        { "email", email }
                    };
                    docRef.SetAsync(userAdditionalInfo);

                    UIManager.Instance.OpenLoginPanel(); // Ajuste conforme a sua lógica de UI
                }
                else
                {
                    // Se a atualização do perfil falhar, considere deletar o usuário ou tratar o erro de outra forma
                    Debug.LogError(updateProfileTask.Exception);
                }
            }
        }
    }
}
