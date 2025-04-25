using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public AudioSource titleAudioSource;

    [SerializeField]
    public AudioSource levelAudioSource;

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
        titleAudioSource.Stop();
        levelAudioSource.Play();
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

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("TitleScreen");
        titleAudioSource.Play();
        levelAudioSource.Stop();
    }
}