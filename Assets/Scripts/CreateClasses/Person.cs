/// <summary>
/// Обычный пассажир
/// </summary>
public class Person : Passenger
{
    public Person(int floorCount, int personID, int startFloor) : base(floorCount, personID, startFloor) => id = 1;
}

/// <summary>
/// Ребенок
/// </summary>
public class Child : Passenger
{
    public Child(int floorCount, int personID, int startFloor) : base(floorCount, personID, startFloor)
    {
        id = 2;
        Size = 1;
    }
}

/// <summary>
/// Пассажир с лишним весом
/// </summary>
public class FatGuy : Passenger
{
    public FatGuy(int floorCount, int personID, int startFloor) : base(floorCount, personID, startFloor)
    {
        id = 3;
        Size = 3;
    }
}
