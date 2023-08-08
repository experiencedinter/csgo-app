using System.Collections.Generic;
using UnityEngine;

public class CaseOpening : MonoBehaviour
{
    public GameObject tempName;
    public TextAsset jsonFilePath;
    public TextAsset ProfilPath;
    public string Casename = "";
    public WeaponList Case = new WeaponList();
    public List<Weapon> Soloweapons = new List<Weapon>();
    public List<Weapon> Colorweapon = new List<Weapon>();
    public CaseDatabase db = new CaseDatabase();

    bool notIn;
    int Condition;

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

    public void StartCaseOpen()
    {
        SearchInfos();
        OpenCase();
    }

    void SearchInfos()
    {
        tempName = GameObject.FindGameObjectWithTag("CaseInfo");
        Casename = tempName.name;
        if (tempName.name != "TempName")
        {
            jsonFilePath = Resources.Load<TextAsset>("Database/" + tempName.name);
            Case = JsonUtility.FromJson<WeaponList>(jsonFilePath.text);
        }
    }
    public void OpenCase()
    {
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
    public Inventar inventar;
    public WriteInventar writeInventar;
    public WriteLastItem writeLastItem;
    public WriteCaseStats writeCaseStats;
    void RandomSelectedWeapon()
    {
        int lastRandom = Random.Range(0, Soloweapons.Count);
        try
        {
            Case.CaseWeapon = Soloweapons[lastRandom];
        }
        catch (System.Exception)
        {

            Debug.Log(lastRandom);
        }
        inventar.ID = Case.CaseWeapon.ID;
        inventar.Name = Case.CaseWeapon.Name;
        inventar.Price = Case.CaseWeapon.Price;
        inventar.CaseName = Case.CaseWeapon.CaseName;
        inventar.Color = Case.CaseWeapon.Color;
        inventar.Condition = Case.CaseWeapon.Condition;
        inventar.InspectInGame = Case.CaseWeapon.InspectInGame;
        inventar.StatTrak = Case.CaseWeapon.StatTrak;

        writeInventar.WriteInventarData(inventar);
        
        writeLastItem.WriteLastItemData(inventar);
        writeCaseStats.AddgeoeffneteKisten(1);
        writeCaseStats.AddVerdient(Case.CaseWeapon.Price);
        for (int i = 0; i < db._case.Count; i++)
        {
            if (db.GetStats(i).Name == Case.CaseWeapon.CaseName)
            {
                writeCaseStats.AddAusgaben(db.GetStats(i).Price);
            }
        }
        switch(Case.CaseWeapon.Color) {
            case 3:
                {
                    if (Case.CaseWeapon.StatTrak)
                    {
                        writeCaseStats.AddMilStat(1);
                    }
                    else
                    {
                        writeCaseStats.AddMilItem(1);
                    }
                    break;
                }
            case 4:
                {
                    if (Case.CaseWeapon.StatTrak)
                    {
                        writeCaseStats.AddResStat(1);
                    }
                    else
                    {
                        writeCaseStats.AddResItem(1);
                    }
                    break;
                }
            case 5:
                {
                    if (Case.CaseWeapon.StatTrak)
                    {
                        writeCaseStats.AddClasStat(1);
                    }
                    else
                    {
                        writeCaseStats.AddClasItem(1);
                    }
                    break;
                }
            case 6:
                {
                    if (Case.CaseWeapon.StatTrak)
                    {
                        writeCaseStats.AddCovStat(1);
                    }
                    else
                    {
                        writeCaseStats.AddCovItem(1);
                    }
                    break;
                }



        }
        

    }
}