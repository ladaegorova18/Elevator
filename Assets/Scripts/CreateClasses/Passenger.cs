using UnityEngine;
using UnityEngine.UI;

public class Passenger : MonoBehaviour
{
    public int id { get; set; } = 0;
    public int uniqueID { get; set; } = 0;

    [SerializeField]
    private TextMesh text;

    [SerializeField]
    private Image timerImage;
    
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private ParticleSystem emitter;

    [SerializeField]
    public int StartFloor { get; set; }

    public int FinishFloor { get; set; }

    public int FloorCount { get; set; } = 0;

    public int PersonID { get; set; }

    public bool MoveRight { get; set; } = false;
    public bool MoveLeft { get; set; } = false;

    public float Limit = 15;

    public int Size { get; set; } = 2;

    public bool Enter { get; set; } = false;
    private bool exit;
    private Counter counter;

    private void Start()
    {
        var house = GameObject.FindGameObjectWithTag("house");
        text = transform.Find("Number").GetComponent<TextMesh>();
        text.text = (house.transform.childCount - FinishFloor - 1).ToString();
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
            UpdateTimerImage();
            if (Limit < 0.1f)
            {
                DestroyFromFloor();
                counter.LosePassenger();
            }
        }
        else
            timerImage.fillAmount = 0.0f;

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
        var house = GameObject.FindGameObjectWithTag("house");
        // Debug.Log("Destroy from floor: " + " " + StartFloor);
        if (house == null)
        {
            Debug.Log("House is null: " + StartFloor);
            return;
        }
        else if (house.transform.GetChild(StartFloor) == null)
        {
            Debug.Log("House child is null: " + StartFloor);
            return;
        }
        else if (house.transform.GetChild(StartFloor).GetComponent<Queue>() == null)
        {
            Debug.Log("Queue is null: " + StartFloor);
            return;
        }
        house.transform.GetChild(StartFloor).GetComponent<Queue>().Remove(id);
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
        Debug.Log("PLAY: " + StartFloor);
        emitter?.Play();
        counter.RidePasssenger();
    }

    void OnAnimatorMove()
    {
        if (MoveRight)
            transform.position += Vector3.right / 20;

        if (MoveLeft)
            transform.position += Vector3.left / 20;
    }

    private void UpdateTimerImage()
    {
        timerImage.fillAmount = Limit / 15.0f;
        if (Limit < 0.1f)
        {
            timerImage.fillAmount = 0.0f;
            timerImage.color = Color.red;
        }
        else if (Limit < 5.0f && Limit > 0.1f)
        {
            timerImage.fillAmount = Limit / 15.0f;
            timerImage.color = Color.red;
        }
        else if (Limit < 10.0f && Limit > 5.0f)
        {
            timerImage.fillAmount = Limit / 15.0f;
            timerImage.color = Color.yellow;
        }
        else if (Limit > 10.0f)
        {
            timerImage.fillAmount = Limit / 15.0f;
            timerImage.color = Color.green;
        }
    }
}