using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChangePointsCoins : MonoBehaviour
{
    private const string userDataFileName = "userData.json";
    private UserData userData;
    public ShowWeaponInfo showWeaponInfo;

    public GameObject Name;
    public GameObject Condition;
    public GameObject StatTrak;

    private void Awake()
    {
        LoadUserData();
    }

    private void Update()
    {
        try
        {
            showWeaponInfo = GameObject.FindFirstObjectByType<ShowWeaponInfo>();
        }
        catch (System.Exception)
        {
        }
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
    public void ChangeCoins(int addCoins)
    {
        LoadUserData();
        userData.coins += addCoins;
        SaveUserData();
    }

    public void ChangePoints(int addPoints)
    {
        LoadUserData();
        userData.points += addPoints;
        SaveUserData();
    }

    public void ChangeCoins(TMP_Text addCoins)
    {
        LoadUserData();
        userData.coins += int.Parse(addCoins.text);
        showWeaponInfo.SellWeapon();
        SaveUserData();
    }

    public void ChangePoints(TMP_Text addPoints)
    {
        LoadUserData();
        userData.points += int.Parse(addPoints.text);
        showWeaponInfo.SellWeapon();
        SaveUserData();
    }


}
