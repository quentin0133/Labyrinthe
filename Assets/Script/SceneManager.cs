using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
