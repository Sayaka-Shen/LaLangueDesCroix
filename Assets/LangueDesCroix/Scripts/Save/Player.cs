using NUnit.Framework;
using System;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject MessageContain;
    public GameObject MessageSender,MessageReceiver;
    //private void Awake()
    //{
    //    LoadPlayer();
    //}

    private void Start()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            LoadPlayer();
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {

        PlayerData data = SaveSystem.LoadPlayer();
        CreateMessages(data);
    }

    public (String[], MessageOwner[]) GetAllMessages()
    {
        GameObject[] childObjects = new GameObject[MessageContain.transform.childCount];

        for (int i = 0; i < MessageContain.transform.childCount; i++)
        {
            childObjects[i] = MessageContain.transform.GetChild(i).gameObject;
            Debug.Log("Child GameObject: " + childObjects[i].name);
        }
        String[] messageTexts = new String[childObjects.Length];

        MessageOwner[] messageOwners = new MessageOwner[childObjects.Length];

        for(int i = 0; i < childObjects.Length; i++)
        {
            Message messageComponent = childObjects[i].GetComponentInChildren<Message>();
            if (messageComponent != null)
            {
                messageTexts[i] = messageComponent.GetMessageText();
                messageOwners[i] = messageComponent.GetMessageOwner();
            }
        }

        return (messageTexts, messageOwners);

    }
    
    public void CreateMessages(PlayerData data)
    {

        for (int i = 0; i < data.MessageTextArray.Length; i++)
        {
            GameObject messageObject;
            switch
                (data.MessageOwnerArray[i])
            {
                case MessageOwner.Sender:
                    messageObject = Instantiate(MessageSender, MessageContain.transform);
                    break;
                case MessageOwner.Receiver:
                    messageObject = Instantiate(MessageReceiver, MessageContain.transform);
                    break;
                default:
                    messageObject = null;
                    break;
            }

            Message msg = messageObject.GetComponentInChildren<Message>();
            if (msg != null)
            {
                msg.SetMessageText(data.MessageTextArray[i]);
            }
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application quitting, saving player data.");
        SavePlayer();
    }
}
