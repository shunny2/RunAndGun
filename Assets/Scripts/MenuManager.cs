using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string sceneName;
    public GameObject shortcutPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Shortcuts()
    {
        shortcutPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        shortcutPanel.SetActive(false);
    }
}
