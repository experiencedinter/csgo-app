using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static WeaponGridCreate;

public class CaseGridCreate : MonoBehaviour
{
    public List<CaseInfos> caseinfo = new List<CaseInfos>();

    private CaseDatabase database = new CaseDatabase();
    Sprite Icon;

    private void Start()
    {
        database = GameObject.FindAnyObjectByType<CaseDatabase>();
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
                    CaseInfoCreate();

                }
            }
        }
    }
    void CaseInfoCreate()
    {
        caseinfo.Clear();
        Case _CaseInfo = database.GetStats(gameObject.name);
        name = _CaseInfo.Name;
        Icon = _CaseInfo.Picture;

        GameObject _case = new GameObject(name);
        _case.transform.parent = GameObject.Find("Case_Group").transform;
        _case.AddComponent<Image>().sprite = Icon;
        _case.transform.localScale = new Vector3(1, 1, 1);
        _case.AddComponent<CaseDelet>();
        _case.AddComponent<BoxCollider2D>().size = new Vector2(137, 114);
    }
}
