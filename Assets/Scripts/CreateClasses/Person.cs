/// <summary>
/// Обычный пассажир
/// </summary>
public class Person
{
    public int id { get; set; } = 0;
    public int StartFloor { get; set; }

    public int FinishFloor { get; set; }

    public int Size { get; set; } = 2;
    
    public int PersonID { get; set; }
    private System.Random rnd = new System.Random();

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="floorCount"> Число этажей </param>
    /// <param name="personID"> Уникальный ID </param>
    public Person(int floorCount, int personID, int startFloor)
    {
        this.StartFloor = startFloor;
        this.PersonID = personID;

        do
        {
            FinishFloor = rnd.Next(1, floorCount - 1);
        }
        while (FinishFloor == startFloor);
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
