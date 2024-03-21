using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed;
    [SerializeField] Vector2 direction;
    public Queue<Vector2> positions;

    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Comidita"))
        {
            GameManager.Instance.GenerateComida();
        }
    }

    private void MoveSnake()
    {
        Vector2 newPosition = positions.Peek() + (direction * speed * Time.deltaTime);

        // Actualizar la cola de posiciones de la serpiente
        positions.Enqueue(newPosition);
        if (positions.Count > 1)
            positions.Dequeue(); // Eliminar la posición más antigua

        // Mover el objeto de la serpiente a la nueva posición
        transform.position = newPosition;
    }
}
