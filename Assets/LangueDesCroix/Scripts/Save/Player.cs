using NUnit.Framework;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject MessageContain;

    //private void Awake()
    //{
    //    LoadPlayer();
    //}

    private void Start()
    {
        LoadPlayer();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Debug.Log(data.MessageArray.Length);
    }

    public GameObject[] GetChildGameObjects()
    {
        GameObject[] childObjects = new GameObject[MessageContain.transform.childCount];

        for (int i = 0; i < MessageContain.transform.childCount; i++)
        {
            childObjects[i] = MessageContain.transform.GetChild(i).gameObject;
            Debug.Log("Child GameObject: " + childObjects[i].name);
        }
        return childObjects;

    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application quitting, saving player data.");
        SavePlayer();
    }
}
