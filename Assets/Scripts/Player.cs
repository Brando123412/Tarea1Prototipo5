using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject tailPrefab;
    private Vector2 nextPosition;

    [SerializeField] Vector2 direction;
    public List<Transform> positions;
    [SerializeField] ScoreManager SM;

    private void Awake()
    {
        positions = new List<Transform>();
    }

    private void Start()
    {
        direction = Vector2.right;
        nextPosition = CalculateInitialPosition();
        transform.position = nextPosition;

        positions.Add(transform);
    }

    private void Update()
    {
        MoveSnake();
        HandleInput();
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0)
        {
            direction = new Vector2(horizontalInput, 0f);
        }
        else if (verticalInput != 0)
        {
            direction = new Vector2(0f, verticalInput);
        }
    }

    private void MoveSnake()
    {
        nextPosition += direction * speed * Time.deltaTime;

        transform.position = nextPosition;

        for (int i = 1; i < positions.Count; i++)
        {
            if (nextPosition == (Vector2)positions[i].position)
            {
                GameManager.Instance.murio = true;
                return;
            }
        }

        positions.Insert(0, transform);
        if (positions.Count > 1)
            positions.RemoveAt(positions.Count - 1);


        if (nextPosition.x < GameManager.Instance.matrizFondo[0, 0].position.x ||
            nextPosition.x > GameManager.Instance.matrizFondo[0, GameManager.Instance.valx - 1].position.x ||
            nextPosition.y < GameManager.Instance.matrizFondo[0, 0].position.y ||
            nextPosition.y > GameManager.Instance.matrizFondo[GameManager.Instance.valy - 1, 0].position.y)
        {
            GameManager.Instance.murio = true;
        }

        MoveTail();
    }

    private void MoveTail()
    {
        for (int i = 1; i < positions.Count; i++)
        {
            positions[i].position = Vector2.Lerp(positions[i].position, positions[i - 1].position, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Comidita"))
        {
            GameManager.Instance.GenerateComida();
            SM.UpdateScore();
            AddCola();
        }
    }

    private void AddCola()
    {
        GameObject newTailSegment = Instantiate(tailPrefab, positions[positions.Count - 1].position, Quaternion.identity);
        positions.Add(newTailSegment.transform);
    }

    private Vector2 CalculateInitialPosition()
    {
        int randX = Random.Range(0, GameManager.Instance.valx - 1);
        int randY = Random.Range(0, GameManager.Instance.valy - 1);
        Vector2 initialPosition = GameManager.Instance.matrizFondo[randY, randX].position;
        return initialPosition;
    }
}
