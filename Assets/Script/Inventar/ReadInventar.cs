using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadInventar : MonoBehaviour
{
    public List<Inventar> inventar = new List<Inventar>();

    public TMP_Text Seiten;

    public Image Weaponbackground;
    public Image WeaponIMG;

    public int maxPerPage = 42;
    private int currentPage = 0;

    public Button nextPageButton; 
    public Button prevPageButton;

    public int pageCount = 0;
    public int min = 0;
    public int max = 42;

    int colornnum;

    private Color color = Color.white;

    public Transform Parent;

    private void Update()
    {
        Seiten.text = (currentPage + 1 ).ToString() + " / " + pageCount.ToString();
    }
    private void Awake()
    {
        Weaponbackground.enabled = false;
        WeaponIMG.enabled = false;
        LoadInventarData();
        UpdatePageButtons();
        CreateWeapon();
        maxSite();
    }
    public void Newload()
    {
        LoadInventarData();
        UpdatePageButtons();
        CreateWeapon();
        maxSite();
    }
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

    public void nextSite()
    {
        if (inventar.Count - currentPage * maxPerPage > maxPerPage)
        {
            currentPage++;
            CreateWeapon();
            UpdatePageButtons();
        }
    }
    public void BackSite()
    {
        if (currentPage > 0)
        {
            currentPage--;
            CreateWeapon();
            UpdatePageButtons();
        }

    }
    
    void maxSite()
    {
        int maxPerPage = 42;
        pageCount = Mathf.Max(Mathf.CeilToInt((float)inventar.Count / maxPerPage), 1);

    }
    void UpdatePageButtons()
    {
        prevPageButton.interactable = currentPage > 0;

        nextPageButton.interactable = inventar.Count - currentPage * maxPerPage > maxPerPage;
    }
    void CreateWeapon()
    {
        ClearWeapon();
        int startIndex = currentPage * maxPerPage;
        int endIndex = Mathf.Min(startIndex + maxPerPage - 1, inventar.Count - 1);

        for (int k = startIndex ; k <= endIndex; k++)
        {
            GameObject weapon = new GameObject(inventar[k].Name);
            weapon.transform.parent = Parent;
            weapon.AddComponent<ShowWeaponInfo>();

            GameObject weaponcolor = new GameObject(inventar[k].Color.ToString());
            weaponcolor.transform.parent = weapon.transform;
            weaponcolor.transform.localPosition = new Vector3(-69f, 0f, 0f);
            weaponcolor.transform.localScale = new Vector2(0.03f, 1);
            weaponcolor.AddComponent<RectTransform>().sizeDelta = new Vector2(100, 83);
           
            weaponcolor.AddComponent<Image>();

            colornnum = inventar[k].Color;
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
            GameObject weaponbackground = new GameObject(inventar[k].Name + " Background");
            weaponbackground.transform.parent = weapon.transform;
            weaponbackground.transform.localScale = new Vector2(1.37f, 1.14f);
            weaponbackground.AddComponent<RectTransform>().sizeDelta= new Vector2(83, 83);
            weaponbackground.transform.position = new Vector3(0, 0, 0);

            GameObject weaponStatTrak = new GameObject(inventar[k].StatTrak.ToString());
            weaponStatTrak.transform.parent = weapon.transform;

            GameObject weaponCondition = new GameObject(inventar[k].Condition.ToString());
            weaponCondition.transform.parent = weapon.transform;

            string name = inventar[k].Name;
            string formatiertername = name.Replace(" | ", "_");
            weaponbackground.AddComponent<Image>().sprite = Resources.Load<Sprite>("Weapon/" + inventar[k].CaseName + "/" + formatiertername);

            weapon.AddComponent<Image>().sprite = Resources.Load<Sprite>("Weapon/Background/BackgroundWeapons");

            weapon.transform.localScale = new Vector3(1, 1, 1);
            weapon.AddComponent<BoxCollider2D>().size = new Vector2(137, 83);
        }
    }
    void ClearWeapon()
    {
        int childCount = Parent.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = Parent.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public void CreateFilterWeapon(List<Inventar> inventar)
    {
        ClearWeapon();
        int startIndex = currentPage * maxPerPage;
        int endIndex = Mathf.Min(startIndex + maxPerPage - 1, inventar.Count - 1);


        for (int k = startIndex; k <= endIndex; k++)
        {
            GameObject weapon = new GameObject(inventar[k].Name);
            weapon.transform.parent = Parent;
            weapon.AddComponent<ShowWeaponInfo>();

            GameObject weaponcolor = new GameObject(inventar[k].Color.ToString());
            weaponcolor.transform.parent = weapon.transform;
            weaponcolor.transform.localPosition = new Vector3(-69f, 0f, 0f);
            weaponcolor.transform.localScale = new Vector2(0.03f, 1);
            weaponcolor.AddComponent<RectTransform>().sizeDelta = new Vector2(100, 83);


            weaponcolor.AddComponent<Image>();

            colornnum = inventar[k].Color;
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

            GameObject weaponbackground = new GameObject(inventar[k].Name + " Background");
            weaponbackground.transform.parent = weapon.transform;
            weaponbackground.transform.localScale = new Vector2(1.37f, 1.14f);
            weaponbackground.AddComponent<RectTransform>().sizeDelta = new Vector2(83, 83);
            weaponbackground.transform.position = new Vector3(0, 0, 0);

            GameObject weaponStatTrak = new GameObject(inventar[k].StatTrak.ToString());
            weaponStatTrak.transform.parent = weapon.transform;

            GameObject weaponCondition = new GameObject(inventar[k].Condition.ToString());
            weaponCondition.transform.parent = weapon.transform;

            string name = inventar[k].Name;
            string formatiertername = name.Replace(" | ", "_");
            weaponbackground.AddComponent<Image>().sprite = Resources.Load<Sprite>("Weapon/" + inventar[k].CaseName + "/" + formatiertername);

            weapon.AddComponent<Image>().sprite = Resources.Load<Sprite>("Weapon/Background/BackgroundWeapons");

            weapon.transform.localScale = new Vector3(1, 1, 1);
            weapon.AddComponent<BoxCollider2D>().size = new Vector2(137, 83);

        }

    }

}
