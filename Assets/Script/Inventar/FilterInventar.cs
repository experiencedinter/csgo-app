using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FilterInventar : MonoBehaviour
{
    public TMP_Text filter;

    public List<Inventar> Filterlist = new List<Inventar>();
    public List<Inventar> inventar = new List<Inventar>();
    public List<Case> Caselist = new List<Case>();

    public ReadInventar readInventar;

    public CaseDatabase caseDatabase = new CaseDatabase();
    private void Start()
    {
        LoadInventarData();
    }
    
    private void LoadInventarData()
    {
        string path = Path.Combine(Application.persistentDataPath, "Inventar.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            inventar = JsonConvert.DeserializeObject<List<Inventar>>(jsonData);
        }
        else
        {
            List<Inventar> inventar = new List<Inventar>();
            string emptyJsonData = JsonUtility.ToJson(inventar);
            File.WriteAllText(path, emptyJsonData);
        }
    }

    public void Filter()
    {
        Filterlist.Clear();
        caseDatabase.Awake();
        switch (filter.text)
        {
            case "Kiste":
                {
                    Caselist.Clear();
                    for (int i = 0; i < caseDatabase._case.Count; i++)
                    {
                        Caselist.Add(caseDatabase.GetStats(i));
                    }

                    for (int i = 0; i < Caselist.Count;)
                    {
                        for (int j = 0; j < inventar.Count; j++)
                        {
                            if (inventar[j].CaseName == Caselist[i].Name)
                            {
                                Filterlist.Add(inventar[j]);
                            }

                        }
                        i++;
                    }
                    readInventar.CreateFilterWeapon(Filterlist);
                    break;
                }

            case "Zustand":
                {
                    int condition = 5;
                    Caselist.Clear();
                    for (int i = 0; i < caseDatabase._case.Count; i++)
                    {
                        Caselist.Add(caseDatabase.GetStats(i));
                    }


                    for (int i = 0; i < Caselist.Count;)
                    {
                        for (int j = 0; j < inventar.Count; j++)
                        {
                            if (inventar[j].Condition == condition)
                            {
                                Filterlist.Add(inventar[j]);
                            }

                        }
                        if (condition != 0)
                        {
                            condition--;
                        }
                        i++;
                    }
                    readInventar.CreateFilterWeapon(Filterlist);
                    break;
                }
            case "Preis Ab":
                {
                    Caselist.Clear();

                    inventar.Sort((g1, g2) => g1.Price.CompareTo(g2.Price));
                    for (int i = 0; i < inventar.Count; i++)
                    {
                        Filterlist.Add(inventar[i]);
                    }
                    readInventar.CreateFilterWeapon(Filterlist);
                    break;
                }
            case "Preis Auf":
                {
                    Caselist.Clear();

                    inventar.Sort((g1, g2) => g2.Price.CompareTo(g1.Price));
                    for (int i = 0; i < inventar.Count; i++)
                    {
                        Filterlist.Add(inventar[i]);
                    }
                    readInventar.CreateFilterWeapon(Filterlist);
                    break;
                }
        }

    }

    public void Filterreset()
    {
        readInventar.Newload();
    }
}
