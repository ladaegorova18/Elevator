using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("ComixScene");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");
    }
}