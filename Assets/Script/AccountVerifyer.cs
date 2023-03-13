using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class AccountVerifyer : MonoBehaviour
{
    [SerializeField] private RegisterButton Register;
    [SerializeField] private LoginButton Login;


    private void OnEnable()
    {
        Login.LoginEvent += CallLoginEvent;
        Register.GetRegisterInfo += CallRegisterEvent;
    }
    private void OnDisable()
    {
        Login.LoginEvent -= CallLoginEvent;
        Register.GetRegisterInfo -= CallRegisterEvent;
    }

    private void CallLoginEvent(string username, string password)
    {
        PlayFabClientAPI.LoginWithEmailAddress
        (new LoginWithEmailAddressRequest()
        {
            Email = username,
            Password = password,
            TitleId = PlayFabSettings.TitleId,
        }, (loginResult) =>{
            Debug.Log("Login Successful");
            PhotonNetwork.NickName = username;
            SceneManager.LoadScene(1);
        }, (errorResult) =>
        {
            Debug.Log($"Failed to login{errorResult.ErrorMessage}");
        }
        );
    }

    private void CallRegisterEvent(string username, string password)
    {
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            TitleId = PlayFabSettings.TitleId,
        },  (LoginSuccess) =>
            {
                PlayFabClientAPI.AddUsernamePassword(new AddUsernamePasswordRequest
                {
                    Email = username,
                    Password = password,
                    Username = LoginSuccess.PlayFabId
                }, (UpdateSuccess) =>
                {
                    Debug.Log("Register Successful");
                    PhotonNetwork.NickName = username;
                    SceneManager.LoadScene(1);
                }, (UpdateFailed) =>
                {
                    var msg = "";
                    foreach (var Variable in UpdateFailed.ErrorDetails)
                    {
                        msg += Variable.Key + "\n";
                        foreach (var item in Variable.Value)
                        {
                            msg += $"#{item}";
                        }
                    }
                    Debug.Log($"Failed to Register: {UpdateFailed.Error} \n {UpdateFailed.ErrorMessage} \n {msg}");
                });
                
            }, (LoginFail) =>
            {
                    Debug.Log($"Unable to Login With Custom Id {LoginFail.ErrorMessage}");
            });
    }
}
