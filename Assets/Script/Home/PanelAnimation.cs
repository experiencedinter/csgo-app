using UnityEngine;

public class PanelAnimation : MonoBehaviour
{
    public Animator animator;
    bool PanelOpen = true;

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
                    if (animator != null)
                    {
                        PanelOpen = animator.GetBool("Panel Open");

                        animator.SetBool("Panel Open", !PanelOpen);


                    }
                }
            }
        }
    }
}
