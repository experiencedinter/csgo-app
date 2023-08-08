using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Xml.Linq;

public class WeaponGridCreate : MonoBehaviour
{
    public GameObject Case_Open_ZoneImg;
    public TextAsset jsonFilePath;
    public WeaponList Case = new WeaponList();
    public List<Weapon> Soloweapons = new List<Weapon>();
   

    private Color color = Color.white;
    int colornnum;
    bool notIn;
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
    public class CaseInfos
    {
        public int ID;
        public string Name;
        public float Price;
    }
    [System.Serializable]
    public class WeaponList
    {
        public Weapon[] Case;
    }
    void Start()
    {
        Case_Open_ZoneImg = GameObject.Find("Case IMG");
        jsonFilePath = Resources.Load<TextAsset>("Database/" + gameObject.name);
        try
        {
            Case = JsonUtility.FromJson<WeaponList>(jsonFilePath.text);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "    " + gameObject.name);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.GetComponent<BoxCollider2D>() != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                        CaseImageChange();
                        WeaponListCreate();
                        WeaponObjektCreate();

                    GameObject tempName = GameObject.FindGameObjectWithTag("CaseInfo");
                    tempName.name = gameObject.name;
                }
            }
        }
    } 

    void CaseImageChange()
    {
        Case_Open_ZoneImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Case/" + gameObject.name);
        Case_Open_ZoneImg.GetComponent<Image>().enabled = true;
    }

    void WeaponListCreate()
    {
        Soloweapons.Clear();
        Soloweapons.Add(Case.Case[0]);
        for (int i = 0; i < Case.Case.Length; i++)
        {
            for (int j = 0; j < Soloweapons.Count; j++)
            {
                if (Soloweapons[j].Name != Case.Case[i].Name)
                    notIn = true;
                else
                {
                    notIn = false;
                    break;
                }
            }
            if (notIn)
            {
                Soloweapons.Add(Case.Case[i]);
            }
        }

    }

    void WeaponObjektCreate()
    {
        DeleteAllChildren();
        
        for (int i = 0; i < Soloweapons.Count; i++)
        {
            if (!notaKnife(Soloweapons[i].Name))
            {
            namen = Soloweapons[i].Name;
            GameObject weapon = new GameObject(namen);
            weapon.transform.parent = GameObject.Find("Weapon_Zone").transform;

            GameObject weaponcolor = new GameObject(namen + "Color");
            weaponcolor.transform.parent = weapon.transform;
            weaponcolor.transform.localScale = new Vector2(0.03f, 1.14f);
            weaponcolor.transform.position = new Vector3(-70, 0, 0);
            weaponcolor.AddComponent<Image>();
            for (int j = 0; j < Soloweapons.Count; j++)
            {
                if (weapon.name == Soloweapons[j].Name)
                {
                    colornnum = Soloweapons[j].Color;
                    switch (colornnum)
                    {
                        case 1:
                            color = new Color(176f / 255f, 195f / 255f, 217f / 255f);
                            weaponcolor.GetComponent<Image>().color = color;
                            break;
                        case 2:
                            color = new Color(94f / 255f, 152f / 255f, 217f / 255f);
                            weaponcolor.GetComponent<Image>().color = color;
                            break;
                        case 3:
                            color = new Color(75f / 255f, 105f / 255f, 255f / 255f);
                            weaponcolor.GetComponent<Image>().color = color;
                            break;
                        case 4:
                            color = new Color(136f / 255f, 71f / 255f, 255f / 255f);
                            weaponcolor.GetComponent<Image>().color = color;
                            break;
                        case 5:
                            color = new Color(211f / 255f, 44f / 255f, 230f / 255f);
                            weaponcolor.GetComponent<Image>().color = color;
                            break;
                        case 6:
                            color = new Color(235f / 255f, 75f / 255f, 75f / 255f);
                            weaponcolor.GetComponent<Image>().color = color;
                            break;
                        case 7:
                            color = new Color(158f / 255f, 132f / 255f, 0f / 255f);
                            weaponcolor.GetComponent<Image>().color = color;
                            break;
                    }
                }
            }

            GameObject weaponbackground = new GameObject(namen + "Background");
            weaponbackground.transform.parent = weapon.transform;
            weaponbackground.transform.localScale = new Vector2(1.37f, 1.14f);
            weaponbackground.transform.position = new Vector3(0, 0, 0);
            string name = namen;
            string formatiertername = name.Replace(" | ", "_");
            weaponbackground.AddComponent<Image>().sprite = Resources.Load<Sprite>("Weapon/" + Soloweapons[0].CaseName + "/" + formatiertername);



            weapon.AddComponent<Image>().sprite = Resources.Load<Sprite>("Weapon/Background/BackgroundWeapons");

            weapon.transform.localScale = new Vector3(1, 1, 1);
            weapon.AddComponent<BoxCollider2D>().size = new Vector2(137, 114);


            }

        }
    }
    void DeleteAllChildren()
    {
        int childCount = GameObject.Find("Weapon_Zone").transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = GameObject.Find("Weapon_Zone").transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    bool notaKnife(string name)
    {
        string weaponName = name;
        string[] parts = weaponName.Split('|');
        name = parts[0];
        if (name == "Nomad Knife " || name == "Skeleton Knife " || name == "Survival Knife " || name == "Paracord Knife " || name == "Classic Knife " || name == "Bayonet " || name == "Bowie Knife " || name == "Butterfly Knife " || name == "Falchion Knife " || name == "Flip Knife " ||
        name == "Gut Knife " || name == "Huntsman Knife" || name == "Karambit " || name == "M9 Bayonet " || name == "Shadow Daggers " || name == "Stiletto Knife " || name == "Talon Knife " || name == "Ursus Knife " || name == "Bowie Knife " ||name == "Huntsman Knife " || name == "Navaja Knife " 
        || name == "Ursus Knife " || name == "Huntsman Knife ")
        {
            return true;
        }
        return false;
    }
    
    
}

