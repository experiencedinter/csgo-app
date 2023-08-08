using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadInventarforHome : MonoBehaviour
{
    public TextMeshProUGUI BestWeapon;
    public TextMeshProUGUI BestPrice;
    public TextMeshProUGUI BestSkinname;
    public Image bestSkin;


    public TextMeshProUGUI[] Weaponname;
    public TextMeshProUGUI[] Weaponprice;
    public TextMeshProUGUI GesamtPrice;

    public List<Inventar> inventar = new List<Inventar>();
    private void LoadInventarData()
    {
        string path = Path.Combine(Application.persistentDataPath, "Inventar.json");

        if (File.Exists(path))
        {
            // Wenn die Datei existiert, lade vorhandene Daten in die Liste
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
    private void Awake()
    {
        LoadInventarData();
        sortweapon();
    }

    void sortweapon()
    {
        if (inventar.Count != 0)
        {
            inventar.Sort((g1, g2) => g2.Price.CompareTo(g1.Price));
            string weaponName = inventar[0].Name;
            string[] parts = weaponName.Split('|');
            BestWeapon.text = parts[0];
            BestSkinname.text = parts[1];
            BestPrice.text = inventar[0].Price.ToString() + "€";
            string name = inventar[0].Name;
            string formatiertername = name.Replace(" | ", "_");
            bestSkin.sprite = Resources.Load<Sprite>("Weapon/" + inventar[0].CaseName + "/" + formatiertername);

        }

        if (inventar.Count > 4)
        {
            for (int i = 0; i < 5; i++)
            {
                Weaponname[i].text = inventar[i].Name;
                Weaponprice[i].text = inventar[i].Price.ToString() + "€";
            }
        }
        else
        {
            for (int i = 0; i < inventar.Count; i++)
            {
                Weaponname[i].text = inventar[i].Name;
                Weaponprice[i].text = inventar[i].Price.ToString() + "€";
            }
        }
        
    }
}
