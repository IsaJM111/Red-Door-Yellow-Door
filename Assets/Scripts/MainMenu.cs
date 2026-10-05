using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadSceneAsync("Level0");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
