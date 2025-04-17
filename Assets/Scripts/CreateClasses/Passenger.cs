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
    private Animator anim;

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
    private Counter counter;
    
    public SlimeAnimationState currentState; 

    private void Start()
    {
        var house = GameObject.FindGameObjectWithTag("house");
        text = transform.Find("Number").GetComponent<TextMesh>();
        text.text = (house.transform.childCount - FinishFloor - 1).ToString();
        // anim = GetComponent<Animator>();
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

        var isMoving = MoveRight || MoveLeft;
        var jump = anim.GetBool("Walk");

        if (isMoving && !jump)
        {
            anim.SetFloat("Speed", 1.0f);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetFloat("Speed", 0.0f);
            anim.SetBool("Idle", true);
        }
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
                    Debug.Log("Elevator: " + other.transform.GetSiblingIndex() + " " + StartFloor);
                    if (!exit)
                    {
                        Enter = true;
                        MoveRight = false;
                    }
                    break;
                }
            case "floor":
                {
                    Debug.Log("Floor: " + other.transform.GetSiblingIndex() + " " + StartFloor);
                    if (exit)
                    {
                        transform.position = other.transform.Find("Ground").GetChild(0).position;
                        transform.SetParent(other.transform);
                    }
                    break;
                }
            case "leftWall":
                {
                    Debug.Log("Left wall: " + other.transform.GetSiblingIndex() + " " + StartFloor);
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
        // Debug.Log("Destroy from floor: " + " " + StartFloor);
        if (GameObject.FindGameObjectWithTag("house") == null)
        {
            Debug.Log("House is null: " + StartFloor);
            return;
        }
        else if (GameObject.FindGameObjectWithTag("house").transform.GetChild(StartFloor) == null)
        {
            Debug.Log("House child is null: " + StartFloor);
            return;
        }
        else if (GameObject.FindGameObjectWithTag("house").transform.GetChild(StartFloor).GetComponent<Queue>() == null)
        {
            Debug.Log("Queue is null: " + StartFloor);
            return;
        }
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
        // Rotate();
        MoveLeft = true;
        exit = true;
    }

    void OnAnimatorMove()
    {
        // apply root motion to AI
        var position = anim.rootPosition;

        if (MoveRight)
            transform.position += Vector3.right / 20;

        if (MoveLeft)
            transform.position += Vector3.left / 20;
    }

    private void Rotate() => transform.Rotate(0, 180, 0);
}