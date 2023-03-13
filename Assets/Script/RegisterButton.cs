using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RegisterButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField Email;

    [SerializeField] private TMP_InputField Password;

    [SerializeField] private TMP_InputField Retype_Password;

    [SerializeField] private Button RegButton;

    public event Action <string, string> GetRegisterInfo;

    private void OnEnable()
    {
        RegButton.onClick.AddListener(Reg_Account);
    }

    private void OnDisable()
    {
        RegButton.onClick.RemoveListener(Reg_Account);
    }

    private void Reg_Account()
    {
        if (string.IsNullOrEmpty(Email.text) || string.IsNullOrEmpty(Password.text) || string.IsNullOrEmpty(Retype_Password.text))
        {
            return;
        }
        if(Password.text != Retype_Password.text)
        {
            Password.text = "";
            Retype_Password.text = "";
            Debug.LogError("Password and Retype Password do not match.");
            return;
        }
        GetRegisterInfo?.Invoke(Email.text, Password.text);
        Password.text = "";
        Retype_Password.text = "";
    }
}
