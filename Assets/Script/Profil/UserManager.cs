using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
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

    public void SaveUserDataAndUI()
    {
        userData.username = usernameInput.text;
        UpdateUI();
        SaveUserData();
    }

    public void SaveUsername()
    {
        string username = usernameInput.text;
        userData.username = username;
        SaveUserDataAndUI();
    }

    private void UpdateUI()
    {
        usernameInput.text = userData.username;
    }

    private void SaveUserData()
    {
        string path = Path.Combine(Application.persistentDataPath, "userData.json");
        string jsonData = JsonUtility.ToJson(userData);
        File.WriteAllText(path, jsonData);
    }
}
