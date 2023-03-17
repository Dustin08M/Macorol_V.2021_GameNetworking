using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class LoginButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField Email;
    [SerializeField] private TMP_InputField Password;
    [SerializeField] private Button _login;

    public event Action<string, string> LoginEvent;

    private void OnEnable()
    {
        _login.onClick.AddListener(LoginAccount);
    }
    private void OnDisable()
    {
        _login.onClick.RemoveListener(LoginAccount);
    }

    private void LoginAccount()
    {
        if (string.IsNullOrEmpty(Email.text) || string.IsNullOrEmpty(Password.text))
        {
            Debug.LogError("Input required field");
            return;
        }
        LoginEvent?.Invoke(Email.text, Password.text);

        Email.text = "";
        Password.text = "";
    }
}
