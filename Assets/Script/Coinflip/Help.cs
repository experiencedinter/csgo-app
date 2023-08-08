using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Help : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject helpBox;
    
    private bool isHovering = false;

    private void Start()
    {
        helpBox.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        ShowHelp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        HideHelp();
    }

    private void ShowHelp()
    {
        helpBox.SetActive(true);
        //helpBox.transform.position = transform.position + new Vector3(50f, 50f, 0f);
    }

    private void HideHelp()
    {
        helpBox.SetActive(false);
    }
}
