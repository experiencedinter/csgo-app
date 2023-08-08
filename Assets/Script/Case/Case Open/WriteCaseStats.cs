using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class WriteCaseStats : MonoBehaviour
{
    public ReadCaseStats readCaseStats;
    private const string userDataFileName = "LastCaseItems.json";

    public LastCase lastCase;
    private void Awake()
    {
        LoadLastItems();
    }

    private void LoadLastItems()
    {
        string path = Path.Combine(Application.persistentDataPath, "LastCaseItems.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            lastCase = JsonUtility.FromJson<LastCase>(jsonData);
        }
        else
        {
            lastCase = new LastCase();
        }
    }
    private void SaveUserData()
    {
        string path = Path.Combine(Application.persistentDataPath, userDataFileName);
        string jsonData = JsonUtility.ToJson(lastCase);
        File.WriteAllText(path, jsonData);
    }

    public void AddgeoeffneteKisten(int number)
    {
        LoadLastItems();
        lastCase.geoeffneteKisten += number;
        SaveUserData();
    }

    public void AddVerdient(float Verdient)
    {
        LoadLastItems();
        lastCase.Verdient += Verdient;
        SaveUserData();
    }
    public void AddAusgaben(float Ausgaben)
    {
        LoadLastItems();
        lastCase.Ausgaben += Ausgaben;
        SaveUserData();
    }
    public void AddMilItem(int item)
    {
        LoadLastItems();
        lastCase.MilItem += item;
        SaveUserData();
    }
    public void AddMilStat(int item)
    {
        LoadLastItems();
        lastCase.MilStat += item;
        SaveUserData();
    }
    public void AddResItem(int item)
    {
        LoadLastItems();
        lastCase.ResItem += item;
        SaveUserData();
    }
    public void AddResStat(int item)
    {
        LoadLastItems();
        lastCase.ResStat += item;
        SaveUserData();
    }
    public void AddClasItem(int item)
    {
        LoadLastItems();
        lastCase.ClasItem += item;
        SaveUserData();
    }
    public void AddClasStat(int item)
    {
        LoadLastItems();
        lastCase.ClasStat += item;
        SaveUserData();
    }

    public void AddCovItem(int item)
    {
        LoadLastItems();
        lastCase.CovItem += item;
        SaveUserData();
    }
    public void AddCovStat(int item)
    {
        LoadLastItems();
        lastCase.CovStat += item;
        SaveUserData();
    }



}
