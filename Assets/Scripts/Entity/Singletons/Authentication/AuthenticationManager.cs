using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using TMPro;
using Persistence;

namespace Authentication
{
    public class AuthenticationManager : Singleton<AuthenticationManager>
    {
        [Header("Login")]
        [SerializeField] private TMP_InputField loginEmailText;
        [SerializeField] private TMP_InputField loginPasswordText;
        [SerializeField] private Button loginButton;
        [Header("Register")]
        [SerializeField] private TMP_InputField registerEmailText;
        [SerializeField] private TMP_InputField registerPasswordText;
        [SerializeField] private TMP_InputField registerVerifyPasswordText;
        [SerializeField] private Button registerButton;

        private FirebaseAuth auth;

        // Start is called before the first frame update
        void Start()
        {
            auth = FirebaseAuth.DefaultInstance;
            loginButton.onClick.AddListener(Login);
            registerButton.onClick.AddListener(Register);
        }

        void Login()
        {
            string email = loginEmailText.text;
            string password = loginPasswordText.text;

            StartCoroutine(LoginCoroutine(email, password));
        }

        void Register()
        {
            string email = registerEmailText.text;
            string password = registerPasswordText.text;
            string verifyPassword = registerVerifyPasswordText.text;

            // Validate text
            if (!IsValidEmail(email))
            {
                ModalWindowManager.Instance.SetTitleAndMessage("Invalid Email", "Email is invalid");
                ModalWindowManager.Instance.ShowModalWindow();
                return;
            }

            if (!IsValidPassword(password))
            {
                ModalWindowManager.Instance.SetTitleAndMessage("Invalid Password", "Password is invalid");
                ModalWindowManager.Instance.ShowModalWindow();
                return;
            }

            if (verifyPassword != password)
            {
                ModalWindowManager.Instance.SetTitleAndMessage("Invalid Verify Password", "Verify Password does not match");
                ModalWindowManager.Instance.ShowModalWindow();
                return;
            }


            StartCoroutine(RegisterCoroutine(email, password));
        }

        IEnumerator LoginCoroutine(string email, string password)
        {
            Task<FirebaseUser> task = auth.SignInWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => task.IsCompleted);

            if (task.Exception != null)
            {
                ModalWindowManager.Instance.SetTitleAndMessage("Invalid Login", "Login credential invalid");
                ModalWindowManager.Instance.ShowModalWindow();
                yield break;
            }

            PersistenceManager.Instance.LoadGame(task.Result.UserId);
        }

        IEnumerator RegisterCoroutine(string email, string password)
        {
            Task<FirebaseUser> task = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => task.IsCompleted);

            if (task.Exception != null)
            {
                ModalWindowManager.Instance.SetTitleAndMessage("Invalid Register", "Register credential invalid");
                ModalWindowManager.Instance.ShowModalWindow();
                yield break;
            }

            ModalWindowManager.Instance.SetTitleAndMessage("Register Succesful", "Succesfully registered");
            ModalWindowManager.Instance.ShowModalWindow();
        }

        bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        bool IsValidPassword(string password)
        {
            return password.Length >= 8 && Regex.IsMatch(password, @"^[a-zA-Z0-9]+$");
        }
    }
}
