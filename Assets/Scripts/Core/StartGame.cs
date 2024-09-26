using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{
    private int sceneIndex;

    public void StartMyGame()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneIndex);
    }
}
