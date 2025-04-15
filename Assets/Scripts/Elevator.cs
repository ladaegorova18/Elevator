using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private int limit;
    public int currentFloor { get; set; }

    private List<GameObject> passengers = new List<GameObject>();
    private List<Transform> slots = new List<Transform>();

    private int filledSlots;

    [SerializeField]
    private Material openSlot;

    [SerializeField]
    private Material closedSlot;

    // Start is called before the first frame update
    private void Start()
    {
        slots = new List<Transform>();
        for (var i = 0; i < transform.Find("Ground").childCount; ++i)
            if (transform.Find("Ground").GetChild(i).tag == "slot")
                slots.Add(transform.Find("Ground").GetChild(i));
        OpenSlots();
    }

    private void OpenSlots()
    {
        filledSlots = 0;
        foreach (var slot in slots)
            slot.GetComponent<MeshRenderer>().material = openSlot;
    }

    private void FixedUpdate() => Stabilize();

    public void Passengers()
    {
        PassengerExit();
        PassengerEnter();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "passenger")
        {
            other.transform.position = transform.position;
            other.transform.SetParent(transform);
            passengers.Add(other.gameObject);
            filledSlots += other.GetComponent<Passenger>().Size;
        }
    }

    private class UsersComparer : IEqualityComparer<GameObject>
    {
        public bool Equals(GameObject x, GameObject y) => 
            x.GetComponent<Passenger>().PersonID == y.GetComponent<Passenger>().PersonID;

        public int GetHashCode(GameObject obj) => obj.name.GetHashCode();
    }

    private void PassengerEnter()
    {
        var create = GameObject.FindGameObjectWithTag("house").GetComponent<PassengerController>();
        var count = slots.Count - filledSlots;
        create.PassengerEnter(currentFloor, count);
        //passengers.Distinct(new UsersComparer());

    }

    private void PassengerExit()
    {
        var exited = new List<GameObject>();
        foreach (var pass in passengers)
        {
            if (pass.GetComponent<Passenger>().FinishFloor == currentFloor)
            {
                exited.Add(pass);
                filledSlots -= pass.GetComponent<Passenger>().Size;
                for (var i = filledSlots; i < filledSlots + pass.GetComponent<Passenger>().Size; ++i)
                    slots[i].GetComponent<MeshRenderer>().material = openSlot;
            }
        }
        foreach (var exit in exited)
        {
            passengers.Remove(exit);
            exit.GetComponent<Passenger>().ElevatorExit();
        }
    }

    private void Stabilize()
    {
        var index = 0;

        for (var passNumber = 0; passNumber < passengers.Count; ++passNumber)
        {
            try
            {
                passengers[passNumber].transform.position = GetPosition(passNumber, ref index);
                if (index > limit)
                    index = 0; ///костыыыыль
            }
            catch (MissingReferenceException)
            {
                passengers.RemoveAt(passNumber);
            }
        }
    }

    private Vector3 GetPosition(int passNumber, ref int index)
    {
        Vector3 position = Vector3.zero;
        for (var j = 0; j < passengers[passNumber].GetComponent<Passenger>().Size; ++j)
        {
            position += slots[index].position;
            slots[index].GetComponent<MeshRenderer>().material = closedSlot;
            ++index;
        }
        position /= passengers[passNumber].GetComponent<Passenger>().Size;
        return position;
    }

    public void Clear()
    {
        for (var i = 0; i < passengers.Count; ++i)
            Destroy(passengers[i].gameObject);
        passengers.Clear();
        OpenSlots();
    }
}
