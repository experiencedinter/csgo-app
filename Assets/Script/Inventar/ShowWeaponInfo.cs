using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShowWeaponInfo : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text Price;
    public TMP_Text Condition;
    public TMP_Text Case;
    public TMP_Text Points;
    public TMP_Text Coins;

    public Image WeaponIMG;
    public Image WeaponBackground;
    

    public WriteInventar writeInventar;
    public ReadInventar readInventar;
    public ChangePointsCoins changePointsCoins;

    private void Start()
    {
        writeInventar = GameObject.FindAnyObjectByType<WriteInventar>();
        readInventar = GameObject.FindAnyObjectByType<ReadInventar>();
        changePointsCoins = GameObject.FindAnyObjectByType<ChangePointsCoins>();
        Name = GameObject.Find("Name_Txt").GetComponent<TMP_Text>();
        Price = GameObject.Find("Price_Txt").GetComponent<TMP_Text>();
        Condition = GameObject.Find("Condition_Txt").GetComponent<TMP_Text>();
        Case = GameObject.Find("Case_Txt").GetComponent<TMP_Text>();
        Points = GameObject.Find("Points Text").GetComponent<TMP_Text>();
        Coins = GameObject.Find("Coins Text").GetComponent<TMP_Text>();
        WeaponIMG = GameObject.Find("WeaponIMG").GetComponent<Image>();
        WeaponBackground = GameObject.Find("Background").GetComponent<Image>();
        LoadInventarData();
    }
    public List<Inventar> inventar = new List<Inventar>();
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
    int indexnumber;

    public GameObject weapon;
    private void OnMouseDown()
    {
        
        WeaponIMG.enabled = true;
        WeaponBackground.enabled = true;
        for (int i = 0; i < inventar.Count; i++)
        {
            if (inventar[i].Name == gameObject.name && inventar[i].StatTrak.ToString() == gameObject.transform.GetChild(2).name && inventar[i].Condition.ToString() == gameObject.transform.GetChild(3).name)
            {
                indexnumber = i;
                break;
            }
        }
        Name.text = inventar[indexnumber].Name;
        Case.text = inventar[indexnumber].CaseName;
        switch (inventar[indexnumber].Condition)
        {
            case 1:
                {
                    Condition.text = "Battle-Scarred";
                    break;
                }
            case 2:
                {
                    Condition.text = "Well-Worn";
                    break;
                }
            case 3:
                {
                    Condition.text = "Field-Tested";
                    break;
                }
            case 4:
                {
                    Condition.text = "Minimal Wear";
                    break;
                }
            case 5:
                {
                    Condition.text = "Factory New";
                    break;
                }

        }
        Price.text = inventar[indexnumber].Price.ToString();
        Coins.text = ((int)(inventar[indexnumber].Price * 10f)).ToString();
        Points.text = ((int)inventar[indexnumber].Price).ToString();

        string name = inventar[indexnumber].Name;
        string formatiertername = name.Replace(" | ", "_");
        WeaponIMG.sprite = Resources.Load<Sprite>("Weapon/" + inventar[indexnumber].CaseName + "/" + formatiertername);
    }
    public Inventar inventarlist = new Inventar();
    public void SellWeapon()
    {
        for (int i = 0; i < inventar.Count; i++)
        {
            weapon = GameObject.Find(inventar[i].Name);
        }
        LoadInventarData();
        for (int i = 0; i < inventar.Count; i++)
        {
            if (inventar[i].Name == weapon.name && inventar[i].StatTrak.ToString() == weapon.transform.GetChild(2).name && inventar[i].Condition.ToString() == weapon.transform.GetChild(3).name)
            {
                indexnumber = i;
                break;
            }
        }
        inventarlist.ID = inventar[indexnumber].ID;
        inventarlist.Name = inventar[indexnumber].Name;
        inventarlist.Price = inventar[indexnumber].Price;
        inventarlist.Condition = inventar[indexnumber].Condition;
        inventarlist.StatTrak = inventar[indexnumber].StatTrak;
        inventarlist.Color = inventar[indexnumber].Color;
        inventarlist.CaseName = inventar[indexnumber].CaseName;
        inventarlist.InspectInGame = inventar[indexnumber].InspectInGame;
        writeInventar.RemoveFromData(inventarlist);

        readInventar.Newload();
    }
}
