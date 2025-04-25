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
        var lastFloorIdx = 0;
        for (var floorNumber = count; floorNumber > 0; --floorNumber)
        {
            var newFloorIdx = Random.Range(0, _floors.Count); 
            while (newFloorIdx == lastFloorIdx)
            {
                newFloorIdx = Random.Range(0, _floors.Count); 
            }
            var newFloor = _floors[newFloorIdx];
            lastFloorIdx = newFloorIdx; 

            var floor = Instantiate(newFloor, this.transform);
            floor.GetComponentInChildren<Text>().text = floorNumber.ToString();
        }
        Instantiate(_groundObject, this.transform);

        GetComponent<PassengerController>().Passengers(count);
    }
}
