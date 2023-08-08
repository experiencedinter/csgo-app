using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadMostGames : MonoBehaviour
{
    public TextMeshProUGUI winlose1;
    public TextMeshProUGUI Gamename1;
    public Image Gameicon1;

    public TextMeshProUGUI winlose2;
    public TextMeshProUGUI Gamename2;
    public Image Gameicon2;

    public TextMeshProUGUI winlose3;
    public TextMeshProUGUI Gamename3;
    public Image Gameicon3;

    List<Infos> infolist = new List<Infos>();
    public Infos info = new Infos();
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
        infolist.Sort((g1, g2) => g2.Played.CompareTo(g1.Played));
        try
        {
            Gameicon1.sprite = Resources.Load<Sprite>("Images/" + infolist[0].Gamename);
            Gamename1.text = infolist[0].Gamename;
            winlose1.text = infolist[0].WinLose.ToString() + "�";
        }
        catch (System.Exception)
        {
        }

        try
        {
            Gameicon2.sprite = Resources.Load<Sprite>("Images/" + infolist[1].Gamename);
            Gamename2.text = infolist[1].Gamename;
            winlose2.text = infolist[1].WinLose.ToString() + "�";
        }
        catch (System.Exception)
        {
       }

        try
        {
            Gameicon3.sprite = Resources.Load<Sprite>("Images/" + infolist[2].Gamename);
            Gamename3.text = infolist[2].Gamename;
            winlose3.text = infolist[2].WinLose.ToString() + "�";
        }
        catch (System.Exception)
        {

        }

        
    }

}
