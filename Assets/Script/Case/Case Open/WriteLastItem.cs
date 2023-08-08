using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WriteLastItem : MonoBehaviour
{
    private void Awake()
    {
        inventar.Clear();
    }

    public Inventar inventarlist = new Inventar();
    public List<Inventar> inventar = new List<Inventar>();
    public void WriteLastItemData(Inventar weaponinfo)
    {
        string path = Path.Combine(Application.persistentDataPath, "LastItems.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            inventar = JsonConvert.DeserializeObject<List<Inventar>>(jsonData);
        }

        inventarlist.ID = weaponinfo.ID;
        inventarlist.Name = weaponinfo.Name;
        inventarlist.Condition = weaponinfo.Condition;

        inventar.Add(inventarlist);

        var updatedJson = JsonConvert.SerializeObject(inventar, Formatting.Indented);
        File.WriteAllText(path, updatedJson);
    }
}
