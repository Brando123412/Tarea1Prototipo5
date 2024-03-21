using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]float speed;
    public GameObject tailPrefab;
    private Vector2 nextPosition;

    [SerializeField] Vector2 direction;
    public Queue<Vector2> positions= new Queue<Vector2>();
    private void Awake()
    {
        positions = new Queue<Vector2>();
}
    private void Start()
    {
        direction = Vector2.right;
        nextPosition = CalculateInitialPosition();
        transform.position = nextPosition;

        positions.Enqueue(nextPosition);
    }
    private void Update()
    {
        MoveSnake();
        HandleInput();
    }
    private void HandleInput()
    {
        // Obtener la entrada del jugador en los ejes horizontal y vertical
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Determinar la dirección de movimiento basada en la entrada del jugador
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
        // Mover la serpiente hacia la dirección actual
        nextPosition += direction * speed * Time.deltaTime;

        // Actualizar la posición de la cabeza de la serpiente
        transform.position = nextPosition;

        // Verificar si la serpiente colisiona con su propia cola
        foreach (Vector2 position in positions)
        {
            if (nextPosition == position)
            {
                GameManager.Instance.murio = true;  // La serpiente muere si colisiona con su cola
                return;
            }
        }

        // Mantener un registro de las posiciones anteriores de la serpiente
        positions.Enqueue(nextPosition);
        if (positions.Count > 1)
            positions.Dequeue();

        // Verificar si la serpiente sale de los límites de la matriz de fondo
        if (nextPosition.x < GameManager.Instance.matrizFondo[0, 0].x ||
            nextPosition.x > GameManager.Instance.matrizFondo[0, GameManager.Instance.valx - 1].x ||
            nextPosition.y < GameManager.Instance.matrizFondo[0, 0].y ||
            nextPosition.y > GameManager.Instance.matrizFondo[GameManager.Instance.valy - 1, 0].y)
        {
            GameManager.Instance.murio = true; // La serpiente muere si sale de la matriz de fondo
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Comidita"))
        {
            Grow(); 
            GameManager.Instance.GenerateComida();
        }
    }

    private void Grow()
    {
        // Crear una nueva cola en la última posición de la serpiente
        GameObject newTail = Instantiate(tailPrefab, positions.Peek(), Quaternion.identity);
        positions.Enqueue(positions.Peek());
    }
    private Vector2 CalculateInitialPosition()
    {
        int randX = Random.Range(0, GameManager.Instance.valx-1);
        int randY = Random.Range(0, GameManager.Instance.valy-1);
        Vector2 initialPosition = GameManager.Instance.matrizFondo[randY, randX];
        return initialPosition;
    }
}
