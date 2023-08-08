using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.StickyNote;

public class CaseBattle : MonoBehaviour
{
    GameObject tempName;
    TextAsset jsonFilePath;
    WeaponList Case = new WeaponList();
    List<Weapon> Soloweapons = new List<Weapon>();
    List<Weapon> Colorweapon = new List<Weapon>();

    List<string> Casenames = new List<string>();


    public List<Weapon> PlayerWeapons = new List<Weapon>();
    public List<Weapon> BotWeapons = new List<Weapon>();
    public float playerWeaponPrice = 0, botWeaponPrice = 0;

    bool PlayerCase;
    int count = 0;
    int Condition;
    string namen;

    [System.Serializable]
    public class Weapon
    {
        public int ID;
        public string Name;
        public float Price;
        public int Condition;
        public int Color;
        public string CaseName;
        public string InspectInGame;
        public bool StatTrak;
    }

    [System.Serializable]
    public class WeaponList
    {
        public Weapon[] Case;
        public Weapon CaseWeapon;
        public Weapon weapon;
    }

    private void SetReset()
    {
        PlayerWeapons.Clear();
        BotWeapons.Clear();
        count = 0;
        playerWeaponPrice = 0;
        botWeaponPrice = 0;
    }
    public void StartCaseOpen()
    {
        SetReset();
        SearchInfos();
        for (int i = 0; i < tempName.transform.childCount; i++)
        {
            Casename();
            PlayerOpenCase();
            BotOpenCase();
            WeaponObjektCreate();
        }
        WinnerSelected();
    }

    void SearchInfos()
    {
        Casenames.Clear();
        tempName = GameObject.Find("Case_Group");
        for (int i = 0; i < tempName.transform.childCount; i++)
        {
            Transform childTransform = tempName.transform.GetChild(i);
            Casenames.Add(childTransform.name);
        }
    }
    void Casename()
    {
        jsonFilePath = Resources.Load<TextAsset>("Database/" + Casenames[count]);
        Case = JsonUtility.FromJson<WeaponList>(jsonFilePath.text);
        count++;
    }
    public void PlayerOpenCase()
    {
        PlayerCase = true;
        int number = RandomValueColor();
        if (number < 7992)
        {
            SelectedWeapons(3);
            RandomConditionValue(StatTrakprof(3));
            RandomSelectedWeapon();
        }
        else if (number < 9590 && number > 7992)
        {
            SelectedWeapons(4);
            RandomConditionValue(StatTrakprof(3));
            RandomSelectedWeapon();
        }
        else if (number < 9910 && number > 9590)
        {
            SelectedWeapons(5);
            RandomConditionValue(StatTrakprof(3));
            RandomSelectedWeapon();
        }
        else if (number < 9974 && number > 9910)
        {
            SelectedWeapons(6);
            RandomConditionValue(StatTrakprof(3));
            RandomSelectedWeapon();
        }
        else
        {
            SelectedWeapons(7);
            RandomConditionValue(StatTrakprof(3));
            RandomSelectedWeapon();
        }
    }
    public void BotOpenCase()
    {
        PlayerCase = false;
        int number = RandomValueColor();
        if (number < 7992)
        {
            SelectedWeapons(3);
            RandomConditionValue(StatTrakprof(3));
            RandomSelectedWeapon();
        }
        else if (number < 9590 && number > 7992)
        {
            SelectedWeapons(4);
            RandomConditionValue(StatTrakprof(3));
            RandomSelectedWeapon();
        }
        else if (number < 9910 && number > 9590)
        {
            SelectedWeapons(5);
            RandomConditionValue(StatTrakprof(3));
            RandomSelectedWeapon();
        }
        else if (number < 9974 && number > 9910)
        {
            SelectedWeapons(6);
            RandomConditionValue(StatTrakprof(3));
            RandomSelectedWeapon();
        }
        else
        {
            SelectedWeapons(7);
            RandomConditionValue(StatTrakprof(3));
            RandomSelectedWeapon();
        }
    }
    int RandomValueColor()
    {
        int randomValue;
        return randomValue = Random.Range(0, 10000); ;
    }

    void SelectedWeapons(int Color)
    {
        Colorweapon.Clear();

        for (int i = 0; i < Case.Case.Length; i++)
        {
            if (Case.Case[i].Color == Color)
            {
                Colorweapon.Add(Case.Case[i]);
            }
        }
    }

    void RandomConditionValue(bool StatTrak)
    {
        Soloweapons.Clear();
        int randomValue = Random.Range(0, 100);

        if (randomValue >= 0 && randomValue < 8)
        {
            Condition = 1;
            for (int i = 0; i < Colorweapon.Count; i++)
            {
                if (Colorweapon[i].Condition == Condition && Colorweapon[i].StatTrak == StatTrak)
                    Soloweapons.Add(Colorweapon[i]);
            }
        }
        else if (randomValue > 7 && randomValue < 15)
        {
            Condition = 2;
            for (int i = 0; i < Colorweapon.Count; i++)
            {
                if (Colorweapon[i].Condition == Condition && Colorweapon[i].StatTrak == StatTrak)
                    Soloweapons.Add(Colorweapon[i]);
            }
        }
        else if (randomValue > 15 && randomValue < 38)
        {
            Condition = 3;
            for (int i = 0; i < Colorweapon.Count; i++)
            {
                if (Colorweapon[i].Condition == Condition && Colorweapon[i].StatTrak == StatTrak)
                    Soloweapons.Add(Colorweapon[i]);
            }
        }
        else if (randomValue > 38 && randomValue < 45)
        {
            Condition = 4;
            for (int i = 0; i < Colorweapon.Count; i++)
            {
                if (Colorweapon[i].Condition == Condition && Colorweapon[i].StatTrak == StatTrak)
                    Soloweapons.Add(Colorweapon[i]);
            }
        }
        else
        {
            Condition = 5;
            for (int i = 0; i < Colorweapon.Count; i++)
            {
                if (Colorweapon[i].Condition == Condition && Colorweapon[i].StatTrak == StatTrak)
                    Soloweapons.Add(Colorweapon[i]);
            }
        }
    }

