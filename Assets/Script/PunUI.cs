using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PunUI : MonoBehaviourPunCallbacks
{
    public Text playerNameText;
    public Text actorNumberText;
    public Text bonusPointsText;
    public RawImage hostIconImage;
    public Button transferHostButton;

    private bool isHost;

    private void Start()
    {
        playerNameText.text = PhotonNetwork.LocalPlayer.NickName;

        actorNumberText.text = "Actor Number: " + PhotonNetwork.LocalPlayer.ActorNumber.ToString();

        bonusPointsText.text = "Bonus Points: " + PhotonNetwork.LocalPlayer.UserId;

        isHost = PhotonNetwork.IsMasterClient;
        hostIconImage.color = isHost ? Color.red : Color.green;

        transferHostButton.interactable = isHost;

        if (PhotonNetwork.IsMasterClient)
        {
            hostIconImage.color = Color.red;
        }
        else
        {
            hostIconImage.color = Color.green;
        }
    }

    public void TransferHost()
    {
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber && players[i].IsInRoom)
            {
                PhotonNetwork.SetMasterClient(players[i]);
                break;
            }
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        isHost = PhotonNetwork.IsMasterClient;
        hostIconImage.color = isHost ? Color.red : Color.green;
        transferHostButton.interactable = isHost;
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("PunBasics-Room for 1", roomOptions, TypedLobby.Default);
    }

    public void JoinRoom()
    {
        PhotonNetwork.LoadLevel("PunBasics-Room for 1");
    }
}
