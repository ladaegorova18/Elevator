using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    [SerializeField]
    public int passengersCount;

    [SerializeField]
    private GameObject passenger;

    [SerializeField]
    private GameObject child;

    [SerializeField]
    private GameObject fatGuy;

    [SerializeField]
    private int limitOnFloor;

    [SerializeField]
    private int interval;

    private int floorCount;

    private Coroutine currentCoroutine;
    private System.Random rnd = new System.Random();

    public void Passengers(int floorCount)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        var passengersOnFloors = new int[floorCount];
        var list = new List<Passenger>();
        var peopleCreator = new PeopleCreator();
        var childrenCreator = new ChildrenCreator();
        var fatGuysCreator = new FatGuysCreator();
        for (var i = 0; i < passengersCount / 3; ++i)
        {
            var person = GetPassengerOnFloor(floorCount, passengersOnFloors, peopleCreator, i);
            var child = GetPassengerOnFloor(floorCount, passengersOnFloors, childrenCreator, i + 1);
            var fatGuy = GetPassengerOnFloor(floorCount, passengersOnFloors, fatGuysCreator, i + 2);
            ++passengersOnFloors[person.StartFloor];
            ++passengersOnFloors[child.StartFloor];
            ++passengersOnFloors[fatGuy.StartFloor];
            list.Add(person);
            list.Add(child);
            list.Add(fatGuy);
            list = Mix(list);
        }
        currentCoroutine = StartCoroutine(Creator(interval, list));
        this.floorCount = floorCount;
    }

    private List<Passenger> Mix(List<Passenger> list)
    {
        var rnd = new System.Random();
        var newList = list;
        for (int i = 0; i < list.Count; i++)
        {
            var tmp = list[i];
            list.RemoveAt(i);
            list.Insert(rnd.Next(list.Count), tmp);
        }
        return list;
    }

    private IEnumerator Creator(float sec, List<Passenger> list)
    {
        foreach (var person in list)
        {
            yield return new WaitForSeconds(sec);
            GameObject objectPassenger = new GameObject();
            switch(person.GetType().Name)
            {
                case ("Person"):
                    {
                        objectPassenger = Instantiate(passenger, transform.GetChild(person.StartFloor), false);
                        break;
                    }
                case ("Child"):
                    {
                        objectPassenger = Instantiate(child, transform.GetChild(person.StartFloor), false);
                        break;
                    }
                case ("FatGuy"):
                    {
                        objectPassenger = Instantiate(fatGuy, transform.GetChild(person.StartFloor), false);
                        break;
                    }
            }

            objectPassenger.transform.position = transform.GetChild(person.StartFloor).Find("Ground").position;
            AddPass(person.StartFloor, objectPassenger);

            var pass = objectPassenger.GetComponent<Passenger>();

            //pass = person;
            pass.StartFloor = person.StartFloor;
            pass.FinishFloor = person.FinishFloor;
            pass.MoveRight = true; 
            pass.PersonID = person.PersonID;
            pass.Limit = 15;
            pass.Size = person.Size;
        }
    }

    public void AddPass(int floor, GameObject objectPassenger) => 
        transform.GetChild(floor).GetComponent<Queue>().AddPassenger(objectPassenger);

    private Passenger GetPassengerOnFloor(int floorCount, int[] passengersOnFloors, Creator peopleCreator, int personID)
    {
        var startFloor = rnd.Next(1, floorCount - 1);
        var person = peopleCreator.GetPassenger(floorCount, personID, startFloor);

        var limit = passengersOnFloors[person.StartFloor];

        //while (limit < limitOnFloor)
        //{
        //    Debug.Log("A");
        //    person = peopleCreator.GetPassenger(floorCount, personID, startFloor); //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
        //    limit = passengersOnFloors[person.startFloor];
        //}
        return person;
    }

    public List<GameObject> PassengerEnter(int currentFloor, int count)
    {
        var passengers = new List<GameObject>();
        var thisCount = count;
        for (var i = 0; i < transform.GetChild(currentFloor).childCount; ++i)
        {
            Passenger passenger;
            var pass = transform.GetChild(currentFloor).GetChild(i);
            if (pass.TryGetComponent(out passenger) && !passengers.Contains(pass.gameObject))
            {
                if (thisCount - passenger.Size >= 0 && passenger.FinishFloor != currentFloor)
                {
                    passengers.Add(pass.gameObject);
                    passenger.ElevatorEnter();

                    thisCount -= passenger.Size;
                }
            }
        }
        return passengers;
    }

    public void Restart()
    {
        Clear();
        Passengers(floorCount);
    }

    private void Clear()
    {
        for (var i = 0; i < transform.childCount; ++i)
        {
            for (var j = 0; j < transform.GetChild(i).childCount; ++j)
            {
                Passenger passenger;
                var pass = transform.GetChild(i).GetChild(j);
                if (pass.TryGetComponent(out passenger))
                {
                    Destroy(pass.gameObject);
                }
            }
        }
        for (var i = 1; i < transform.childCount - 1; ++i)
            transform.GetChild(i).GetComponent<Queue>().Clear();
    }
}

public class PeopleCreator : Creator
{
    public override Passenger GetPassenger(int floorCount, int personID, int startFloor) => new Person(floorCount, personID, startFloor);
}

public class ChildrenCreator : Creator
{
    public override Passenger GetPassenger(int floorCount, int personID, int startFloor) => new Child(floorCount, personID, startFloor);
}

public class FatGuysCreator : Creator
{
    public override Passenger GetPassenger(int floorCount, int personID, int startFloor) => new FatGuy(floorCount, personID, startFloor);
}