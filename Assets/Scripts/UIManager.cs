using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverCanvas;
    public static UIManager instance;

    public GameObject infoCanvas;
    public GameObject BeginCanvas;
    public GameObject pauseCanvas;
 
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void OnRetry()
    {
        AudioManager.Instance.PlayButtonPress();    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnGameOver()
    {
        //Time.timeScale = 0.1f;
        gameOverCanvas.SetActive(true);
    }

    public void OnPlay()
    {
        AudioManager.Instance.PlayButtonPress();
        print("PlayButton clicked");
        SceneManager.LoadScene("MainGame");
    }

    public void OnExit() 
    {
        AudioManager.Instance.PlayButtonPress();
        Application.Quit();
    }

    public void OnHelp()
    {
        AudioManager.Instance.PlayButtonPress();
        BeginCanvas.SetActive(false);
        infoCanvas.SetActive(true);
    }
     public void OnBackFromHelp()
    {
        AudioManager.Instance.PlayButtonPress();
        BeginCanvas.SetActive(true);
        infoCanvas.SetActive(false);
    }

    public void OnResume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void OnPause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OnExitFromMain()
    {
        AudioManager.Instance.PlayButtonPress();
        SceneManager.LoadScene("BeginGame");
    }

}
