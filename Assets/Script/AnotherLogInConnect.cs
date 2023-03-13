using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class AnotherLogInConnect : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject loginPanel;
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField playerNumberInput;
    [SerializeField] Button loginButton;

    private bool isConnected;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        loginPanel.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        loginButton.onClick.RemoveListener(OnLoginButtonClicked);
    }

    private void OnLoginButtonClicked()
    {
        string username = usernameInput.text;
        string playerNumber = playerNumberInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(playerNumber))
        {
            Debug.LogError("Username and player number cannot be empty.");
            return;
        }

        PhotonNetwork.NickName = username;
        isConnected = true;

        loginPanel.SetActive(false);

        if (PhotonNetwork.IsConnectedAndReady)
        {
            JoinOrCreateRoom();
        }
    }

    private void JoinOrCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;

        PhotonNetwork.JoinOrCreateRoom("PunBasics-Room for 1", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel("PunBasics-Room for 1");
        }

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);

        if (isConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