    bool StatTrakprof(int Color)
    {
        int statTrakchance = 0, randomStatTrakchance;
        switch (Color)
        {
            case 3:
                statTrakchance = 799;
                break;
            case 4:
                statTrakchance = 159;
                break;
            case 5:
                statTrakchance = 31;
                break;
            case 6:
                statTrakchance = 6;
                break;
            case 7:
                statTrakchance = 2;
                break;
        }
        randomStatTrakchance = Random.Range(0, 10000);
        if (randomStatTrakchance <= statTrakchance)
            return true;
        else
            return false;
    }

    void RandomSelectedWeapon()
    {
        int lastRandom = Random.Range(0, Soloweapons.Count-1);
        Case.CaseWeapon = Soloweapons[lastRandom];

        if (PlayerCase)
            PlayerWeapons.Add(Soloweapons[lastRandom]);
        else
            BotWeapons.Add(Soloweapons[lastRandom]);

        string path = GetPath("Profil/" + Case.CaseWeapon.CaseName + "_LastDrops.json");

        jsonFilePath = Resources.Load<TextAsset>("Profil/" + Case.CaseWeapon.CaseName + "_LastDrops.json");
        List<Weapon> weaponlist = new List<Weapon>();
        string updatedJsonData = JsonUtility.ToJson(weaponlist);

    }
    
    string GetPath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }
    public WriteInventar writeInventar;
    Inventar weapon = new Inventar();
    void WinnerSelected()
    {
        
        for (int i = 0; i < PlayerWeapons.Count; i++)
        {
            playerWeaponPrice += PlayerWeapons[i].Price;
            botWeaponPrice += BotWeapons[i].Price;
        }
        if(playerWeaponPrice > botWeaponPrice)
        {
            for (int i = 0; i < PlayerWeapons.Count; i++)
            {
                weapon.Name = PlayerWeapons[i].Name;
                weapon.Condition = PlayerWeapons[i].Condition;
                weapon.Price = PlayerWeapons[i].Price;
                weapon.Color = PlayerWeapons[i].Color;
                weapon.StatTrak = PlayerWeapons[i].StatTrak;
                weapon.CaseName = PlayerWeapons[i].CaseName;
                weapon.InspectInGame = PlayerWeapons[i].InspectInGame;
                writeInventar.WriteInventarData(weapon);
            }
            for (int i = 0; i < BotWeapons.Count; i++)
            {
                weapon.Name = BotWeapons[i].Name;
                weapon.Condition = BotWeapons[i].Condition;
                weapon.Price = BotWeapons[i].Price;
                weapon.Color = BotWeapons[i].Color;
                weapon.StatTrak = BotWeapons[i].StatTrak;
                weapon.CaseName = BotWeapons[i].CaseName;
                weapon.InspectInGame = BotWeapons[i].InspectInGame;
                writeInventar.WriteInventarData(weapon);
            }

        }
     
    }

    void WeaponObjektCreate()
    {
        DeleteAllChildren();
        for (int i = 0; i < PlayerWeapons.Count; i++)
        {
            // Player
            namen = PlayerWeapons[i].Name;
            GameObject weaponplayer = new GameObject(namen);
            weaponplayer.transform.parent = GameObject.Find("Player_Weapon_Zone").transform;

            string name = namen;
            string formatiertername = name.Replace(" | ", "_");
            weaponplayer.AddComponent<Image>().sprite = Resources.Load<Sprite>("Weapon/" + PlayerWeapons[i].CaseName + "/" + formatiertername);

            weaponplayer.transform.localScale = new Vector3(1, 1, 1);
            weaponplayer.AddComponent<BoxCollider2D>().size = new Vector2(137, 114);

            // Bot

            namen = BotWeapons[i].Name;
            GameObject weaponbot = new GameObject(namen);
            weaponbot.transform.parent = GameObject.Find("Bot_Weapon_Zone").transform;

            name = namen;
            formatiertername = name.Replace(" | ", "_");
            weaponbot.AddComponent<Image>().sprite = Resources.Load<Sprite>("Weapon/" + BotWeapons[i].CaseName + "/" + formatiertername);

            weaponbot.transform.localScale = new Vector3(1, 1, 1);
            weaponbot.AddComponent<BoxCollider2D>().size = new Vector2(137, 114);

        }
    }

    void DeleteAllChildren()
    {
        int childCount = GameObject.Find("Player_Weapon_Zone").transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = GameObject.Find("Player_Weapon_Zone").transform.GetChild(i);
            Destroy(child.gameObject);
            child = GameObject.Find("Bot_Weapon_Zone").transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }
}
