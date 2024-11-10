using UnityEngine;

public class HomeMenu : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        SceneInfo.SceneOrigin = SceneOrigin.MainMenu;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
