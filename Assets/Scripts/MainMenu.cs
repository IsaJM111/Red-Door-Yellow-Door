using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
