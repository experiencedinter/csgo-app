using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadLastItem : MonoBehaviour
{
   
    public List<Inventar> inventar = new List<Inventar>();
    public Transform Parent;
    int count = 0;
    private void LoadInventarData()
    {
        string path = Path.Combine(Application.persistentDataPath, "LastItems.json");

        if (File.Exists(path))
        {
            // Wenn die Datei existiert, lade vorhandene Daten in die Liste
            string jsonData = File.ReadAllText(path);
            inventar = JsonConvert.DeserializeObject<List<Inventar>>(jsonData);
        }
        else
        {
            List<Inventar> inventar = new List<Inventar>();
            string emptyJsonData = "[]";
            File.WriteAllText(path, emptyJsonData);
        }
    }

    private void Update()
    {
        LoadInventarData();

        if (count < inventar.Count)
        {
            ClearList();
            createList();
        }
        count = inventar.Count;
    }
    UnityEngine.Color color;
    void createList()
    {
        for (int i = 0; i < inventar.Count; i++)
        {
            if (inventar[i].Name != "")
            {
                GameObject overobject = new GameObject("OverItem");
                overobject.transform.parent = Parent;
                overobject.transform.localScale = new Vector3(1, 1, 1);
                overobject.AddComponent<Image>();
                color = new UnityEngine.Color(87f / 255f, 87f / 255f, 87f / 255f);
                overobject.GetComponent<Image>().color = color;

                GameObject item = new GameObject("Item");
                item.transform.parent = overobject.transform;
                item.AddComponent<TextMeshProUGUI>();
                item.transform.localPosition = new Vector3(-144.39f, 0);
                item.AddComponent<RectTransform>();
                item.GetComponent<RectTransform>().sizeDelta = new Vector2(69.23f, 30);
                item.transform.localScale = Vector3.one;
                item.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                item.GetComponent<TextMeshProUGUI>().fontSize = 20;
                item.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                item.GetComponent<TextMeshProUGUI>().color = UnityEngine.Color.white;
                item.GetComponent<TextMeshProUGUI>().text = i.ToString();



                GameObject itemname = new GameObject("Item");
                itemname.transform.parent = overobject.transform;
                itemname.AddComponent<TextMeshProUGUI>();
                itemname.transform.localPosition = new Vector3(21.692f, 0);
                itemname.AddComponent<RectTransform>();
                itemname.GetComponent<RectTransform>().sizeDelta = new Vector2(308.31f, 30);
                itemname.transform.localScale = Vector3.one;
                itemname.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                itemname.GetComponent<TextMeshProUGUI>().fontSize = 20;
                itemname.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                itemname.GetComponent<TextMeshProUGUI>().color = UnityEngine.Color.white;
                itemname.GetComponent<TextMeshProUGUI>().text = inventar[i].Name;
            }
            }
    }
    public void ClearList()
    {
        int childCount = Parent.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = Parent.GetChild(i);
            Destroy(child.gameObject);
        }
    }
}
