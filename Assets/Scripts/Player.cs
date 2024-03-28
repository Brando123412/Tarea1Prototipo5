using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject colaPrefab;
    
    public List<Vector2> positions;
    public List<Transform> colas;
    [SerializeField] Transform ParentCola;

    [SerializeField] int directionX;
    [SerializeField] int directionY;
    [SerializeField] int caminarX;
    [SerializeField] int caminarY;
    int x=2, y = 2;
    private void Awake()
    {
        positions = new List<Vector2>();
        caminarX = 1;
        directionX = x+1;
        directionY = y;
    }

    private void Start()
    {
        InitialAction();
    }

    private void Update()
    {
        
        HandleInput();
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 && caminarX == 0)
        {
            caminarX = (int)horizontalInput;
            caminarY = 0;
        }
        else if (verticalInput != 0 && caminarY ==0)
        {
            caminarY = (int)verticalInput; 
            caminarX = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.matrizFondo[directionY, directionX], speed * Time.deltaTime);

        if (Mathf.Approximately(transform.position.x, GameManager.Instance.matrizFondo[directionY, directionX].x) &&
            Mathf.Approximately(transform.position.y, GameManager.Instance.matrizFondo[directionY, directionX].y))
        {
            UpdateTailPosition();
            if (colas.Count > 0)
            {
                positions.Insert(0, GameManager.Instance.matrizFondo[directionY, directionX]);
                if (positions.Count - colas.Count >=2)
                {
                    positions.RemoveAt(colas.Count+1);
                }
                
            }
            directionX +=caminarX;
            directionY += caminarY;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Comidita"))
        {
            GameManager.Instance.GenerateComida();
            GameManager.Instance.comio = true;
            AddCola((int)colas[colas.Count-1].position.x, (int)colas[colas.Count - 1].position.y);
        }
    }

    private void AddCola(int x1,int y1)
    {
        Vector3 tmtVector = GameManager.Instance.matrizFondo[y1, x1];
        GameObject tmp = Instantiate(colaPrefab,new Vector3(tmtVector.x, tmtVector.y, 0), Quaternion.identity, ParentCola);
        colas.Insert(0,tmp.transform);
    }

    private void InitialAction()
    {
        transform.position = GameManager.Instance.matrizFondo[y, x];
        positions.Add(GameManager.Instance.matrizFondo[y, x]);
        x--;
        AddCola(x,y);
        
        positions.Add(GameManager.Instance.matrizFondo[y, x]);
        x--;
        AddCola(x,y);
        positions.Add(GameManager.Instance.matrizFondo[y, x]);
    }

    private void UpdateTailPosition()
    {
        for (int i = 0; i < colas.Count; i++)
        {
            colas[i].position = positions[i];
        }
    }
}
