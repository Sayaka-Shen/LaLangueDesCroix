using UnityEngine;


[System.Serializable]
public class PlayerData
{
    
    public int messagesSent;

    public PlayerData (Player player)
    {
        messagesSent = player.messagesSents;
    }

}
