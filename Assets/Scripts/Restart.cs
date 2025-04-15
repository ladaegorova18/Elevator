using UnityEngine;

public class Restart : MonoBehaviour
{
    public void OnClick()
    {
        GameObject.FindGameObjectWithTag("house").GetComponent<PassengerController>().Restart();
        GameObject.FindGameObjectWithTag("elevator").GetComponent<Elevator>().Clear();
        GameObject.FindGameObjectWithTag("counter").GetComponent<Counter>().Clear();
    }
}
