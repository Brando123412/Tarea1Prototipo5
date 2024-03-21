using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject comidaPrefab;

    [SerializeField]GameObject[] fondos;
    public Vector2[,] matrizFondo;
    [SerializeField]Transform matrizPadre;
    [SerializeField] Player playerReferences;


    [Range(3, 30)]
    public int valx ;
    [Range(3, 18)]
    public int valy;

    public bool comio =false;
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

        float inicioI = -(valx/4);
        float inicioJ = -(valy/4);
        matrizFondo = new Vector2[valy,valx];
        for (int i = 0; i < matrizFondo.GetLength(0); i++)
        {
            for (int j = 0; j < matrizFondo.GetLength(1); j++)
            {
                matrizFondo[i, j] = new Vector2(inicioI, inicioJ);
                if ((i +j) % 2 ==0)
                {
                    Instantiate(fondos[0], matrizFondo[i,j], Quaternion.identity,matrizPadre);
                }else
                {
                    Instantiate(fondos[1], matrizFondo[i, j], Quaternion.identity, matrizPadre);
                }
                
                inicioI += 0.512f;
            }
            inicioJ += 0.512f;
            inicioI = -valx / 4;
        }
    }
    public void GenerateComida()
    {
        comio = true;
        Vector2 comidaPos;
        do
        {
            comidaPos = matrizFondo[Random.Range(0, valy), Random.Range(0, valx)];
        } while (IsSnakePosition(comidaPos));

        comidaPrefab.transform.position = comidaPos;
        comio = false;
    }
    bool IsSnakePosition(Vector2 pos)
    {
        // Verificar si la posición está ocupada por la serpiente
        foreach (Vector2 snakePos in playerReferences.positions)
        {
            if (pos == snakePos)
                return true;
        }
        return false;
    }
}
