using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
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

    public void OnRestartButtonClicked()
    {
        GameObject.FindGameObjectWithTag("house").GetComponent<PassengerController>().Restart();
        GameObject.FindGameObjectWithTag("elevator").GetComponent<Elevator>().Clear();
        GameObject.FindGameObjectWithTag("counter").GetComponent<Counter>().Clear();
    }

    public void OnBackButtonClicked() => SceneManager.LoadScene("TitleScene");
}