using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{
    private List<Transform> slots;
    private List<GameObject> passengers;
    private int limit = 2; // проставить ссылку

    // Start is called before the first frame update
    void Start()
    {
        passengers = new List<GameObject>();
        slots = new List<Transform>();
        var ground = gameObject.transform.Find("Ground");
        for (var i = 0; i < ground.childCount; ++i)
        {
            if (ground.GetChild(i).tag == "slot")
                slots.Add(ground.GetChild(i));
        }
    }

    public void AddPassenger(GameObject passenger)
    {
        passengers.Add(passenger);
        Stabilize();
    }

    public void Remove(int id) => 
        passengers.Remove(passengers.Find(x => x.GetComponent<Passenger>().id == id));

    private void GetExited()
    {
        var exited = new List<GameObject>();
        foreach (var pass in passengers)
        {
            if (pass.GetComponent<Passenger>().Enter)
            {
                exited.Add(pass);
            }
        }
        foreach (var exit in exited)
        {
            passengers.Remove(exit);
        }
    }

    // Update is called once per frame
    private void Stabilize()
    {
        GetExited();
        var index = 0;
        for (var i = 0; i < passengers.Count; ++i)
        {
            passengers[i].transform.position = slots[index].transform.position;
            ++index;
            if (index > limit)
                index = 0; ///костыыыыль
        }
    }

    public void Clear()
    {
        for (var i = 0; i < passengers.Count; ++i)
        {
            Destroy(passengers[i].gameObject);
        }
        passengers.Clear();
    }
}
