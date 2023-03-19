using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Name Input")]
    public string playerName;
    [SerializeField] private TMP_Text playerNameText;


    [Header("For Trap TP")]
    [SerializeField] private Transform _spawnLocation;

    [SerializeField] private static GameObject localPlayer;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("Characters/Player", _spawnLocation.position, Quaternion.identity);
        playerNameText = Instantiate(Resources.Load<TextMeshPro>("PlayerName"));

        playerName = PhotonNetwork.NickName;
        playerNameText.text = playerName;
        playerNameText.transform.SetParent(this.transform);
        playerNameText.transform.position = localPlayer.transform.position + Vector3.up * 1.5f;
        //playerNameText.text = playerName;
    }

    private void Update()
    {
        playerNameText.transform.position = localPlayer.transform.position + Vector3.up * 1.5f;
    }
}
