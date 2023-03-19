using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class HealthAndBumpSystem : MonoBehaviourPun,IPunObservable
{
    [SerializeField] private GameObject HPBar;
    [SerializeField] private Image healthbarColor;
    [SerializeField] private float playerHP = 100f;
    [SerializeField] private float currentHP;

    [SerializeField] private GameObject TpToSpawn;

    void start()
    {
        currentHP = playerHP;
        //Find a specific spawn gameobject and have it attach here
        GameObject startingobj = GameObject.Find("Starting Line");
        if (startingobj != null)
        {
            TpToSpawn.transform.position = startingobj.transform.position;
        }
        else
        {
            Debug.LogError("Could Not Find A Spawn Gameobject!");
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send the current health value to other players
            stream.SendNext(currentHP);
        }
        else if (stream.IsReading)
        {
            // Receive the health value from the network
            currentHP = (int)stream.ReceiveNext();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bump"))
        {
            Debug.Log("bumped");
            var PView = collision.gameObject.GetComponent<PhotonView>();
            if (!PView.IsMine)
            {
                var selfpv = GetComponent<PhotonView>();
                selfpv.RPC(nameof(TakeDamage),PView.Owner);
            }
            
            // Call the TakeDamage() function on the bumped player using a PUN RPC
            //collision.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void TakeDamage(float damage = 25)
    {
        playerHP -= damage;
        healthbarColor.fillAmount = playerHP / 100;



        if (currentHP <= 0)
        {
            //BacktoSpawn();
        }

    }

    private void BacktoSpawn(int viewID)
    {
        currentHP = playerHP;
        GameObject player = PhotonView.Find(viewID).gameObject;
        player.transform.position = TpToSpawn.transform.position;
    }
}
