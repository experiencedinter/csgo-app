using Nobi.UiRoundedCorners;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateShopGrid : MonoBehaviour
{
    public CaseDatabase caseDatabase = new CaseDatabase();

    public List<Case> Caselist = new List<Case>();

    public Transform Parent;
    Color color;
    TextMeshProUGUI tmp;
    string wpname;
    string wpskin;

    public TextAsset jsonFilePath;
    public WeaponList Case = new WeaponList();

    public Weapon gameObject = new Weapon();

    public List<Weapon> AllWeapons = new List<Weapon>();
    public List<Weapon> Soloweapons = new List<Weapon>();
    public List<Weapon> filterlist = new List<Weapon>();

    public TMP_InputField abCoins;
    public TMP_InputField bisCoins;
    public TMP_InputField abPoints;
    public TMP_InputField bisPoints;

    public TMP_Text WeaponName;
    public TMP_Text WeaponSkin;
    public TMP_Text WeaponCondition;
    public TMP_Text WeaponPrice;
    public TMP_Text WeaponCoins;
    public TMP_Text WeaponPoints;

    public GameObject StatTrak;
    public GameObject WeaponIMG;
    public GameObject BuyItem;

    bool notIn;
    bool filter;


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
    }

    private void Start()
    {
        AllCasesinList();
        AllWeaponsinList();
        CreateItem();
    }

    void AllCasesinList()
    {
        Caselist.Clear();
        for (int i = 0; i < caseDatabase._case.Count; i++)
        {
            Caselist.Add(caseDatabase.GetStats(i));
        }
    }
    void AllWeaponsinList()
    {
        AllWeapons.Clear();
        try
        {
            for (int i = 0; i < caseDatabase._case.Count; i++)
            {
                jsonFilePath = Resources.Load<TextAsset>("Database/" + caseDatabase._case[i].Name);
                Case = JsonUtility.FromJson<WeaponList>(jsonFilePath.text);
                for (int j = 0; j < Case.Case.Length; j++)
                {
                    AllWeapons.Add(Case.Case[j]);
                }
                DeleteCloneinList();
            }
        }
        catch (Exception e)
        {

            Debug.LogError(e);
        }

    }

    void DeleteCloneinList()
    {
        Soloweapons.Clear();
        if (Soloweapons.Count == 0)
        {
            Soloweapons.Add(AllWeapons[0]);
        }
        for (int i = 0; i < AllWeapons.Count; i++)
        {
            for (int j = 0; j < Soloweapons.Count; j++)
            {
                if (Soloweapons[j].Name != AllWeapons[i].Name)
                    notIn = true;
                else
                {
                    notIn = false;
                    break;
                }
            }
            if (notIn)
            {
                Soloweapons.Add(AllWeapons[i]);
            }
        }
    }
    void CreateItem()
    {
        for (int i = 0; i < Soloweapons.Count; i++)
        {
            GameObject Item = new GameObject(Soloweapons[i].Name);
            Item.transform.parent = Parent;
            Item.transform.localScale = new Vector3(1, 1, 1);
            Item.AddComponent<Image>();
            color = new Color(64f / 255f, 64f / 255f, 64f / 255f);
            Item.GetComponent<Image>().color = color;

            GameObject WeaponIMG = new GameObject("WeaponIMG");
            WeaponIMG.transform.parent = Item.transform;
            WeaponIMG.AddComponent<Image>();
            string name = Soloweapons[i].Name;
            string formatiertername = name.Replace(" | ", "_");
            WeaponIMG.GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapon/" + Soloweapons[i].CaseName + "/" + formatiertername);
            WeaponIMG.transform.localPosition = new Vector3(0, 80, 0);
            WeaponIMG.transform.localScale = Vector3.one;
            WeaponIMG.GetComponent<RectTransform>().sizeDelta = new Vector2(179.53f, 123);

            GameObject WeaponPrice = new GameObject("WeaponPrice");
            WeaponPrice.transform.parent = Item.transform;
            tmp = WeaponPrice.AddComponent<TextMeshProUGUI>();
            tmp.text = Soloweapons[i].Price.ToString() + " $";
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontSize = 24;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = Color.white;

            WeaponPrice.transform.localPosition = new Vector3(0, 12.5f, 0);
            WeaponPrice.transform.localScale = Vector3.one;
            WeaponPrice.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 25);

            GameObject CoinsPrice = new GameObject("CoinsPrice");
            CoinsPrice.transform.parent = Item.transform;
            tmp = CoinsPrice.AddComponent<TextMeshProUGUI>();
            if (Soloweapons[i].Price < 0.10f)
                tmp.text = (Mathf.CeilToInt((Soloweapons[i].Price * 10f))).ToString();
            else if (Soloweapons[i].Price > 0.09f && Soloweapons[i].Price < 1.00f)
                tmp.text = (Mathf.CeilToInt((Soloweapons[i].Price * 100f))).ToString();
            else
                tmp.text = (Mathf.CeilToInt(Soloweapons[i].Price)).ToString();
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontSize = 24;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = Color.white;

            CoinsPrice.transform.localPosition = new Vector3(70, -16.5f, 0);
            CoinsPrice.transform.localScale = Vector3.one;
            CoinsPrice.GetComponent<RectTransform>().sizeDelta = new Vector2(99.85f, 25);

            GameObject CoinsIcon = new GameObject("CoinsIcon");
            CoinsIcon.transform.parent = Item.transform;
            CoinsIcon.transform.localPosition = new Vector3(-106, -16.5f, 0);
            CoinsIcon.transform.localScale = Vector3.one;
            CoinsIcon.AddComponent<RectTransform>();
            CoinsIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(17.87f, 17.87f);
            CoinsIcon.AddComponent<Image>();
            CoinsIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/CoinsIcon");


            GameObject PointsPrice = new GameObject("PointsPrice");
            PointsPrice.transform.parent = Item.transform;
            tmp = PointsPrice.AddComponent<TextMeshProUGUI>();
            tmp.text = (Mathf.CeilToInt((Soloweapons[i].Price * 10f))).ToString();
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontSize = 24;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = Color.white;

            PointsPrice.transform.localPosition = new Vector3(-47.2f, -16.5f, 0);
            PointsPrice.transform.localScale = Vector3.one;
            PointsPrice.GetComponent<RectTransform>().sizeDelta = new Vector2(99.85f, 25);

            GameObject PointsIcon = new GameObject("PointsIcon");
            PointsIcon.transform.parent = Item.transform;
            PointsIcon.transform.localPosition = new Vector3(16, -16.5f, 0);
            PointsIcon.transform.localScale = Vector3.one;
            PointsIcon.AddComponent<RectTransform>();
            PointsIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(17.87f, 17.87f);
            PointsIcon.AddComponent<Image>();
            PointsIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/PointsIcon");

            GameObject WeaponName = new GameObject("WeaponName");
            WeaponName.transform.parent = Item.transform;
            tmp = WeaponName.AddComponent<TextMeshProUGUI>();
            string weaponName = Soloweapons[i].Name;
            string[] parts = weaponName.Split('|');
            wpname = parts[0];
            wpskin = parts[1];
            tmp.text = wpname;
            tmp.fontSize = 13;
            tmp.fontStyle = FontStyles.Bold;
            color = Color.white;
            color.a = 0.7f;
            tmp.color = color;

            WeaponName.transform.localPosition = new Vector3(-40.684f, -35.87f, 0);
            WeaponName.transform.localScale = Vector3.one;
            WeaponName.GetComponent<RectTransform>().sizeDelta = new Vector2(95.24f, 13.74f);

            GameObject SkinName = new GameObject("SkinName");
            SkinName.transform.parent = Item.transform;
            tmp = SkinName.AddComponent<TextMeshProUGUI>();
            tmp.text = wpskin;
            tmp.fontSize = 24;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = new Color(138, 138, 138);
            if (wpskin.Length > 16)
            {
                SkinName.transform.localPosition = new Vector3(0, -57.185f, 0);
                SkinName.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 28.89f);
            }

            else
            {
                SkinName.transform.localPosition = new Vector3(14.656f, -57.185f, 0);
                SkinName.GetComponent<RectTransform>().sizeDelta = new Vector2(222.13f, 28.89f);
            }
            SkinName.transform.localScale = Vector3.one;
            GameObject Condition = new GameObject("Condition");
            Condition.transform.parent = Item.transform;
            tmp = Condition.AddComponent<TextMeshProUGUI>();
            switch (Soloweapons[i].Condition)
            {
                case 1:
                    {
                        tmp.text = "Fabrikneu";
                        break;
                    }
                case 2:
                    {
                        tmp.text = "Minimale Gebrauchsspuren";
                        break;
                    }
                case 3:
                    {
                        tmp.text = "Einsatzerprobt";
                        break;
                    }
                case 4:
                    {
                        tmp.text = "Abgenutzt";
                        break;
                    }
                case 5:
                    {
                        tmp.text = "Kampfspuren";
                        break;
                    }
            }
            tmp.fontSize = 13;
            tmp.fontStyle = FontStyles.Bold;
            color = Color.white;
            color.a = 0.7f;
            tmp.color = color;

            Condition.transform.localPosition = new Vector3(16.72f, -76.4f, 0);
            Condition.transform.localScale = Vector3.one;
            Condition.GetComponent<RectTransform>().sizeDelta = new Vector2(210.05f, 13.74f);


            GameObject buttonGameObject = new GameObject("Button", typeof(RectTransform), typeof(Image), typeof(Button));
            buttonGameObject.transform.SetParent(Item.transform);
            buttonGameObject.transform.localPosition = new Vector3(0f, -116f, 0);
            buttonGameObject.transform.localScale = Vector3.one;

            RectTransform buttonRectTransform = buttonGameObject.GetComponent<RectTransform>();
            buttonRectTransform.sizeDelta = new Vector2(208.66f, 50.76f);

            Image buttonImage = buttonGameObject.GetComponent<Image>();
            buttonImage.sprite = Resources.Load<Sprite>("Images/Farbverlauf Nach unten");

            Button buttonComponent = buttonGameObject.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => BuyWeapon(buttonComponent.transform.parent.name, buttonComponent.transform.parent.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text, buttonComponent.transform.parent.transform.GetChild(10).GetComponent<Image>().enabled));

        GameObject buttonChild = new GameObject("Text", typeof(TextMeshProUGUI));
            buttonChild.transform.parent = buttonGameObject.transform;
            buttonChild.transform.localScale = Vector3.one;
            buttonChild.transform.localPosition = Vector3.zero;

            TextMeshProUGUI buttonText = buttonChild.GetComponent<TextMeshProUGUI>();
            buttonText.text = "Kaufen";
            buttonText.fontSize = 24;
            buttonText.fontStyle = FontStyles.Bold;
            buttonText.alignment = TextAlignmentOptions.Center;

            GameObject WeaponStatTrak = new GameObject("WeaponStatTrak");
            WeaponStatTrak.transform.parent = Item.transform;
            WeaponStatTrak.AddComponent<Image>();
            WeaponStatTrak.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/StatTrak");
            WeaponStatTrak.transform.localPosition = new Vector3(83.5f, 39.396f, 0);
            WeaponStatTrak.transform.localScale = Vector3.one;
            WeaponStatTrak.GetComponent<RectTransform>().sizeDelta = new Vector2(22.15f, 28.791f);

            if (Soloweapons[i].StatTrak)
            {
                WeaponStatTrak.GetComponent<Image>().enabled = true;
            }
            else
            {
                WeaponStatTrak.GetComponent<Image>().enabled = false;
            }
            
        }


    }

    void BuyWeapon(string weaponname , string Condition , bool statTrak)
    {
        int condition = 0;
        switch (Condition)
        {
            case "Fabrikneu":
                {
                    condition = 1;
                    break;
                }
            case "Minimale Gebrauchsspuren":
                {
                    condition = 2;
                    break;
                }
            case "Einsatzerprobt":
                {
                    condition = 3;
                    break;
                }
            case "Abgenutzt":
                {
                    condition = 4;
                    break;
                }
            case "Kampfspuren":
                {
                    condition = 5;
                    break;
                }
        }
        
        if (filter)
        {
            for (int i = 0; i < filterlist.Count; i++)
            {
                if (filterlist[i].Name == weaponname && filterlist[i].Condition == condition && filterlist[i].StatTrak == statTrak)
                {
                    gameObject = filterlist[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < AllWeapons.Count; i++)
            {
                if (AllWeapons[i].Name == weaponname && AllWeapons[i].Condition == condition && AllWeapons[i].StatTrak == statTrak)
                {
                    gameObject = AllWeapons[i];
                }
            }
        }
        BuyItem.SetActive(true);
        string name = gameObject.Name;
        string formatiertername = name.Replace(" | ", "_");
        WeaponIMG.GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapon/" + gameObject.CaseName + "/" + formatiertername);
        WeaponName.text = gameObject.Name;
        switch (gameObject.Condition)
        {
            case 1:
                {
                    WeaponCondition.text = "Fabrikneu";
                    break;
                }
            case 2:
                {
                    WeaponCondition.text = "Minimale Gebrauchsspuren";
                    break;
                }
            case 3:
                {
                    WeaponCondition.text = "Einsatzerprobt";
                    break;
                }
            case 4:
                {
                    WeaponCondition.text = "Abgenutzt";
                    break;
                }
            case 5:
                {
                    WeaponCondition.text = "Kampfspuren";
                    break;
                }
        }
        WeaponPrice.text = gameObject.Price.ToString() + " $";
        if (gameObject.Price < 0.10f)
            WeaponPoints.text = (Mathf.CeilToInt((gameObject.Price * 10f))).ToString();
        else if (gameObject.Price > 0.09f && gameObject.Price < 1.00f)
            WeaponPoints.text = (Mathf.CeilToInt((gameObject.Price * 100f))).ToString();
        else
            WeaponPoints.text = (Mathf.CeilToInt(gameObject.Price)).ToString();

        WeaponCoins.text = (Mathf.CeilToInt((gameObject.Price * 10f))).ToString();

        if (gameObject.StatTrak)
        {
            StatTrak.GetComponent<Image>().enabled = true;
        }
    }



    public void FilterPreis()
    {
        if (bisCoins.text == "")
        {
            bisCoins.text = "0";
        }
        else if (bisPoints.text == "")
        {
            bisPoints.text = "0";
        }
        else if (abCoins.text== "")
        {
            abCoins.text = "0";
        }
        else if (abPoints.text == "")
        {
            abPoints.text = "0";
        }
        filterlist.Clear();
        for (int i = 0; i < Soloweapons.Count; i++)
        {
            if (int.Parse(bisCoins.text) != 0)
            {

                if (int.Parse(abCoins.text) <= Mathf.CeilToInt((Soloweapons[i].Price * 10f)) && int.Parse(bisCoins.text) >= Mathf.CeilToInt((Soloweapons[i].Price * 10f)))
                {
                    filterlist.Add(Soloweapons[i]);
                }
            }
            else if (int.Parse(bisPoints.text) != 0)
            {
                if (Soloweapons[i].Price < 0.10f)
                {
                    if (int.Parse(bisPoints.text) <= Mathf.CeilToInt((Soloweapons[i].Price * 10f)) && int.Parse(bisPoints.text) >= Mathf.CeilToInt((Soloweapons[i].Price * 10f)))
                        filterlist.Add(Soloweapons[i]);
                }
                else if (Soloweapons[i].Price > 0.09f && Soloweapons[i].Price < 1.00f)
                {
                    if (int.Parse(bisPoints.text) <= Mathf.CeilToInt((Soloweapons[i].Price * 100f)) && int.Parse(bisPoints.text) >= Mathf.CeilToInt((Soloweapons[i].Price * 100f)))
                        filterlist.Add(Soloweapons[i]);
                }
                else
                {
                    if (int.Parse(bisPoints.text) <= Mathf.CeilToInt(Soloweapons[i].Price) && int.Parse(bisPoints.text) >= Mathf.CeilToInt(Soloweapons[i].Price))
                    {
                        filterlist.Add(Soloweapons[i]);
                    }
                }
            }
        }
        
    }

    public void goFilter()
    {
        FilterPreis();
        for (int i = 0; i < Parent.gameObject.transform.childCount; i++)
        {
            Destroy(Parent.transform.GetChild(i).gameObject);
        }
        CreateFilterItem();
    }

    public void goConditionFilter(int condition)
    {
        FilterCondition(condition);
        for (int i = 0; i < Parent.gameObject.transform.childCount; i++)
        {
            Destroy(Parent.transform.GetChild(i).gameObject);
        }
        CreateFilterItem();
    }

    public void FilterCondition(int condition)
    {
        filterlist.Clear();
        for (int i = 0; i < AllWeapons.Count; i++)
        {
            if (AllWeapons[i].Condition == condition)
            {
                filterlist.Add(AllWeapons[i]);
            }
        }

    }

    public void CloseBuyItem()
    {
        BuyItem.SetActive(false);
    }

    public void goAwayFilter()
    {
        for (int i = 0; i < Parent.gameObject.transform.childCount; i++)
        {
            Destroy(Parent.transform.GetChild(i).gameObject);
        }
        AllCasesinList();
        AllWeaponsinList();
        CreateItem();
        filter = false;
    }

    void CreateFilterItem()
    {
        filter = true;
        for (int i = 0; i < filterlist.Count; i++)
        {
            GameObject Item = new GameObject(filterlist[i].Name);
            Item.transform.parent = Parent;
            Item.transform.localScale = new Vector3(1, 1, 1);
            Item.AddComponent<Image>();
            color = new Color(64f / 255f, 64f / 255f, 64f / 255f);
            Item.GetComponent<Image>().color = color;

            GameObject WeaponIMG = new GameObject("WeaponIMG");
            WeaponIMG.transform.parent = Item.transform;
            WeaponIMG.AddComponent<Image>();
            string name = filterlist[i].Name;
            string formatiertername = name.Replace(" | ", "_");
            WeaponIMG.GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapon/" + filterlist[i].CaseName + "/" + formatiertername);
            WeaponIMG.transform.localPosition = new Vector3(0, 80, 0);
            WeaponIMG.transform.localScale = Vector3.one;
            WeaponIMG.GetComponent<RectTransform>().sizeDelta = new Vector2(179.53f, 123);

            GameObject WeaponPrice = new GameObject("WeaponPrice");
            WeaponPrice.transform.parent = Item.transform;
            tmp = WeaponPrice.AddComponent<TextMeshProUGUI>();
            tmp.text = filterlist[i].Price.ToString() + " $";//(Soloweapons[i].Price * 0.9065f).ToString() + "€";
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontSize = 24;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = Color.white;

            WeaponPrice.transform.localPosition = new Vector3(0, 12.5f, 0);
            WeaponPrice.transform.localScale = Vector3.one;
            WeaponPrice.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 25);

            GameObject CoinsPrice = new GameObject("CoinsPrice");
            CoinsPrice.transform.parent = Item.transform;
            tmp = CoinsPrice.AddComponent<TextMeshProUGUI>();
            if (filterlist[i].Price < 0.10f)
                tmp.text = (Mathf.CeilToInt((filterlist[i].Price * 10f))).ToString();
            else if (filterlist[i].Price > 0.09f && filterlist[i].Price < 1.00f)
                tmp.text = (Mathf.CeilToInt((filterlist[i].Price * 100f))).ToString();
            else
                tmp.text = (Mathf.CeilToInt(filterlist[i].Price)).ToString();
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontSize = 24;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = Color.white;

            CoinsPrice.transform.localPosition = new Vector3(70, -16.5f, 0);
            CoinsPrice.transform.localScale = Vector3.one;
            CoinsPrice.GetComponent<RectTransform>().sizeDelta = new Vector2(99.85f, 25);

            GameObject CoinsIcon = new GameObject("CoinsIcon");
            CoinsIcon.transform.parent = Item.transform;
            CoinsIcon.transform.localPosition = new Vector3(-106, -16.5f, 0);
            CoinsIcon.transform.localScale = Vector3.one;
            CoinsIcon.AddComponent<RectTransform>();
            CoinsIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(17.87f, 17.87f);
            CoinsIcon.AddComponent<Image>();
            CoinsIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/CoinsIcon");


            GameObject PointsPrice = new GameObject("PointsPrice");
            PointsPrice.transform.parent = Item.transform;
            tmp = PointsPrice.AddComponent<TextMeshProUGUI>();
            tmp.text = (Mathf.CeilToInt((filterlist[i].Price * 10f))).ToString();
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontSize = 24;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = Color.white;

            PointsPrice.transform.localPosition = new Vector3(-47.2f, -16.5f, 0);
            PointsPrice.transform.localScale = Vector3.one;
            PointsPrice.GetComponent<RectTransform>().sizeDelta = new Vector2(99.85f, 25);

            GameObject PointsIcon = new GameObject("PointsIcon");
            PointsIcon.transform.parent = Item.transform;
            PointsIcon.transform.localPosition = new Vector3(16, -16.5f, 0);
            PointsIcon.transform.localScale = Vector3.one;
            PointsIcon.AddComponent<RectTransform>();
            PointsIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(17.87f, 17.87f);
            PointsIcon.AddComponent<Image>();
            PointsIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/PointsIcon");

            GameObject WeaponName = new GameObject("WeaponName");
            WeaponName.transform.parent = Item.transform;
            tmp = WeaponName.AddComponent<TextMeshProUGUI>();
            string weaponName = filterlist[i].Name;
            string[] parts = weaponName.Split('|');
            wpname = parts[0];
            wpskin = parts[1];
            tmp.text = wpname;
            tmp.fontSize = 13;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = new Color(138f / 255, 138f / 255, 138f / 255);

            WeaponName.transform.localPosition = new Vector3(-40.684f, -35.87f, 0);
            WeaponName.transform.localScale = Vector3.one;
            WeaponName.GetComponent<RectTransform>().sizeDelta = new Vector2(95.24f, 13.74f);

            GameObject SkinName = new GameObject("SkinName");
            SkinName.transform.parent = Item.transform;
            tmp = SkinName.AddComponent<TextMeshProUGUI>();
            tmp.text = wpskin;
            tmp.fontSize = 24;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = new Color(138, 138, 138);
            if (wpskin.Length > 16)
            {
                SkinName.transform.localPosition = new Vector3(0, -57.185f, 0);
                SkinName.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 28.89f);
            }

            else
            {
                SkinName.transform.localPosition = new Vector3(14.656f, -57.185f, 0);
                SkinName.GetComponent<RectTransform>().sizeDelta = new Vector2(222.13f, 28.89f);
            }
            SkinName.transform.localScale = Vector3.one;
            GameObject Condition = new GameObject("Condition");
            Condition.transform.parent = Item.transform;
            tmp = Condition.AddComponent<TextMeshProUGUI>();
            switch (filterlist[i].Condition)
            {
                case 1:
                    {
                        tmp.text = "Fabrikneu";
                        break;
                    }
                case 2:
                    {
                        tmp.text = "Minimale Gebrauchsspuren";
                        break;
                    }
                case 3:
                    {
                        tmp.text = "Einsatzerprobt";
                        break;
                    }
                case 4:
                    {
                        tmp.text = "Abgenutzt";
                        break;
                    }
                case 5:
                    {
                        tmp.text = "Kampfspuren";
                        break;
                    }
            }
            tmp.fontSize = 13;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = new Color(138, 138, 138);

            Condition.transform.localPosition = new Vector3(16.72f, -76.4f, 0);
            Condition.transform.localScale = Vector3.one;
            Condition.GetComponent<RectTransform>().sizeDelta = new Vector2(210.05f, 13.74f);


            GameObject buttonGameObject = new GameObject("Button", typeof(RectTransform), typeof(Image), typeof(Button));
            buttonGameObject.transform.SetParent(Item.transform);
            buttonGameObject.transform.localPosition = new Vector3(0f, -116f, 0);
            buttonGameObject.transform.localScale = Vector3.one;

            RectTransform buttonRectTransform = buttonGameObject.GetComponent<RectTransform>();
            buttonRectTransform.sizeDelta = new Vector2(208.66f, 50.76f);

            Image buttonImage = buttonGameObject.GetComponent<Image>();
            buttonImage.sprite = Resources.Load<Sprite>("Images/Farbverlauf Nach unten");

            Button buttonComponent = buttonGameObject.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => BuyWeapon(buttonComponent.transform.parent.name, buttonComponent.transform.parent.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text, buttonComponent.transform.parent.transform.GetChild(10).GetComponent<Image>().enabled));

            GameObject buttonChild = new GameObject("Text", typeof(TextMeshProUGUI));
            buttonChild.transform.parent = buttonGameObject.transform;
            buttonChild.transform.localScale = Vector3.one;
            buttonChild.transform.localPosition = Vector3.zero;

            TextMeshProUGUI buttonText = buttonChild.GetComponent<TextMeshProUGUI>();
            buttonText.text = "Kaufen";
            buttonText.fontSize = 24;
            buttonText.fontStyle = FontStyles.Bold;
            buttonText.alignment = TextAlignmentOptions.Center;

            GameObject WeaponStatTrak = new GameObject("WeaponStatTrak");
            WeaponStatTrak.transform.parent = Item.transform;
            WeaponStatTrak.AddComponent<Image>();
            WeaponStatTrak.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/StatTrak");
            WeaponStatTrak.transform.localPosition = new Vector3(83.5f, 39.396f, 0);
            WeaponStatTrak.transform.localScale = Vector3.one;
            WeaponStatTrak.GetComponent<RectTransform>().sizeDelta = new Vector2(22.15f, 28.791f);

            if (filterlist[i].StatTrak)
            {
                WeaponStatTrak.GetComponent<Image>().enabled = true;
            }
            else
            {
                WeaponStatTrak.GetComponent<Image>().enabled = false;
            }
        }


    }

    public void Knifefilter()
    {
        filterlist.Clear();
        for (int i = 0; i < AllWeapons.Count; i++)
        {
            string weaponName = AllWeapons[i].Name;
            string[] parts = weaponName.Split('|');
            wpname = parts[0];

            if (wpname == "Nomad Knife " || wpname == "Skeleton Knife " || wpname == "Survival Knife " || wpname == "Paracord Knife " || wpname == "Classic Knife " || wpname == "Bayonet " || wpname == "Bowie Knife " || wpname == "Butterfly Knife " || wpname == "Falchion Knife " || wpname == "Flip Knife " ||
               wpname == "Gut Knife " || wpname == "Huntsman Knife" || wpname == "Karambit " || wpname == "M9 Bayonet " || wpname == "Shadow Daggers " || wpname == "Stiletto Knife " || wpname == "Talon Knife " || wpname == "Ursus Knife ")
            {
                filterlist.Add(AllWeapons[i]);
            }
        }
        for (int i = 0; i < Parent.gameObject.transform.childCount; i++)
        {
            Destroy(Parent.transform.GetChild(i).gameObject);
        }
        CreateFilterItem();
    }

    public void Pistolefilter()
    {
        filterlist.Clear();
        for (int i = 0; i < AllWeapons.Count; i++)
        {
            string weaponName = AllWeapons[i].Name;
            string[] parts = weaponName.Split('|');
            wpname = parts[0];
            if (wpname == "CZ75-Auto " || wpname == "Desert Eagle " || wpname == "Dual Berettas " || wpname == "Five-SeveN " || wpname == "Glock-18 " ||
                wpname == "P2000 " || wpname == "P250 " || wpname == "R8 Revolver " || wpname == "Tec-9 " || wpname == "USP-S " )
            {
                filterlist.Add(AllWeapons[i]);
            }
        }
        for (int i = 0; i < Parent.gameObject.transform.childCount; i++)
        {
            Destroy(Parent.transform.GetChild(i).gameObject);
        }
        CreateFilterItem();
    }

    public void Gewehrfilter()
    {
        filterlist.Clear();
        for (int i = 0; i < AllWeapons.Count; i++)
        {
            string weaponName = AllWeapons[i].Name;
            string[] parts = weaponName.Split('|');
            wpname = parts[0];
            if (wpname == "AK-47 " || wpname == "AUG " || wpname == "AWP " || wpname == "FAMAS " || wpname == "G3SG1 " ||
                wpname == "Galil AR " || wpname == "M4A1-S " || wpname == "M4A4 " || wpname == "SCAR-20 " || wpname == "SG 533 " || wpname == "SSG 08 ")
            {
                filterlist.Add(AllWeapons[i]);
            }
        }
        for (int i = 0; i < Parent.gameObject.transform.childCount; i++)
        {
            Destroy(Parent.transform.GetChild(i).gameObject);
        }
        CreateFilterItem();
    }

    public void MPfilter()
    {
        filterlist.Clear();
        for (int i = 0; i < AllWeapons.Count; i++)
        {
            string weaponName = AllWeapons[i].Name;
            string[] parts = weaponName.Split('|');
            wpname = parts[0];
            if (wpname == "MAC-10 " || wpname == "MP5-SD " || wpname == "MP7 " || wpname == "MP9 " || wpname == "PP-Bizon " ||
                wpname == "P90 " || wpname == "UMP-45 " )
            {
                filterlist.Add(AllWeapons[i]);
            }
        }
        for (int i = 0; i < Parent.gameObject.transform.childCount; i++)
        {
            Destroy(Parent.transform.GetChild(i).gameObject);
        }
        CreateFilterItem();
    }

    public void Schwerfilter()
    {
        filterlist.Clear();
        for (int i = 0; i < AllWeapons.Count; i++)
        {
            string weaponName = AllWeapons[i].Name;
            string[] parts = weaponName.Split('|');
            wpname = parts[0];
            if (wpname == "MAG-7 " || wpname == "Nova " || wpname == "Sawed-Off " || wpname == "XM1014 " || wpname == "M249 " ||
                wpname == "Negev ")
            {
                filterlist.Add(AllWeapons[i]);
            }
        }
        for (int i = 0; i < Parent.gameObject.transform.childCount; i++)
        {
            Destroy(Parent.transform.GetChild(i).gameObject);
        }
        CreateFilterItem();
    }

}
