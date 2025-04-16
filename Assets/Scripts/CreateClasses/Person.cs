/// <summary>
/// Обычный пассажир
/// </summary>
public class Person
{
    public int id { get; set; } = 0;
    public int StartFloor { get; set; }

    public int FinishFloor { get; set; }
    public bool MoveRight { get; set; }
    public bool MoveLeft { get; set; }

    public int Size { get; set; } = 2;
    
    public int PersonID { get; set; }
    private System.Random rnd = new System.Random();

    public Person(int floorCount, int personID, int startFloor)
    {
        this.StartFloor = startFloor;
        //startFloor = rnd.Next(1, floorCount - 1);
        this.PersonID = personID;

        do
        {
            FinishFloor = rnd.Next(1, floorCount - 1);
        }
        while (FinishFloor == startFloor);

        MoveRight = false;
        MoveLeft = false;
    }
}

/// <summary>
/// Ребенок
/// </summary>
public class Child : Person
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
public class FatGuy : Person
{
    public FatGuy(int floorCount, int personID, int startFloor) : base(floorCount, personID, startFloor)
    {
        id = 3;
        Size = 3;
    }
}
