using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ReadProfil : MonoBehaviour
{
    public TextMeshProUGUI winlose;
    List<Infos> infolist = new List<Infos>();
    public Infos info = new Infos();
    float temp;
    private void LoadInventarData()
    {
        string path = Path.Combine(Application.persistentDataPath, "MostGames.json");

        if (File.Exists(path))
        {
            // Wenn die Datei existiert, lade vorhandene Daten in die Liste
            string jsonData = File.ReadAllText(path);
            infolist = JsonConvert.DeserializeObject<List<Infos>>(jsonData);
        }
        else
        {
            List<Infos> infolist = new List<Infos>();
            string emptyJsonData = JsonUtility.ToJson(infolist);
            File.WriteAllText(path, emptyJsonData);
        }
    }
    private void Awake()
    {
        LoadInventarData();
        updateText();
    }
    void updateText()
    {
        for (int i = 0; i < infolist.Count; i++)
        {
            temp += infolist[i].WinLose;
        }

        winlose.text = temp.ToString() + "€";

    }
}
