using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Student;
    public GameObject Desk;
    GameObject deskObject;

    public bool[,] map1 = new bool[16, 7];
    public int rowOfMap = 16;
    public int colOfMap = 7;
    public int numberOfTrueTiles = 30;

    void Start()
    {
        GenerateRandomTiles();
        SetChair();
    }

    void GenerateRandomTiles()
    {
        int totalTiles = map1.GetLength(0) * map1.GetLength(1);
        if (numberOfTrueTiles > totalTiles)
        {
            numberOfTrueTiles = totalTiles;
        }

        int remainingTrueTiles = numberOfTrueTiles;

        for (int x = 0; x < map1.GetLength(0); x++)
        {
            for (int y = 0; y < map1.GetLength(1); y++)
            {
                if (UnityEngine.Random.Range(0, totalTiles) < remainingTrueTiles)
                {
                    map1[x, y] = true;
                    remainingTrueTiles--;
                }
                totalTiles--;
            }
        }
    }

    void SetChair() { // 16 * 7 책상 배치
        for (int i = 0; i < 16; i++){
            for (int j = 0; j < 7; j++){
                if (map1[i, j] == true) { // 1이라면 Prefab1
                    deskObject = Instantiate(Student);
                    deskObject.transform.position = new Vector2(i * 3, j * (-3f) - 1f);
                } else if (map1[i,j] == false) { // 0이라면 Prefab2
                    deskObject = Instantiate(Desk);
                    deskObject.transform.position = new Vector2(i * 3, j * (-3f) - 1f);
                }
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {

    }
}
