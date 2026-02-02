using UnityEngine;

public class Player : MonoBehaviour
{
    public int messagesSents;

    public void SavePlayer() 
    { 
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer() 
    { 
        PlayerData data = SaveSystem.LoadPlayer();
        messagesSents = data.messagesSent;
    }
}
