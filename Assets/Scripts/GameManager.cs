using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] GameObject comidaPrefab;
    [SerializeField] GameObject[] fondos;
    public Vector2[,] matrizFondo;
    [SerializeField] Transform matrizPadre;
    [SerializeField] Player playerReferences;


    [Range(6, 30)]
    public int valx ;
    [Range(6, 12)]
    public int valy;

    public bool murio = false;
    public bool comio = false;
    

    private void Awake()
    {
        CreacionMatriz();
       
        //MusicManagerPersistent.Instance.PlayRandomMusic();
    }
    void Start()
    {
        GenerateComida();
    }
    void CreacionMatriz()
    {
        float inicioI = -(valx / 4);
        float inicioJ = -(valy / 4);
        matrizFondo = new Vector2[valy, valx]; 
        for (int i = 0; i < matrizFondo.GetLength(0); i++)
        {
            for (int j = 0; j < matrizFondo.GetLength(1); j++)
            {
                matrizFondo[i, j] = new Vector2(inicioI, inicioJ); 
                if ((i + j) % 2 == 0)
                {
                    Instantiate(fondos[0], matrizFondo[i, j], Quaternion.identity, matrizPadre);
                }
                else
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
        Vector2 comidaPos;
        do
        {
            comidaPos = matrizFondo[Random.Range(0, valy), Random.Range(0, valx)];
        } while (IsSnakePosition(comidaPos));

        comidaPrefab.transform.position = comidaPos;

    }
    bool IsSnakePosition(Vector2 pos)
    {
        foreach (Vector2 snakePos in playerReferences.positions)
        {
            if (snakePos == pos) 
                return true;
        }
        return false;
    }

}
