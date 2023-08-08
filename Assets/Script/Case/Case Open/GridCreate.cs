using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridCreate : MonoBehaviour
{
    private CaseDatabase database = new CaseDatabase();
    private List<Case> cases = new List<Case>();
    string name;
    Sprite Icon;



    void Start()
    {
        database = GameObject.FindAnyObjectByType<CaseDatabase>();
        CreateCase();
    }
    
    void CreateCase()
    {
        for (int i = 0; i < database._case.Count; i++)
        {
            Case _CaseInfo = database.GetStats(i);
            name = _CaseInfo.Name;
            Icon = _CaseInfo.Picture;

            GameObject _case = new GameObject(name);
            _case.transform.parent = gameObject.transform;
            _case.AddComponent<Image>().sprite = Icon;
            _case.transform.localScale = new Vector3(1,1,1);
            string currentSceneName = SceneManager.GetActiveScene().name;
            Debug.Log(currentSceneName);
            if (currentSceneName == "Case Open")
            {
                _case.AddComponent<WeaponGridCreate>();
            }
            else if (currentSceneName == "Case Battle")
            {
                _case.AddComponent<CaseGridCreate>();
            }
            
            _case.AddComponent<BoxCollider2D>().size = new Vector2(180,150);
        }
    }
    
}
