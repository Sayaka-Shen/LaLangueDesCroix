using System;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public MessageOwner[] MessageOwnerArray;
    public String[] MessageTextArray;

    public PlayerData (Player player)
    {
        MessageTextArray = player.GetAllMessages().Item1;
        MessageOwnerArray = player.GetAllMessages().Item2;
    }

}
