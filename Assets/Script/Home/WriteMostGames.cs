using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Rendering;
using UnityEngine;

public class WriteMostGames : MonoBehaviour
{
    private const string userDataFileName = "MostGames.json";
    List<Infos> infolist = new List<Infos>();
    public Infos info = new Infos();

    bool notin = true;
    public void LoadLastItems(Infos infogame)
    {
        string path = Path.Combine(Application.persistentDataPath, "MostGames.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            infolist = JsonConvert.DeserializeObject<List<Infos>>(jsonData);
        }
        else
        {
            infolist = new List<Infos>();
        }
        if (infolist.Count == 0)
        {

            info.Gamename = infogame.Gamename;
            info.Played = 1;
            info.WinLose += infogame.WinLose;
            infolist.Add(info);

        }
        else
        {
            for (int i = 0; i < infolist.Count; i++)
            {
                if (infolist[i].Gamename == infogame.Gamename)
                {
                    info.Played += infogame.Played;
                    info.WinLose += infogame.WinLose;
                    infolist[i].WinLose += infogame.WinLose;
                    infolist[i].Played += infogame.Played;
                    notin = false;
                    break;
                }

            }
            if (notin)
            {
                info.Gamename = infogame.Gamename;
                info.Played += infogame.Played;
                info.WinLose += infogame.WinLose;
                infolist.Add(info);

            }
        }
        var updatedJson = JsonConvert.SerializeObject(infolist, Formatting.Indented);
        File.WriteAllText(path, updatedJson);
    }
}
