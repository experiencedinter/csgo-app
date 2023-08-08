using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderManager : MonoBehaviour
{
    public string Scene;
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
                    SceneManager.LoadScene(Scene);
                }
            }
        }
    }
}
