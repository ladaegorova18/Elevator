using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateHouse : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> _floors;

    [SerializeField]
    private int count = 4;

    [SerializeField]
    private GameObject border;


    private void Start()
    {
        Instantiate(border, this.transform);
        for (var floorNumber = count; floorNumber > 0; --floorNumber)
        {
            // var newFloor = _floors[3]; 
            var newFloor = _floors[Random.Range(0, _floors.Count)]; 
            var floor = Instantiate(newFloor, this.transform);
            floor.GetComponentInChildren<Text>().text = floorNumber.ToString();
        }
        Instantiate(border, this.transform);

        GetComponent<PassengerController>().Passengers(count);
    }
}
