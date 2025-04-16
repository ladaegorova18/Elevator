using UnityEngine;

public abstract class Creator
{
    public abstract Person GetPassenger(int floorCount, int personID, int startFloor);
}
