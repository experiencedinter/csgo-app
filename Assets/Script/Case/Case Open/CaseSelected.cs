using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseSelected : MonoBehaviour
{
    public GameObject Case_Open_ZoneImg;
    public Weapon weapon;

    public string Casename;
    public bool chooseCase;
    private void Start()
    {
        Case_Open_ZoneImg = GameObject.Find("Case IMG");
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
                }
            }
        }
    }

    void CaseImageChange()
    {
        Case_Open_ZoneImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Case/" + gameObject.name);
    }
}
