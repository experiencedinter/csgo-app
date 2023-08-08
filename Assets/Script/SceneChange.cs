using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    
    public string targetSceneName;

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("ClickableObject"))
            SceneManager.LoadScene(targetSceneName);
    }
}
