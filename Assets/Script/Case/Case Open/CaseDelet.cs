using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaseDelet : MonoBehaviour
{
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
                    DeleteCaseAllChildren();
                }
            }
        }
    }
    void DeleteCaseAllChildren()
    {
        int childCount = GameObject.Find("Case_Group").transform.childCount;
        int a = 0;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = GameObject.Find("Case_Group").transform.GetChild(i);
            if(child.name == gameObject.name && a == 0)
            {
                Destroy(child.gameObject);
                a = 1;
            }
                
        }
    }
}
