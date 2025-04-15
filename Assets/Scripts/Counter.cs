using UnityEngine;

public class Counter : MonoBehaviour
{
    private TextMesh counter;

    private int entered;
    private int lose;
    private int passengersCount;

    // Start is called before the first frame update
    void Start()
    {
        counter = GetComponent<TextMesh>();
        passengersCount = GameObject.FindGameObjectWithTag("house").GetComponent<PassengerController>().passengersCount;
        entered = 0;
        lose = 0;
    }

    private void Update() => counter.text = $"{entered}/{passengersCount}  \nLost: {lose}";

    public void AddPassenger() => ++entered;

    public void LosePassenger() => ++lose;

    public void Clear()
    {
        entered = 0;
        lose = 0;
    }
}
