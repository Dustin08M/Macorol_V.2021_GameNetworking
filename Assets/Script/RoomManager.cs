using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using PlayFab.ClientModels;
using PlayFab;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField playerPassword;
    [SerializeField] private TMP_InputField bonusPointsInput;
    [SerializeField] private int PlayerCount;

    public Text usernameText;
    public Text playerNumberText;
    public Text bonusPointsText;
    public RawImage hostImage;

    private AccountVerifyer Authentication;

    private bool isHost = false;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom()
    {
        string username = usernameInput.text;
        string password = playerPassword.text;
        int bonusPoints = int.Parse(bonusPointsInput.text);

        if (PhotonNetwork.InLobby)
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = (byte)PlayerCount;
            PhotonNetwork.CreateRoom(username, options);
        }
        else
        {
            Debug.Log("Not in lobby yet");
        }

    }

    public void JoinRoom()
    {
        string username = usernameInput.text;
        PhotonNetwork.JoinRoom(username);
    }

    public override void OnJoinedRoom()
    {
        string username = usernameInput.text;
        string password = playerPassword.text;
        int bonusPoints = int.Parse(bonusPointsInput.text);

        PhotonNetwork.NickName = username;

        /*usernameText.text = "Username: " + username;
        playerNumberText.text = "Player Number: " + playerNumber;
        bonusPointsText.text = "Bonus Points: " + bonusPoints;*/

        if (PhotonNetwork.IsMasterClient)
        {
            isHost = true;
            hostImage.color = Color.green;
        }
        else
        {
            isHost = false;
            hostImage.color = Color.red;
        }
        PhotonNetwork.LoadLevel(2);
    }

    public void TransferHost()
    {
        if (isHost)
        {
            Player[] players = PhotonNetwork.PlayerList;
            foreach (Player player in players)
            {
                if (!player.IsMasterClient)
                {
                    PhotonNetwork.SetMasterClient(player);
                    break;
                }
            }
        }
    }
}
