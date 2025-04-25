using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateHouse : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> _floors;

    [SerializeField]
    private GameObject _groundObject;

    [SerializeField]
    private GameObject _roofObject;

    [SerializeField]
    private int count = 8;


    private void Start()
    {
        Instantiate(_roofObject, this.transform);
        for (var floorNumber = count; floorNumber > 0; --floorNumber)
        {
            // var newFloor = _floors[3]; 
            var newFloor = _floors[Random.Range(0, _floors.Count)]; 
            var floor = Instantiate(newFloor, this.transform);
            floor.GetComponentInChildren<Text>().text = floorNumber.ToString();
        }
        Instantiate(_groundObject, this.transform);

        GetComponent<PassengerController>().Passengers(count);
    }
}
