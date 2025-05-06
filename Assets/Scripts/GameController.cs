using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject levelCompletePanel;

    private GameObject house;
    private GameObject elevator;
    private Counter counter;

    private void Start()
    {
        house = GameObject.FindGameObjectWithTag("house");
        elevator = GameObject.FindGameObjectWithTag("elevator");
        counter = GameObject.FindGameObjectWithTag("counter").GetComponent<Counter>();   
        counter.OnFinishLevel += FinishLevel;
        elevator.SetActive(true);
    }

    public void OnRestartButtonClicked()
    {
        levelCompletePanel.SetActive(false);
        house.GetComponent<PassengerController>().Restart();
        elevator.GetComponent<Elevator>().Clear();
        counter.GetComponent<Counter>().Clear();
        elevator.SetActive(true);
    }

    public void OnBackButtonClicked() => SceneManager.LoadScene("TitleScene");

    public void FinishLevel(float successRate)
    {
        counter.OnFinishLevel -= FinishLevel;
        
        levelCompletePanel.SetActive(true);
        levelCompletePanel.GetComponent<LevelCompleted>().SetResult(successRate);

        house.GetComponent<PassengerController>().StopAllCoroutines();
        elevator.SetActive(false);
    }
}