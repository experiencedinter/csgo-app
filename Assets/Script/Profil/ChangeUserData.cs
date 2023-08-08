using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ChangeUserData : MonoBehaviour
{
    public TMP_Text[] Username;
    public TMP_Text Points;
    public TMP_Text Coins;

    private UserData userData;

    private void Start()
    {
        LoadUserData();
        UpdateUI();
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
    private void Update()
    {
        LoadUserData();
        UpdateUI();
    }
    private void UpdateUI()
    {
        for (int i = 0; i < Username.Length; i++)
        {
            Username[i].text = userData.username;
        }
        Points.text = userData.points.ToString();
        Coins.text = userData.coins.ToString();
    }
}
