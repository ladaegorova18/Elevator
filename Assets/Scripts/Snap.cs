using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Snap : MonoBehaviour
{
    [SerializeField]
    private Transform centralPoint;
    //это пустой GameObject который нужно создать как дочерний объект к вашему объекту на котором назначен ScrollRect
    //далее этот centralPoint нужно расположить на канвасе так как нужно вам - относительно этой точки и будет снаппинг элементов скролла

    //тут храним расстояние между родительским объектом элементов скролла и всех элем. скролла
    private List<float> distancesToCenter = new List<float>();

    //происходит ли драг скролла
    private bool dragging;
    private Vector3 position;
    private bool tapped = false;
    private Elevator elevator;

    private Vector3 destination;
    private ScrollRect scroll;
    private bool passengers;

    private void Start()
    {
        position = Input.mousePosition;
        elevator = GameObject.FindGameObjectWithTag("elevator").GetComponent<Elevator>();
        scroll = GameObject.FindGameObjectWithTag("Scroll").GetComponent<ScrollRect>();
        destination = transform.position;
        passengers = true;
    }

    private void FixedUpdate()
    {
        GetFloor();
        if (BoundFloor())
            scroll.StopMovement();
        CalcDistanceCenterToFloors();
        StopOnTheFloor();
        passengers = (scroll.velocity.magnitude < 80);
    }

    private bool BoundFloor() => elevator.currentFloor == 1 || elevator.currentFloor == transform.childCount - 2;

    private void Update()
    {
        /// управление, если нажатие происходит в любом месте экрана
        if (Input.GetMouseButton(0) && !tapped)
        {
            MoveHouse(Input.mousePosition);
            dragging = true;
        }
        else if (!tapped)
            dragging = false;
    }

    private void OnMouseDrag()
    {
        tapped = true;
        Vector3 mousePosVector = ConvertMousePosition();
        MoveHouse(mousePosVector);
        dragging = true;
    }

    private void MoveHouse(Vector3 mousePosVector)
    {
        var vector = (mousePosVector - position).y * Vector3.up;
        transform.position += vector.normalized * 0.2f;
        position = mousePosVector;
    }

    private static Vector3 ConvertMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        var mousePosVector = new Vector3();
        if (Physics.Raycast(ray, out hit))
            mousePosVector = hit.point;
        return mousePosVector;
    }

    private void OnMouseUp() => tapped = false;

    /// <summary>
    /// на каком этаже лифт сейчас
    /// </summary>
    private void GetFloor()
    {
        for (int i = 0; i < distancesToCenter.Count; i++)
        {
            if (distancesToCenter[i] == distancesToCenter.Min())
            {
                elevator.currentFloor = i;
            }
        }
    }

    ////этот метод будет вызван на момент конца драга (когда отпустят кнопку мыши)
    //снаппинг(подгонка) фото к центру
    public void StopOnTheFloor()
    {
        if (distancesToCenter[elevator.currentFloor] < 0.03f)
            scroll.StopMovement();

        //если скролл не драгается
        if (dragging == false && scroll.velocity.magnitude < 60)
        {
            GetFloor();
            //перемещаем контент-объект с со всеми элементами (центровка самого ближнего к центру элемента)
            var posY = Mathf.Lerp(transform.position.y, transform.position.y - distancesToCenter[elevator.currentFloor], Time.deltaTime);
            //if (Mathf.Abs(posY - distancesToCenter[elevator.currentFloor]) > 0.05f)
            //{
                var changeHouseVector = centralPoint.position - transform.GetChild(elevator.currentFloor).position;
                destination = transform.position + changeHouseVector;
                transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * 25);
                if (passengers)
                    elevator.Passengers();
            //}z
        }
    }

    //получаем дистанцию каждой фотографии до скролл-объекта
    public void CalcDistanceCenterToFloors()
    {
        distancesToCenter.Clear();
        for (var i = 0; i < transform.childCount; ++i)
        {
            //вычисляем дистанцию между скролл-контент точкой и фото
            distancesToCenter.Add(Mathf.Abs(centralPoint.position.y - transform.GetChild(i).position.y));
        }
    }
}
