using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]GameObject[] fondos;
    Vector2[,] matrizFondo;
    [SerializeField]Transform matrizPadre;

    [Range(1, 10)]
    public int valx, valy;
    void Start()
    {
        CreacionMatriz();
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
}
