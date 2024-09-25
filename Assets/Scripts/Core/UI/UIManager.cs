using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Win")]
    [SerializeField] private GameObject winScreen;
    private int sceneIndex;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    //  #region Win
    public void Win()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }
    // #endregion

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.Instance.PlaySound(gameOverSound);
    }
    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        //Can use it to make the game faster(2) or slower(0.5)
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void SoundVolume()
    {
        SoundManager.Instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.Instance.ChangeMusicVolume(0.2f);

    }
    public void Restart()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    /*public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }*/
    public void Quit()
    {
        Application.Quit();
    }
}
