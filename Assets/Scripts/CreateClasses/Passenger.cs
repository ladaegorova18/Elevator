using System.Collections;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    public int id { get; set; } = 0;
    public int uniqueID { get; set; } = 0;

    [SerializeField]
    private TextMesh text;

    [SerializeField]
    private TextMesh timer;

    [SerializeField]
    public int StartFloor { get; set; }

    public int FinishFloor { get; set; }

    public int PersonID { get; set; }

    public bool MoveRight { get; set; }
    public bool MoveLeft { get; set; }

    public float Limit = 15;

    public int Size { get; set; } = 2;

    public bool Enter { get; set; }
    private bool exit;
    private System.Random rnd = new System.Random();
    private Animator anim;
    private Counter counter;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="floorCount"> Число этажей </param>
    /// <param name="personID"> Уникальный ID </param>
    public Passenger(int floorCount, int personID, int startFloor)
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

    private void Start()
    {
        var house = GameObject.FindGameObjectWithTag("house");
        text = transform.Find("Number").GetComponent<TextMesh>();
        text.text = (house.transform.childCount - FinishFloor - 1).ToString();
        anim = GetComponent<Animator>();
        MoveRight = false;
        MoveLeft = false;
        counter = GameObject.FindGameObjectWithTag("counter").GetComponent<Counter>();
        counter.AddPassenger();
        exit = false;
    }

    /// <summary>
    /// Терпение у пассажира не бесконечное)))
    /// </summary>
    /// <param name="sec"> Через сколько секунд клиент исчезнет </param>
    private void Update()
    {
        if (!Enter)
        {
            Limit -= Time.deltaTime;
            timer.text = System.Math.Round(Limit, 2).ToString();
            if (Limit < 0.1f)
            {
                DestroyFromFloor();
                counter.LosePassenger();
            }
        }
        else
            timer.text = "";

        if (MoveRight || MoveLeft && anim.GetCurrentAnimatorStateInfo(0).IsName("DefaultState"))
        {
            // anim.Play("Walk");
            anim.Play("Chicken_003_run");
        }
        anim.SetBool("Chicken_003_run", MoveRight || MoveLeft);
        // anim.SetBool("Walk", MoveRight || MoveLeft);

        if (MoveRight)
            transform.position += Vector3.right / 20;

        if (MoveLeft)
            transform.position += Vector3.left / 20;
    }

    /// <summary>
    /// Удар о невидимую стену - вход или выход из лифта
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "notElevator":
                {
                    // Debug.Log("Not elevator: " + other.transform.GetSiblingIndex() + " " + StartFloor);
                    if (!exit)
                    {
                        // transform.position = transform.parent.Find("Ground").GetChild(0).position;
                        // MoveRight = false;
                    }
                    break;
                }
            case "elevator":
                {
                    // Debug.Log("Elevator: " + other.transform.GetSiblingIndex() + " " + StartFloor);
                    if (!exit)
                    {
                        Enter = true;
                        MoveRight = false;
                    }
                    break;
                }
            case "floor":
                {
                    // Debug.Log("Floor: " + other.transform.GetSiblingIndex() + " " + StartFloor);
                    if (exit)
                    {
                        transform.position = other.transform.Find("Ground").GetChild(0).position;
                        transform.SetParent(other.transform);
                    }
                    break;
                }
            case "leftWall":
                {
                    // Debug.Log("Left wall: " + other.transform.GetSiblingIndex() + " " + StartFloor);
                    if (exit)
                        DestroyFromFloor();
                    break;
                }
            default:
                break;
        }
    }

    private void DestroyFromFloor()
    {
        Debug.Log("Destroy from floor: " + " " + StartFloor);
        GameObject.FindGameObjectWithTag("house").transform.GetChild(StartFloor).GetComponent<Queue>().Remove(id);
        Destroy(gameObject);
    }

    /// <summary>
    /// Войти в лифт
    /// </summary>
    public void ElevatorEnter()
    {
        if (!exit)
            MoveRight = true;
    }

    /// <summary>
    /// Выйти из лифта
    /// </summary>
    public void ElevatorExit()
    {
        Rotate();
        MoveLeft = true;
        exit = true;
    }

    private void Rotate() => transform.Rotate(0, 180, 0);
}
