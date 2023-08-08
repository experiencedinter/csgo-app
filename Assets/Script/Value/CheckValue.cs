using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckValue : MonoBehaviour
{
    private const string userDataFileName = "userData.json";
    private UserData userData;
    private void Awake()
    {
        LoadUserData();
    }

    private void LoadUserData()
    {
        string path = Path.Combine(Application.persistentDataPath, "userData.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            userData = JsonUtility.FromJson<UserData>(jsonData);
        }
        else
        {
            userData = new UserData();
        }
    }
    private void SaveUserData()
    {
        string path = Path.Combine(Application.persistentDataPath, userDataFileName);
        string jsonData = JsonUtility.ToJson(userData);
        File.WriteAllText(path, jsonData);
    }
    public void ChangeCoins(int minusCoins)
    {
        LoadUserData();
        userData.coins -= minusCoins;
        SaveUserData();
    }
    public void ChangePoints(int minusPoints)
    {
        LoadUserData();
        userData.points -= minusPoints;
        SaveUserData();
    }
    public bool EnoughCoins(int Coins)
    {
        LoadUserData();
        return userData.coins >= Coins;
    }
    public bool EnoughPoints(int Points)
    {
        LoadUserData();
        return userData.points >= Points;
    }

}
