using UnityEngine;

public abstract class Creator
{
    public abstract Passenger GetPassenger(int floorCount, int personID, int startFloor);
}
