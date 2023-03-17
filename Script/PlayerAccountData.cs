using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class PlayerAccountData
{
    public string Player_Email;

    public string Player_Password;

    public Vector3 _playerPos;

    public float _playerHP;

    public int _playerPoints;

    public PlayerAccountData()
    {
        Player_Email = string.Empty;
        Player_Password = string.Empty;
        _playerPos = Vector3.zero;
        _playerHP = 100f;
        _playerPoints = 0;
    }
}
