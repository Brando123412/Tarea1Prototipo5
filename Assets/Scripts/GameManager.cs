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


    [Range(3, 15)]
    public int valx, valy;

    public bool comio =false;
    public bool murio = false;

    private void Awake()
    {
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
        CreacionMatriz();
        GenerateComida();
    }
    void Update()
    {
        
    }
    void CreacionMatriz()
    {

        float inicioI = -valx/2;
        float inicioJ = -valy/2;
        matrizFondo = new Vector2[valy,valx];
        for (int i = 0; i < matrizFondo.GetLength(0); i++)
        {
            for (int j = 0; j < matrizFondo.GetLength(1); j++)
            {
                matrizFondo[i, j] = new Vector2(inicioI, inicioJ);
                if ((i +j) % 2 ==0)
                {
                    print("Hola1");
                    Instantiate(fondos[0], matrizFondo[i,j], Quaternion.identity,matrizPadre);
                }else
                {
                    print("Hola2");
                    Instantiate(fondos[1], matrizFondo[i, j], Quaternion.identity, matrizPadre);
                }
                
                //print(matrizFondo[i,j]);
                inicioI += 1.024f;
            }
            inicioJ += 1.024f;
            inicioI = -valx / 2;
        }
    }
    public void GenerateComida()
    {
        comio = true;
        //
        Vector2 comidaPos;

        // Intentar encontrar una posición no ocupada por la serpiente
        do
        {
            comidaPos = matrizFondo[Random.Range(0, valy), Random.Range(0, valx)];
        } while (IsSnakePosition(comidaPos));

        comidaPrefab.transform.position = comidaPos;
        ///Hacer instancia de la manzana
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
