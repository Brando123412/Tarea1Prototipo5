using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject comidaPrefab;

    [SerializeField]GameObject[] fondos;
    public Transform[,] matrizFondo;
    [SerializeField]Transform matrizPadre;
    [SerializeField] Player playerReferences;


    [Range(3, 30)]
    public int valx ;
    [Range(3, 18)]
    public int valy;

    public bool murio = false;
    

    private void Awake()
    {
        CreacionMatriz();
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        GenerateComida();
    }
    void CreacionMatriz()
    {

        float inicioI = -(valx / 4);
        float inicioJ = -(valy / 4);
        matrizFondo = new Transform[valy, valx]; // Cambiado a un arreglo de Transform
        for (int i = 0; i < matrizFondo.GetLength(0); i++)
        {
            for (int j = 0; j < matrizFondo.GetLength(1); j++)
            {
                matrizFondo[i, j] = new GameObject("Tile (" + i + ", " + j + ")").transform; // Crear un nuevo GameObject como transform
                matrizFondo[i, j].position = new Vector3(inicioI, inicioJ, 0f); // Asignar posición al transform
                if ((i + j) % 2 == 0)
                {
                    Instantiate(fondos[0], matrizFondo[i, j].position, Quaternion.identity, matrizPadre);
                }
                else
                {
                    Instantiate(fondos[1], matrizFondo[i, j].position, Quaternion.identity, matrizPadre);
                }
                inicioI += 0.512f;
            }
            inicioJ += 0.512f;
            inicioI = -valx / 4;
        }
    }
    public void GenerateComida()
    {

        Vector2 comidaPos;
        do
        {
            comidaPos = matrizFondo[Random.Range(0, valy), Random.Range(0, valx)].position; // Acceder a la posición del transform
        } while (IsSnakePosition(comidaPos));

        comidaPrefab.transform.position = comidaPos;

    }
    bool IsSnakePosition(Vector2 pos)
    {
        foreach (Transform snakePos in playerReferences.positions)
        {
            if ((Vector2)snakePos.position == pos) // Acceder a la posición del transform
                return true;
        }
        return false;
    }

}
