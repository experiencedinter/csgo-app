using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ReadCaseStats : MonoBehaviour
{
    public TextMeshProUGUI Kistengeöffnet;
    public TextMeshProUGUI Ausgegeben;
    public TextMeshProUGUI Verdient;
    public TextMeshProUGUI Profit;
    public TextMeshProUGUI MiliItem;
    public TextMeshProUGUI MiliStat;
    public TextMeshProUGUI RestItem;
    public TextMeshProUGUI RestStat;
    public TextMeshProUGUI ClassItem;
    public TextMeshProUGUI ClassStat;
    public TextMeshProUGUI CoveItem;
    public TextMeshProUGUI CoveStat;

    public LastCase lastcase = new LastCase();
   

    private void LoadInventarData()
    {
        string path = Path.Combine(Application.persistentDataPath, "LastCaseItems.json");

        if (File.Exists(path))
        {
            // Wenn die Datei existiert, lade vorhandene Daten in die Liste
            string jsonData = File.ReadAllText(path);
            lastcase = JsonConvert.DeserializeObject<LastCase>(jsonData);
        }
        else
        {
            LastCase lastcase = new LastCase();
            string emptyJsonData = JsonUtility.ToJson(lastcase);
            File.WriteAllText(path, emptyJsonData);
        }

    }
    public void Update()
    {
        LoadInventarData();
        changetext();
    }

    void changetext()
    {        
        Kistengeöffnet.text = lastcase.geoeffneteKisten.ToString();
        Verdient.text = lastcase.Verdient.ToString("F2") + "€";
        Ausgegeben.text = lastcase.Ausgaben.ToString("F2") + "€";
        MiliItem.text = lastcase.MilItem.ToString();
        MiliStat.text = lastcase.MilStat.ToString();
        RestItem.text = lastcase.ResItem.ToString();
        RestStat.text = lastcase.ResStat.ToString();
        ClassItem.text = lastcase.ClasItem.ToString();
        ClassStat.text = lastcase.ClasStat.ToString();
        CoveItem.text = lastcase.CovItem.ToString();
        CoveStat.text = lastcase.CovStat.ToString();
        float profitInProzent = 0f;

        if (lastcase.Ausgaben > 0f)
        {
            float differenz = lastcase.Ausgaben - lastcase.Verdient;
            profitInProzent = (differenz / lastcase.Ausgaben) * 100f;
        }
        Profit.text = profitInProzent.ToString("F2") + "%";
    }
    public ReadLastItem readLastItem;
    public void ResetStats()
    {
        string path = Path.Combine(Application.persistentDataPath, "LastCaseItems.json");
        LastCase lastcase = new LastCase();
        string emptyJsonData = JsonUtility.ToJson(lastcase);
        File.WriteAllText(path, emptyJsonData); 
        readLastItem.ClearList();
        path = Path.Combine(Application.persistentDataPath, "LastItems.json");
        List<Inventar> inventar = new List<Inventar>();
        emptyJsonData = "[]";
        File.WriteAllText(path, emptyJsonData);

    }
}
