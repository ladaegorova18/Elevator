using UnityEngine;

public abstract class Creator : MonoBehaviour
{
    public abstract Passenger GetPassenger(int floorCount, int personID, int startFloor);
}
