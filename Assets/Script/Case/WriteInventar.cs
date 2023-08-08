using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
public class WriteInventar : MonoBehaviour
{
    public Inventar inventarlist = new Inventar();
    public List<Inventar> inventar = new List<Inventar>();

    public void WriteInventarData(Inventar weaponinfo)
    {
        string path = Path.Combine(Application.persistentDataPath, "Inventar.json");
        
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            inventar = JsonConvert.DeserializeObject<List<Inventar>>(jsonData);
        }
       
        inventarlist.ID = weaponinfo.ID;
        inventarlist.Name = weaponinfo.Name;
        inventarlist.Price = weaponinfo.Price;
        inventarlist.Condition = weaponinfo.Condition;
        inventarlist.StatTrak = weaponinfo.StatTrak;
        inventarlist.Color = weaponinfo.Color;
        inventarlist.CaseName = weaponinfo.CaseName;
        inventarlist.InspectInGame = weaponinfo.InspectInGame;



        inventar.Add(inventarlist);

        var updatedJson = JsonConvert.SerializeObject(inventar, Formatting.Indented);
        File.WriteAllText(path, updatedJson);
    }
    public void WriteInventarData(Weapon weaponinfo)
    {
        string path = Path.Combine(Application.persistentDataPath, "Inventar.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            inventar = JsonConvert.DeserializeObject<List<Inventar>>(jsonData);
        }

        inventarlist.ID = weaponinfo.ID;
        inventarlist.Name = weaponinfo.Name;
        inventarlist.Price = weaponinfo.Price;
        inventarlist.Condition = weaponinfo.Condition;
        inventarlist.StatTrak = weaponinfo.StatTrak;
        inventarlist.Color = weaponinfo.Color;
        inventarlist.CaseName = weaponinfo.CaseName;
        inventarlist.InspectInGame = weaponinfo.InspectInGame;



        inventar.Add(inventarlist);

        var updatedJson = JsonConvert.SerializeObject(inventar, Formatting.Indented);
        File.WriteAllText(path, updatedJson);
    }

    public void RemoveFromData(Inventar weaponinfo)
    {
        string path = Path.Combine(Application.persistentDataPath, "Inventar.json");
        if (File.Exists(path))
        { 
            string jsonData = File.ReadAllText(path);
            inventar = JsonConvert.DeserializeObject<List<Inventar>>(jsonData);
        }

        for (int i = 0; i < inventar.Count; i++)
        {
            if (inventar[i].Name == weaponinfo.Name && inventar[i].Condition == weaponinfo.Condition && inventar[i].StatTrak == weaponinfo.StatTrak)
            {
                inventar.Remove(inventar[i]);
            }
        }

        var updatedJson = JsonConvert.SerializeObject(inventar, Formatting.Indented);
        File.WriteAllText(path, updatedJson);
    }

    
}
