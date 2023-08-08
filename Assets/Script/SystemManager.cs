using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{
    public void NextScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

}
