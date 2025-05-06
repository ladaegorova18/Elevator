using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private Text counter;

    // Number of passengers that have entered the elevator
    private int entered = 0;
    // Number of passengers that have lost
    private int lose = 0;
    // Number of passengers that have left the elevator
    private int rided;
    private int passengersCount;
    private PassengerController passengerController;

    // Start is called before the first frame update
    void Start()
    {
        counter = GetComponent<Text>();
        passengerController = GameObject.FindGameObjectWithTag("house").GetComponent<PassengerController>();
        passengersCount = passengerController.passengersCount;
    }

    private void Update() {
        counter.text = $"{rided}/{passengersCount}  \nLost: {lose}";
        if (rided + lose >= passengersCount)
        {
            FinishLevel();
        }
    }

    public void AddPassenger() => ++entered;

    public void RidePasssenger() => ++rided;

    public void LosePassenger() => ++lose;

    public void Clear()
    {
        entered = 0;
        lose = 0;
    }

    public void FinishLevel() {
        Debug.Log("Level finished");
        passengerController.StopAllCoroutines();
    }
}
