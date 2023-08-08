using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridSystem : MonoBehaviour
{
    public GameObject Gridslot; 
    public Transform slotsParent;  
    public float slotSpacing = 10f;
    

    private List<GameObject> cases = new List<GameObject>();
    private CaseDatabase caseDatabase = new CaseDatabase();

    private int numSlots;
    private string Name;
    private Sprite Icon;
    private Image img;

    private void Awake()
    {
        caseDatabase = GameObject.FindAnyObjectByType<CaseDatabase>();
    }
    private void Start()
    {
        CreateVerticalList();
    }

    void CreateVerticalList()
    {
        for (int i = 0; i < 2; i++)
        {
            for(int j = 0; j < caseDatabase._case.Count; j++)
            {
                GameObject grid = Instantiate(Gridslot) as GameObject;
                grid.transform.localScale = new Vector3(1,1,1);
                grid.transform.position = new Vector3(i, j, 0);
                grid.transform.parent = slotsParent;
            }
        }
    }
}
