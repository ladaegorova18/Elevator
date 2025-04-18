using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        // startScreen.SetActive(false);
        // gameScreen.SetActive(true);
        SceneManager.LoadScene("MainScene");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");
    }
}