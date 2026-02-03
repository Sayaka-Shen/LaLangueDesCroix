using UnityEngine;


[System.Serializable]
public class PlayerData
{
    
    public GameObject[] MessageArray;
    

    public PlayerData (Player player)
    {
        MessageArray = player.GetChildGameObjects();
    }

}
