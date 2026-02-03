using System;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public MessageOwner[] MessageOwnerArray;
    public String[] MessageTextArray;

    public PlayerData (Player player)
    {
        MessageOwnerArray = player.GetAllMessages().Item2;
        MessageTextArray = player.GetAllMessages().Item1;

    }

}
