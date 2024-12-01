using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private string gameSceneName;
    private void Start()
    {
        mainMenu.SetActive(true);
        creditsScreen.SetActive(false);
    }
    public void OnClickPlayButton()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnClickCreditsButton()
    {
        mainMenu.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void OnClickExitCreditsButton()
    {
        mainMenu.SetActive(true);
        creditsScreen.SetActive(false);
    }

    public void OnClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
